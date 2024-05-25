using backend_core.Application.Identity.Interfaces;
using backend_core.Application.Common.Interfaces.Services;
using backend_core.Application.Contracts.Infrastructure;
using backend_core.Application.Contracts.Persistance;
using backend_core.Application.Identity;
using backend_core.Application.Models;
using backend_core.Infrastructure.Authentication;
using backend_core.Infrastructure.Mail;
using backend_core.Infrastructure.Persistence.Data;
using backend_core.Infrastructure.Repositories;
using backend_core.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using backend_core.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace backend_core.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager config)
    {
        // Database Services:

        var connectionString = config.GetConnectionString("mysql");
        services.AddDbContext<ApplicationDbContext>(options =>
               options.UseMySql(
                   connectionString,
                   ServerVersion.AutoDetect(connectionString)));


        // Repositories Services:

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPostRepository, PostRepository>();

        // Authentication Services:

        services.Configure<JwtSettings>(config.GetSection(JwtSettings.SectionName));
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();


        // Email Service:

        services.Configure<EmailSettings>(config.GetSection("EmailSettings"));
        services.AddTransient<IEmailSender, EmailSender>();

        // Identity Service:

        services.AddIdentity<User, IdentityRole>(options =>
        {
            options.User.RequireUniqueEmail = true;
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequiredLength = 12;
        })
        .AddEntityFrameworkStores<ApplicationDbContext>();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme =
            options.DefaultChallengeScheme =
            options.DefaultForbidScheme =
            options.DefaultScheme =
            options.DefaultSignInScheme =
            options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = config["JwtSettings:Issuer"],
            ValidateAudience = true,
            ValidAudience = config["JwtSettings:Audience"],
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                    System.Text.Encoding.UTF8.GetBytes(config["JwtSettings:Secret"])
                )
        };
    });

        return services;
    }
}
