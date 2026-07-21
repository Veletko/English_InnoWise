using System.Reflection;
using IdentityService.Application.Interfaces;
using IdentityService.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using IdentityService.Application.DTOs;
using IdentityService.Application.Validators;

namespace IdentityService.Application.DI;

public static class DependecyInjection
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

    private static void AddValidators(this IServiceCollection services)
    {
        services.AddScoped<IValidator<LoginRequestDto>, LoginRequestValidator>();
        services.AddScoped<IValidator<RegisterRequestDto>, RegisterRequestValidator>();
    }
}
