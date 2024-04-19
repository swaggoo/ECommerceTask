using System.Text.Json;
using ECommerce.API.Middleware;
using ECommerce.Services.Helpers.Pagination;
using Microsoft.OpenApi.Models;
namespace ECommerce.API.Extensions;

public static class ApiServicesExtension
{
    public static IServiceCollection AddApiServices
        (this IServiceCollection services)
    {
        services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
        {
            builder.WithOrigins("http://localhost:4200")
                .AllowAnyMethod()
                .AllowAnyHeader();
        }));
        services.AddControllers();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "ECommerce.API", Version = "v1" });
        });
        
        return services;
    }
    
    public static WebApplication UseApiServices
        (this WebApplication app)
    {
        app.MapControllers();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ECommerce.API v1"));
        app.UseMiddleware<ExceptionMiddleware>();
        app.UseCors("MyPolicy");
        
        return app;
    }
}