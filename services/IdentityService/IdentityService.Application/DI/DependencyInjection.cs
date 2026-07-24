using System.Reflection;
using IdentityService.Application.Interfaces;
using IdentityService.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;

namespace IdentityService.Application.DI;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        services
            .AddServices()
            .AddValidators();
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        return services;
    }

    private static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        
        return services;
    }
}
