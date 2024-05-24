using backend_core.Application.Identity.Interfaces;
using backend_core.Application.Common.Interfaces.Services;
using backend_core.Application.Contracts.Infrastructure;
using backend_core.Application.Contracts.Persistance;
using backend_core.Application.Identity;
using backend_core.Application.Models;
using backend_core.Contracts.Persistance;
using backend_core.Infrastructure.Authentication;
using backend_core.Infrastructure.Mail;
using backend_core.Infrastructure.Persistence.Data;
using backend_core.Infrastructure.Persistence.Repositories;
using backend_core.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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

        services.AddScoped<IUserRepository, UserRepository>();

        // Authentication Services:

        services.Configure<JwtSettings>(config.GetSection(JwtSettings.SectionName));
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();


        // Email Service:

        services.Configure<EmailSettings>(config.GetSection("EmailSettings"));
        services.AddTransient<IEmailSender, EmailSender>();



        return services;
    }
}
