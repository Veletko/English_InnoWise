namespace IdentityService.Domain.Entities;

public class RefreshToken
{
    public Guid Id { get; set; }
    public required string Token { get; set; }
    public bool IsRevoked { get; set; }
    public DateTimeOffset CreatedAtUtc { get; set; }
    public DateTimeOffset ExpiresAtUtc { get; set; }
    public Guid UserId { get; set; }
}
