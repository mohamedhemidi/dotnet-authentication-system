using backend_core.Application.Common.Interfaces.Authentication;
using backend_core.Application.Common.Interfaces.Services;
using backend_core.Application.Contracts.Persistance;
using backend_core.Contracts.Persistance;
using backend_core.Infrastructure.Authentication;
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
        services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(
                   config.GetConnectionString("ConnectionString:mysql")));


        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        services.AddScoped<IUserRepository, UserRepository>();


        services.Configure<JwtSettings>(config.GetSection(JwtSettings.SectionName));
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();



        return services;
    }
}
