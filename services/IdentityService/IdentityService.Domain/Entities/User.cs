using Microsoft.AspNetCore.Identity;
using IdentityService.Domain.Enums;

namespace IdentityService.Domain.Entities;

public class User : IdentityUser<Guid>
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
}