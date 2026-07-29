using FluentValidation;
using IdentityService.Application.DTOs;
using IdentityService.Application.Constants;

namespace IdentityService.Application.Validators;

public class LoginRequestValidator : AbstractValidator<LoginRequestDto>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage(ValidationMessages.Required)
            .EmailAddress().WithMessage(ValidationMessages.Invalid);
        
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage(ValidationMessages.Required)
            .MinimumLength(NumericalConsts.MinPasswordLength).WithMessage(ValidationMessages.TooShort)
            .Must(password => password.Any(char.IsUpper)).WithMessage(ValidationMessages.RequireUppercase)
            .Must(password => password.Any(char.IsLower)).WithMessage(ValidationMessages.RequireLowercase)
            .Must(password => password.Any(char.IsDigit)).WithMessage(ValidationMessages.RequireDigit)
            .Must(password => password.Any(ch => !char.IsLetterOrDigit(ch))).WithMessage(ValidationMessages.RequireNonAlphanumeric);
    }
}
