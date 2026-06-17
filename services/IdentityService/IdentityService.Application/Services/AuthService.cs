using IdentityService.Application.DTOs;
using IdentityService.Application.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace IdentityService.Application.Services;

public class AuthService(UserManager<Domain.Entities.User> userManager,
        IRefreshTokenRepository refreshTokenRepository,
        IJwtTokenGenerator jwtTokenGenerator) : IAuthService
{
    private readonly UserManager<Domain.Entities.User> _userManager = userManager;
    private readonly IRefreshTokenRepository _refreshTokenRepository = refreshTokenRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;

    public async Task<AuthResponseDto> LoginAsync(LoginRequestDto request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email)
            ?? throw new Exception("Wrong password or Email");
        
        
        var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);
        if (!isPasswordValid) {
            throw new Exception("Wrong password");
        }

        var roles = await _userManager.GetRolesAsync(user);
        var accessToken = _jwtTokenGenerator.GenerateAccessToken(user, roles);
        var refreshToken = _jwtTokenGenerator.GenerateRefreshToken();

        var refreshTokenEntity = new Domain.Entities.RefreshToken
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            Token = refreshToken,
            CreatedAtUtc = DateTimeOffset.UtcNow,
            ExpiresAtUtc = DateTimeOffset.UtcNow.AddDays(7),
            IsRevoked = false
        };
        await _refreshTokenRepository.AddAsync(refreshTokenEntity, cancellationToken);

        return new AuthResponseDto(accessToken, refreshToken);
    }
    public async Task<AuthResponseDto> RegisterAsync(RegisterRequestDto request, CancellationToken cancellationToken)
    {
        var existingUser = await _userManager.FindByEmailAsync(request.Email);
        if (existingUser is not null)
        {
            throw new Exception("User with this email already exists");
        }

        var newUser = new Domain.Entities.User
        {
            Id = Guid.NewGuid(),
            UserName = request.Email,
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            CreatedAtUtc = DateTimeOffset.UtcNow
        };

        var result = await _userManager.CreateAsync(newUser, request.Password);
        if (!result.Succeeded)
        {
            throw new Exception("User creation failed: " + string.Join(", ", result.Errors.Select(e => e.Description)));
        }

        string roleName = request.Role.ToString();
        await _userManager.AddToRoleAsync(newUser, roleName);

        var roles = new List<string> { roleName };
        string accessToken = _jwtTokenGenerator.GenerateAccessToken(newUser, roles);
        string refreshToken = _jwtTokenGenerator.GenerateRefreshToken();

        var refreshTokenEntity = new Domain.Entities.RefreshToken
        {
            Id = Guid.NewGuid(),
            UserId = newUser.Id,
            Token = refreshToken,
            CreatedAtUtc = DateTimeOffset.UtcNow,
            ExpiresAtUtc = DateTimeOffset.UtcNow.AddDays(7),
            IsRevoked = false
        };

        await _refreshTokenRepository.AddAsync(refreshTokenEntity, cancellationToken);

        return new AuthResponseDto(accessToken, refreshToken);

    }
    public async Task<AuthResponseDto> GenerateRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken)
    {
        var existingToken = await _refreshTokenRepository.GetByTokenAsync(refreshToken, cancellationToken)
            ?? throw new Exception("Refresh token not found");

        if (existingToken.IsRevoked)
        {
            throw new Exception("Refresh token is revoked");
        }

        if (existingToken.ExpiresAtUtc < DateTimeOffset.UtcNow)
        {
            throw new Exception("Refresh token has expired");
        }

        var user = await _userManager.FindByIdAsync(existingToken.UserId.ToString())
            ?? throw new Exception("User not found");
        
        existingToken.IsRevoked = true;
        
        var roles = await _userManager.GetRolesAsync(user);
        var accessToken = _jwtTokenGenerator.GenerateAccessToken(user, roles);
        var newRefreshToken = _jwtTokenGenerator.GenerateRefreshToken();
        
        var refreshTokenEntity = new Domain.Entities.RefreshToken
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            Token = newRefreshToken,
            CreatedAtUtc = DateTimeOffset.UtcNow,
            ExpiresAtUtc = DateTimeOffset.UtcNow.AddDays(7),
            IsRevoked = false
        };
        await _refreshTokenRepository.AddAsync(refreshTokenEntity, cancellationToken);
        
        return new AuthResponseDto(accessToken, refreshToken);
    }
    public async Task RevokeRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken)
    {
        var existingToken = await _refreshTokenRepository.GetByTokenAsync(refreshToken, cancellationToken);
        if (existingToken is null)
        {
            return;
        }
        
        existingToken.IsRevoked = true;
        
        await _refreshTokenRepository.UpdateAsync(existingToken, cancellationToken);
    }
}