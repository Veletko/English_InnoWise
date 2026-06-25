using IdentityService.Domain.Shared;
namespace IdentityService.Domain.Errors;

public sealed record Error(string Code, string Message)
{
    public static readonly Error None = new(string.Empty, string.Empty);

    public static implicit operator string(Error error) => error.Message;
    public static implicit operator Result(Error error) => Result.Failure(error);
    
    public override string ToString() => $"Code: {Code}\nMessage: {Message}";
}
