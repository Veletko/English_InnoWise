using IdentityService.Domain.Entities;

namespace IdentityService.Application.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateAccessToken(User user, IEnumerable<string> roles);
    string GenerateRefreshToken( );
}
