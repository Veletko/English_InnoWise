using IdentityService.Api.Extensions;
using IdentityService.Application.DTOs;
using IdentityService.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Api.Endpoints;

public static class AuthHandler
{
    public static async Task<IResult> LoginAsync(
        [FromBody] LoginRequestDto request, 
        IAuthService authService, 
        CancellationToken cancellationToken) 
    {
        var result = await authService.LoginAsync(request, cancellationToken); 
        return result.ToHttpResponse(); 
    }

    public static async Task<IResult> RegisterAsync(
        [FromBody] RegisterRequestDto request,
        IAuthService authService,
        CancellationToken cancellationToken)
    {
        var result = await authService.RegisterAsync(request, cancellationToken);
        return result.ToHttpResponse();
    }

    public static async Task<IResult> UpdateRefreshTokenAsync(
        [FromBody] string? refreshToken,
        IAuthService authService,
        CancellationToken cancellationToken
    )
    {
        var result = await authService.UpdateRefreshTokenAsync(refreshToken, cancellationToken);
        return result.ToHttpResponse();
    }

    public static async Task<IResult> RevokeTokenAsync(
        [FromBody] string? refreshToken,
        IAuthService authService,
        CancellationToken cancellationToken)
    {
        var result = await authService.RevokeRefreshTokenAsync(refreshToken, cancellationToken);
        return result.ToHttpResponse();
    }
}