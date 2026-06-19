using IdentityService.Application.DTOs;
using IdentityService.Application.Interfaces;
using Microsoft.AspNetCore.Identity;
using IdentityService.Domain.Entities;

namespace IdentityService.Application.Services;

public class AuthService(UserManager<User> userManager,
        IRefreshTokenRepository refreshTokenRepository,
        IJwtTokenGenerator jwtTokenGenerator,
        TimeProvider timeProvider) : IAuthService
{
    private static readonly TimeSpan RefreshTokenLifetime = TimeSpan.FromDays(7);
    private readonly UserManager<User> _userManager = userManager;
    private readonly IRefreshTokenRepository _refreshTokenRepository = refreshTokenRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;
    private readonly TimeProvider _timeProvider = timeProvider;

    private static RefreshToken GenerateRefreshToken(User user, DateTimeOffset now, string refreshToken)
    { 
        return new RefreshToken
        {
            UserId = user.Id,
            Token = refreshToken,
            CreatedAtUtc = now,
            ExpiresAtUtc = now.Add(RefreshTokenLifetime),
            IsRevoked = false
        };
    }
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
        
        var now = _timeProvider.GetUtcNow();
        
        var refreshTokenEntity = GenerateRefreshToken(user, now, refreshToken);
        
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
        
        var now = _timeProvider.GetUtcNow();
        
        var newUser = new User
        {
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            CreatedAtUtc = now
        };

        var result = await _userManager.CreateAsync(newUser, request.Password);
        if (!result.Succeeded)
        {
            throw new Exception("User creation failed: " + string.Join(", ", result.Errors.Select(e => e.Description)));
        }

        var roleName = request.Role.ToString();
        await _userManager.AddToRoleAsync(newUser, roleName);

        var roles = new List<string> { roleName };
        var accessToken = _jwtTokenGenerator.GenerateAccessToken(newUser, roles);
        var refreshToken = _jwtTokenGenerator.GenerateRefreshToken();
        
        var refreshTokenEntity = GenerateRefreshToken(newUser, now, refreshToken);

        await _refreshTokenRepository.AddAsync(refreshTokenEntity, cancellationToken);

        return new AuthResponseDto(accessToken, refreshToken);

    }
    public async Task<AuthResponseDto> UpdateRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken)
    {
        var existingToken = await _refreshTokenRepository.GetByTokenAsync(refreshToken, cancellationToken, true)
            ?? throw new Exception("Refresh token not found");

        if (existingToken.IsRevoked)
        {
            throw new Exception("Refresh token is revoked");
        }
        
        var now = _timeProvider.GetUtcNow();
        
        if (existingToken.ExpiresAtUtc < now)
        {
            throw new Exception("Refresh token has expired");
        }

        var user = await _userManager.FindByIdAsync(existingToken.UserId.ToString())
            ?? throw new Exception("User not found");
        
        existingToken.IsRevoked = true;
        await  _refreshTokenRepository.UpdateAsync(existingToken, cancellationToken);
        
        var roles = await _userManager.GetRolesAsync(user);
        var accessToken = _jwtTokenGenerator.GenerateAccessToken(user, roles);
        var newRefreshToken = _jwtTokenGenerator.GenerateRefreshToken();
        
        var refreshTokenEntity = GenerateRefreshToken(user, now, newRefreshToken);
        
        await _refreshTokenRepository.AddAsync(refreshTokenEntity, cancellationToken);
        
        return new AuthResponseDto(accessToken, newRefreshToken);
    }
    public async Task RevokeRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken)
    {
        var existingToken = await _refreshTokenRepository.GetByTokenAsync(refreshToken, cancellationToken, true);
        if (existingToken is null)
        {
            return;
        }
        
        existingToken.IsRevoked = true;
        
        await _refreshTokenRepository.UpdateAsync(existingToken, cancellationToken);
    }
}
