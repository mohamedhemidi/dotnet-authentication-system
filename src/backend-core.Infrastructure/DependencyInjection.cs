using backend_core.Application.Identity.Common.Interfaces;
using backend_core.Domain.Interfaces;
using backend_core.Domain.Repositories;
using backend_core.Application.Identity;
using backend_core.Domain.Models;
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
using backend_core.Infrastructure.Mail.Models;
using backend_core.Application.Identity.Common.Services;

namespace backend_core.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager config)
    {
        // Common Services:

        services.AddHttpClient();

        // Database Services:

        // Mysql:
        var connectionString = config.GetConnectionString("mysql");
        services.AddDbContext<ApplicationDbContext>(options =>
               options.UseMySql(
                   connectionString,
                   ServerVersion.AutoDetect(connectionString)));

        // postgreSql:
        // var connectionString = config.GetConnectionString("postgresql");
        // services.AddDbContext<ApplicationDbContext>(options =>
        //        options.UseNpgsql(
        //            connectionString));
        // Repositories Services:

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPostRepository, PostRepository>();

        // Authentication Services:

        services.Configure<JwtSettings>(config.GetSection(JwtSettings.SectionName));
        services.AddTransient<IJwtToken, JwtToken>();
        services.AddSingleton<IRefreshToken, RefreshToken>();


        // Email Service:


        services.Configure<EmailSettings>(config.GetSection("EmailSettings"));
        services.AddTransient<IEmailSender, EmailSender>();
        services.AddSingleton<IEmailBodyBuilder, EmailBodyBuilder>();


        // Facebook Authentication Service
        services.Configure<FacebookSettings>(config.GetSection("FacebookSettings"));
        services.AddSingleton<IFacebookAuth, FacebookAuth>();


        // Google Authentication Service
        services.Configure<GoogleSettings>(config.GetSection("GoogleSettings"));

        // Identity Service:

        services.ConfigureIdentitySettings(config);

        return services;
    }
}
