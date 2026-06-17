using IdentityService.Domain.Enums;

namespace IdentityService.Application.DTOs;

public record RegisterRequestDto
(
    string Email,
    string Password,
    string FirstName,
    string LastName,
    UserRole Role
);

