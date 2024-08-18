using Ecommerce.SharedLibrary.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductApi.Application.Interfaces;
using ProductApi.Infrastructure.Data;
using ProductApi.Infrastructure.Repositories;

namespace ProductApi.Infrastructure.DependencyInjection;

public static class ServiceContainer
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        SharedServiceContainer.AddSharedServices<ProductDbContext>(services, config, config["MySerilog:Filename"]!);
        
        // Repositorios
        services.AddScoped<IProduct, ProductRepository>();
        return services;
    }

    public static IApplicationBuilder UseInfrastructurePolicies(this IApplicationBuilder app)
    {
        SharedServiceContainer.UseSharedPolicies(app);
        return app;
    }
}