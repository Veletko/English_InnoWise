using IdentityService.Application.DTOs;
using IdentityService.Application.Interfaces;
using IdentityService.Application.Errors; 
using IdentityService.Domain.Shared; 
using IdentityService.Domain.Errors; 
using Microsoft.AspNetCore.Identity;
using IdentityService.Domain.Entities;
using IdentityService.Domain.Enums;

namespace IdentityService.Application.Services;

public class AuthService(
        UserManager<User> userManager,
        IRefreshTokenRepository refreshTokenRepository,
        IJwtTokenGenerator jwtTokenGenerator,
        TimeProvider timeProvider) : IAuthService
{
    private static readonly TimeSpan RefreshTokenLifetime = TimeSpan.FromDays(7);
    private readonly UserManager<User> _userManager = userManager;
    private readonly IRefreshTokenRepository _refreshTokenRepository = refreshTokenRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;
    private readonly TimeProvider _timeProvider = timeProvider;
    
    private static RefreshToken GenerateRefreshToken(User user, DateTimeOffset currentTime, string refreshToken)
    { 
        return new RefreshToken
        {
            UserId = user.Id,
            Token = refreshToken,
            CreatedAtUtc = currentTime,
            ExpiresAtUtc = currentTime.Add(RefreshTokenLifetime),
            IsRevoked = false
        };
    }
    
    public async Task<Result<AuthResponseDto>> LoginAsync(LoginRequestDto request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        
        if (user is null)
        {
            return ApplicationErrors.UserErrors.InvalidCredentials;
        }
        
        var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);
        if (!isPasswordValid) 
        {
            return ApplicationErrors.UserErrors.InvalidCredentials;
        }

        var roles = await _userManager.GetRolesAsync(user);
        var accessToken = _jwtTokenGenerator.GenerateAccessToken(user, roles);
        var refreshToken = _jwtTokenGenerator.GenerateRefreshToken();
        
        var currentTime = _timeProvider.GetUtcNow();
        var refreshTokenEntity = GenerateRefreshToken(user, currentTime, refreshToken);
        
        await _refreshTokenRepository.AddAsync(refreshTokenEntity, cancellationToken);
        
        return new AuthResponseDto(accessToken, refreshToken);
    }
    public async Task<Result<AuthResponseDto>> RegisterAsync(RegisterRequestDto request, CancellationToken cancellationToken)
    {
        var existingUser = await _userManager.FindByEmailAsync(request.Email);
        if (existingUser is not null)
        {
            return ApplicationErrors.UserErrors.EmailInUse;
        }
        
        var currentTime = _timeProvider.GetUtcNow();
        
        var newUser = new User
        {
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            CreatedAtUtc = currentTime,
            UserName = request.Email
        };

        var result = await _userManager.CreateAsync(newUser, request.Password);
        if (!result.Succeeded)
        {
            return ApplicationErrors.UserErrors.UserCreationFailed;
        }

        var roleName = request.Role.ToString();
        await _userManager.AddToRoleAsync(newUser, roleName);

        IEnumerable<string> roles = [roleName];
        var accessToken = _jwtTokenGenerator.GenerateAccessToken(newUser, roles);
        var refreshToken = _jwtTokenGenerator.GenerateRefreshToken();
        
        var refreshTokenEntity = GenerateRefreshToken(newUser, currentTime, refreshToken);
        await _refreshTokenRepository.AddAsync(refreshTokenEntity, cancellationToken);

        return new AuthResponseDto(accessToken, refreshToken);
    }
    
    public async Task<Result<AuthResponseDto>> UpdateRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken)
    {
        var existingToken = await _refreshTokenRepository.GetByTokenAsync(refreshToken, cancellationToken, trackChanges:true);
        if (existingToken is null)
        {
            return ApplicationErrors.TokenErrors.NotFound;
        }

        if (existingToken.IsRevoked)
        {
            return ApplicationErrors.TokenErrors.Revoked;
        }
        
        var currentTime = _timeProvider.GetUtcNow();
        
        if (existingToken.ExpiresAtUtc < currentTime)
        {
            return ApplicationErrors.TokenErrors.Expired;
        }

        var user = await _userManager.FindByIdAsync(existingToken.UserId.ToString());
        if (user is null)
        {
            return ApplicationErrors.UserErrors.NotFound;
        }
        
        existingToken.IsRevoked = true;
        await _refreshTokenRepository.UpdateAsync(cancellationToken);
        
        var roles = await _userManager.GetRolesAsync(user);
        var accessToken = _jwtTokenGenerator.GenerateAccessToken(user, roles);
        var newRefreshToken = _jwtTokenGenerator.GenerateRefreshToken();
        
        var refreshTokenEntity = GenerateRefreshToken(user, currentTime, newRefreshToken);
        
        await _refreshTokenRepository.AddAsync(refreshTokenEntity, cancellationToken);
        
        return new AuthResponseDto(accessToken, newRefreshToken);
    }


    public async Task<Result> RevokeRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken)
    {
        var existingToken = await _refreshTokenRepository.GetByTokenAsync(refreshToken, cancellationToken, true);
        if (existingToken is null)
        {
            return Result.Success(); 
        }
        
        existingToken.IsRevoked = true;
        
        await _refreshTokenRepository.UpdateAsync(cancellationToken);

        return Result.Success();
    }
}

