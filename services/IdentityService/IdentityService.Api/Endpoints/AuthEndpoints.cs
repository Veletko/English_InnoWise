using IdentityService.Application.Interfaces;
using IdentityService.Application.Services;
using IdentityService.Application.DTOs;
using Microsoft.AspNetCore.Identity.Data;
using IdentityService.Api.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Api.Endpoints;

public static class AuthEndpoints
{
    
    public static IEndpointRouteBuilder MapAuthEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/auth");

        group.MapPost("/login", LoginAsync); 
        group.MapPost("/register", RegisterAsync);
        group.MapPost("/refresh", UpdateRefreshTokenAsync);
        group.MapPost("/revoke", RevokeTokenAsync);

        return app;
    }

    private static async Task<IResult> LoginAsync(
        [FromBody] LoginRequestDto request, 
        IAuthService authService, 
        CancellationToken cancellationToken) 
    {
        var result = await authService.LoginAsync(request, cancellationToken); 
        return result.ToHttpResponse(); 
    }

    private static async Task<IResult> RegisterAsync(
        [FromBody] RegisterRequestDto request,
        IAuthService authService,
        CancellationToken cancellationToken)
    {
        var result = await authService.RegisterAsync(request, cancellationToken);
        return result.ToHttpResponse();
    }

    private static async Task<IResult> UpdateRefreshTokenAsync(
        [FromBody] string? refreshToken,
        IAuthService authService,
        CancellationToken cancellationToken
    )
    {
        var result = await authService.UpdateRefreshTokenAsync(refreshToken, cancellationToken);
        return result.ToHttpResponse();
    }

    private static async Task<IResult> RevokeTokenAsync(
        [FromBody] string? refreshToken,
        IAuthService authService,
        CancellationToken cancellationToken)
    {
        var result = await authService.RevokeRefreshTokenAsync(refreshToken, cancellationToken);
        return result.ToHttpResponse();
    }
}