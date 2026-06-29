using IdentityService.Domain.Errors;

namespace IdentityService.Domain.Shared;

public record Result
{
    protected Result(bool isSuccess, Error? error)
    {
        if (isSuccess && error is not null || !isSuccess && error is null) throw new InvalidOperationException();

        IsSuccess = isSuccess;
        Error = error;
    }

    public bool IsSuccess { get; }
    public Error? Error { get; }

    public static Result Success() =>
        new(true, null);
    
    public static Result Failure(Error error) =>
        new(false, error);
}

