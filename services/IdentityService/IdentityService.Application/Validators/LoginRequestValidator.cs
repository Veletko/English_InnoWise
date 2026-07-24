using FluentValidation;
using IdentityService.Application.DTOs;
using IdentityService.Application.Constants;

namespace IdentityService.Application.Validators;

public class LoginRequestValidator : AbstractValidator<LoginRequestDto>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage(ValidationMessages.Email.Required)
            .EmailAddress().WithMessage(ValidationMessages.Email.Invalid);
        
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage(ValidationMessages.Password.Required)
            .MinimumLength(ValidationMessages.Password.MinPasswordLength).WithMessage(ValidationMessages.Password.TooShort)
            .Must(password => password.Any(char.IsUpper)).WithMessage(ValidationMessages.Password.RequireUppercase)
            .Must(password => password.Any(char.IsLower)).WithMessage(ValidationMessages.Password.RequireLowercase)
            .Must(password => password.Any(char.IsDigit)).WithMessage(ValidationMessages.Password.RequireDigit)
            .Must(password => password.Any(ch => !char.IsLetterOrDigit(ch))).WithMessage(ValidationMessages.Password.RequireNonAlphanumeric);
    }
}
