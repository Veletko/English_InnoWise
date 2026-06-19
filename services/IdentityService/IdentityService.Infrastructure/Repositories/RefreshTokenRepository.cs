using IdentityService.Application.Interfaces;
using IdentityService.Domain.Entities;
using IdentityService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Infrastructure.Repositories;

public class RefreshTokenRepository(ApplicationDbContext context) : IRefreshTokenRepository
{
    private readonly ApplicationDbContext _context = context;
    public async Task<RefreshToken?> GetByTokenAsync(string token, CancellationToken cancellationToken, bool trackChanges = false)
    {
        return trackChanges
            ? await _context.RefreshTokens.FirstOrDefaultAsync(r => r.Token == token, cancellationToken)
            : await _context.RefreshTokens.AsNoTracking().FirstOrDefaultAsync(r => r.Token == token, cancellationToken);
    }

    public async Task AddAsync(RefreshToken refreshToken, CancellationToken cancellationToken)
    {
        _context.RefreshTokens.Add(refreshToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(RefreshToken refreshToken, CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}
