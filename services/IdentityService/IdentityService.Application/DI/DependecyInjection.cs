using IdentityService.Application.Interfaces;
using IdentityService.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityService.Application.DI;

public static class DependecyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddServices();
    }

    private static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
    }
}
