using Microsoft.AspNetCore.Mvc;
using IdentityService.Domain.Shared;
using IdentityService.Application.Exceptions;
using IdentityService.Domain.Errors;
using IdentityService.Domain.Enums;
namespace IdentityService.Api.Extensions;

public static class ResultExtensions
{
    public static IResult ToHttpResponse<T>(this Result<T> result)
    {
        if (result.IsSuccess)
        {
            return result.Value is null ? Results.Ok() : Results.Ok(result.Value);
        }
        
        return MapErrorToResult(result.Error!);
    }
    
    public static IResult ToHttpResponse(this Result result)
    {
        return result.IsSuccess ? Results.Ok() : MapErrorToResult(result.Error!);
    }
    
    private static IResult MapErrorToResult(Error error)
    {
        return error.Type switch
        {
            ErrorType.Unauthorized => Results.Json(
                new { error.Code, error.Message }, 
                statusCode: StatusCodes.Status401Unauthorized),
            
            ErrorType.Forbidden => Results.Json(
                new { error.Code, error.Message }, 
                statusCode: StatusCodes.Status403Forbidden),
            
            ErrorType.Conflict => Results.Conflict(new { error.Code, error.Message }),
            
            ErrorType.NotFound => Results.NotFound(new { error.Code, error.Message }),
            
            ErrorType.Validation => Results.BadRequest(new { error.Code, error.Message }),
            
            _ => Results.Json(
                new { Code = "InternalServerError", Message = "Произошла непредвиденная ошибка." }, 
                statusCode: StatusCodes.Status500InternalServerError)
        };
    }

}

