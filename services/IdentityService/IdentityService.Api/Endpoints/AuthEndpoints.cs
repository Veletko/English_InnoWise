using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;

namespace IdentityService.Api.Endpoints;

public static class AuthEndpoints
{
    
    public static IEndpointRouteBuilder MapAuthEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/auth").AddFluentValidationAutoValidation();

        group.MapPost("/login", AuthHandler.LoginAsync); 
        group.MapPost("/register", AuthHandler.RegisterAsync);
        group.MapPost("/refresh", AuthHandler.UpdateRefreshTokenAsync);
        group.MapPost("/revoke", AuthHandler.RevokeTokenAsync);

        return app;
    }
}
