using IdentityService.Application.Interfaces;
using IdentityService.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityService.Application.DI;

public static class DependecyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services.AddServices();
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddAuthentication();
        return services;
    }
}
