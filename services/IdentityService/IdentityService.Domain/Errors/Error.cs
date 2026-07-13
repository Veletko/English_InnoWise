using IdentityService.Domain.Shared;
using IdentityService.Domain.Enums;

namespace IdentityService.Domain.Errors;

public sealed record Error(
    string Code,
    string Message,
    ErrorType Type)
{
    public static implicit operator string(Error error) => error.Message;
    public static implicit operator Result(Error error) => Result.Failure(error);
}
