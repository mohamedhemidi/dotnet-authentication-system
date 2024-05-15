using System.Reflection;
using backend_core.Application.Modules.Account.Commands.Register;
using backend_core.Application.Common.Behaviours;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace backend_core.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // services.AddMediatR(typeof(DependencyInjection).Assembly);
        // Version 12:
        services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));


        // FluentValidation :
        services.AddScoped(
            typeof(IPipelineBehavior<,>),
            typeof(ValidationBehaviour<,>));
        // services.AddScoped<IPipelineBehavior<RegisterCommand, ErrorOr<AccountResult>>, ValidationBehaviour<RegisterCommand, ErrorOr<AccountResult>>>();

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}
