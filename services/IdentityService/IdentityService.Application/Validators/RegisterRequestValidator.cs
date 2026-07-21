using FluentValidation;
using IdentityService.Application.DTOs;

namespace IdentityService.Application.Validators;

public class RegisterRequestValidator : AbstractValidator<RegisterRequestDto>
{
    public RegisterRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Email is invalid");
        
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(6).WithMessage("Password length is 6")
            .Must(password => password.Any(char.IsUpper)).WithMessage("Password must contain at least one uppercase letter")
            .Must(password => password.Any(char.IsLower)).WithMessage("Password must contain at least one lowercase letter")
            .Must(password => password.Any(char.IsDigit)).WithMessage("Password must contain at least one number")
            .Must(password => password.Any(ch => !char.IsLetterOrDigit(ch))).WithMessage("Password must contain at least one special character");
        
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required")
            .MinimumLength(3).WithMessage("First name must be at least 3 characters long")
            .Must(name => name.All(char.IsLetter)).WithMessage("First name must contain only letters");
        
        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required")
            .MinimumLength(3).WithMessage("Last name must be at least 3 characters long")
            .Must(lastName => lastName.All(char.IsLetter)).WithMessage("Last name must contain only letters");

        RuleFor(x => x.Role)
            .NotEmpty().WithMessage("Role is required");
    }
}
