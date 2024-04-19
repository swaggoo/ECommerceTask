using ECommerce.Core.Interfaces;
using ECommerce.Core.MappingsRegistration;
using ECommerce.Services.Helpers;
using ECommerce.Services.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Services;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices
        (this IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IOrderService, OrderService>();
        services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));
        services.AddAutoMapper(typeof(ECommerceMappings));
        
        return services;
    }
}