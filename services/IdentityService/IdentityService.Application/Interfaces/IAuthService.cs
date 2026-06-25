using IdentityService.Application.DTOs;
using IdentityService.Domain.Shared;

namespace IdentityService.Application.Interfaces;

public interface IAuthService
{
    Task<Result<AuthResponseDto>> LoginAsync(LoginRequestDto request, CancellationToken cancellationToken);
    Task<Result<AuthResponseDto>> RegisterAsync(RegisterRequestDto request, CancellationToken cancellationToken);
    Task<Result<AuthResponseDto>> UpdateRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken);
    Task<Result> RevokeRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken);
}

