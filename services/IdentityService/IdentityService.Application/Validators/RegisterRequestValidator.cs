using FluentValidation;
using IdentityService.Application.DTOs;
using IdentityService.Application.Constants;

namespace IdentityService.Application.Validators;

public class RegisterRequestValidator : AbstractValidator<RegisterRequestDto>
{
    public RegisterRequestValidator()
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
        
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage(ValidationMessages.Required)
            .MinimumLength(NumericalConsts.MinNameLength).WithMessage(ValidationMessages.TooShort)
            .Must(name => name.All(char.IsLetter)).WithMessage(ValidationMessages.MustBeLetters);
        
        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage(ValidationMessages.Required)
            .MinimumLength(NumericalConsts.MinNameLength).WithMessage(ValidationMessages.TooShort)
            .Must(lastName => lastName.All(char.IsLetter)).WithMessage(ValidationMessages.MustBeLetters);

        RuleFor(x => x.Role)
            .NotEmpty().WithMessage("Role is required");
    }
}
