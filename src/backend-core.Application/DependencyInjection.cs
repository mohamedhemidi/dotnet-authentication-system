using System.Reflection;
using backend_core.Application.Common.Behaviors;
using backend_core.Application.Identity.Common.Services;
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

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehaviur<,>));
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        services.AddScoped<GetCurrentUser>();

        return services;
    }
}
