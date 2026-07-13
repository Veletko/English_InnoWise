using IdentityService.Api.Endpoints;
using IdentityService.Application.DI;
using IdentityService.Infrastructure.Data;
using IdentityService.Infrastructure.DI;
using Scalar.AspNetCore;

namespace IdentityService.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Services.AddInfrastructure(builder.Configuration);
        builder.Services.AddApplication();
        builder.Services.AddAuthentication();
        builder.Services.AddOpenApi();
        
        var app = builder.Build();
        
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapScalarApiReference();
        }
        
        using (var serviceScope = app.Services.CreateScope())
        {
            var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            dbContext.Database.EnsureCreated();
        }
        
        app.UseHttpsRedirection();

        app.UseAuthentication();

        app.MapAuthEndpoints();
        
        app.Run();
    }
}
