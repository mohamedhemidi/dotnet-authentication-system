using backend_core.Application.Identity.Interfaces;
using backend_core.Domain.Interfaces;
using backend_core.Domain.Repositories;
using backend_core.Application.Identity;
using backend_core.Domain.Models;
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
using backend_core.Infrastructure.Mail.Models;

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
        string infrastructureRoot = Path.Combine(AppContext.BaseDirectory, "Mail", "Templates");

        services.Configure<EmailSettings>(config.GetSection("EmailSettings"));
        services.AddTransient<IEmailSender, EmailSender>();
        services.AddSingleton<IEmailBodyBuilder, EmailBodyBuilder>();

        // Identity Service:

        services.ConfigureIdentitySettings(config);

        return services;
    }
}
