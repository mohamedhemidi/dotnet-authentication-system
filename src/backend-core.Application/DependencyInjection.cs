using backend_core.Application.Services.Account;
using Microsoft.Extensions.DependencyInjection;

namespace backend_core.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAccountService, AccountService>();

        return services;
    }
}
