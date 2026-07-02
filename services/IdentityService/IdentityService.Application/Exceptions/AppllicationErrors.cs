using IdentityService.Domain.Enums;
using IdentityService.Domain.Errors;

namespace IdentityService.Application.Exceptions;

public static class ApplicationErrors
{
    public static class UserErrors
    {
        public static readonly Error NotFound = new($"{nameof(UserErrors)}.{nameof(NotFound)}", 
            "User was not found.", ErrorType.NotFound);

        public static readonly Error EmailInUse = new($"{nameof(UserErrors)}.{nameof(EmailInUse)}", 
            "User with this email already exists.", ErrorType.Conflict);

        public static readonly Error InvalidCredentials = new($"{nameof(UserErrors)}.{nameof(InvalidCredentials)}", 
            "Wrong email or password.", ErrorType.Validation);
    }

    public static class TokenErrors
    {
        public static readonly Error NotFound = new($"{nameof(TokenErrors)}.{nameof(NotFound)}", 
            "Refresh token not found.", ErrorType.NotFound);

        public static readonly Error Revoked = new($"{nameof(TokenErrors)}.{nameof(Revoked)}", 
            "Refresh token is revoked.", ErrorType.Unauthorized);

        public static readonly Error Expired = new($"{nameof(TokenErrors)}.{nameof(Expired)}", 
            "Refresh token has expired.", ErrorType.Unauthorized);
    }
}

