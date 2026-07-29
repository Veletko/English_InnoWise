namespace IdentityService.Application.Constants;

public static class ValidationMessages
{

    public const string Required = "{PropertyName} is required";
    public const string Invalid = "{PropertyName} is invalid";
    public const string TooShort = "{PropertyName} must be at least {MinLength} characters long";
    public const string MustBeLetters = "{PropertyName} must contain only letters";
    public const string RequireUppercase = "{PropertyName} must contain at least one uppercase letter";
    public const string RequireLowercase = "{PropertyName} must contain at least one lowercase letter";
    public const string RequireDigit = "{PropertyName} must contain at least one number";
    public const string RequireNonAlphanumeric = "{PropertyName} must contain at least one special character";
    
}
