using IdentityService.Domain.Shared;
using IdentityService.Domain.Errors;
using IdentityService.Domain.Enums;

namespace IdentityService.Api.Extensions;

public static class ResultExtensions
{
    public static IResult ToHttpResponse<T>(this Result<T> result)
    {
        return result.IsSuccess ? Results.Ok(result.Value) : MapErrorToResult(result.Error!);
    }
    
    public static IResult ToHttpResponse(this Result result)
    {
        return result.IsSuccess ? Results.Ok() : MapErrorToResult(result.Error!);
    }
    
    private static IResult MapErrorToResult(Error error)
    {
        var extensions = new Dictionary<string, object?>
        {
            { "code", error.Code}
        };
        return error.Type switch
        {
            ErrorType.Unauthorized => Results.Problem(
                detail: error.Message,
                statusCode: StatusCodes.Status401Unauthorized,
                title: "Unauthorized",
                extensions: extensions
                ),
            
            ErrorType.Forbidden => Results.Problem(
                detail: error.Message,
                statusCode: StatusCodes.Status403Forbidden,
                title: "Forbidden",
                extensions: extensions),
            
            ErrorType.Conflict => Results.Problem(
                detail: error.Message,
                statusCode: StatusCodes.Status409Conflict,
                title: "Conflict",
                extensions: extensions),
            
            ErrorType.NotFound => Results.Problem(
                detail: error.Message,
                statusCode: StatusCodes.Status404NotFound,
                title: "NotFound",
                extensions: extensions),
            
            ErrorType.Validation => Results.Problem(
                detail: error.Message,
                statusCode: StatusCodes.Status400BadRequest,
                title: "BadRequest",
                extensions: extensions),
            
            _ => Results.Problem(
                detail: error.Message,
                statusCode: StatusCodes.Status500InternalServerError,
                title: "Internal Server Error",
                extensions: new Dictionary<string, object?> { { "code", "InternalServerError" } })
        };
    }
}

