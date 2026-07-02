using System.Text;
using IdentityService.Api.Endpoints;
using IdentityService.Application.DI;
using IdentityService.Infrastructure.DI;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;

namespace IdentityService.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Services.AddInfrastructure(builder.Configuration);
        builder.Services.AddApplication();
        
        builder.Services.AddOpenApi();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapScalarApiReference();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();

        app.MapAuthEndpoints();
        
        app.Run();
    }
}
