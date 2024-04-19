using ECommerce.Core.Interfaces;
using ECommerce.Data.DbContexts;
using ECommerce.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Data;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices
        (this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database");
        
        // Add services to the container.
        services.AddDbContext<ECommerceDbContext>(options => options.UseSqlServer(connectionString));

        services.AddScoped<IRepository, Repository>();

        return services;
    }
}