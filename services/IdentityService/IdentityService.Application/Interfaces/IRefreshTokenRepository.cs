using IdentityService.Domain.Entities;

namespace IdentityService.Application.Interfaces;

public interface IRefreshTokenRepository
{
    Task<RefreshToken?> GetByTokenAsync(string token, CancellationToken cancellationToken, bool trackChanges = false);
    Task AddAsync(RefreshToken refreshToken, CancellationToken cancellationToken);
    Task UpdateAsync(CancellationToken cancellationToken);
}

