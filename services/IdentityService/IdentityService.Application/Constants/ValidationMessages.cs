namespace IdentityService.Application.Constants;

public static class ValidationMessages
{
    public static class Email
    {
        public const string Required = "Email is required";
        public const string Invalid = "Email is invalid";
    }

    public static class Password
    {
        public const int MinPasswordLength = 6;
        public const string Required = "Password is required";
        public static readonly string TooShort = $"Password length is at least {MinPasswordLength} characters";
        public const string RequireUppercase = "Password must contain at least one uppercase letter";
        public const string RequireLowercase = "Password must contain at least one lowercase letter";
        public const string RequireDigit = "Password must contain at least one number";
        public const string RequireNonAlphanumeric = "Password must contain at least one special character";
    }

    public static class Name
    {
        public const string Required = "{PropertyName} is required";
        public const string TooShort = "{PropertyName} must be at least 3 characters long";
        public const string MustBeLetters = "{PropertyName} must contain only letters";
    }
}
