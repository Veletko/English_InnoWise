namespace IdentityService.Infrastructure.Security;

public class JwtSettings
{
    public const string SectionName = "JwtSettings";
    
    public string Issuer { get; init; } = null!;
    public string Audience { get; init; } = null!;
    public string SecretKey { get; init; } = null!;
    public int ExpiryMinutes { get; init; }
}
