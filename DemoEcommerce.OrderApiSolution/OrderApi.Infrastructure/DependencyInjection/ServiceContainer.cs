using Ecommerce.SharedLibrary.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderApi.Application.Interfaces;
using OrderApi.Infrastructure.Data;
using OrderApi.Infrastructure.Repositories;

namespace OrderApi.Infrastructure.DependencyInjection;

public static class ServiceContainer
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        /*
         * Agregar conexion a Db
         * Agregar auth scheme
         */
        SharedServiceContainer.AddSharedServices<OrderDbContext>(services, config, config["MySerilog:Filename"]!);

        services.AddScoped<IOrder, OrderRepository>();

        return services;
    }
    
    public static IApplicationBuilder UseInfrastructurePolicies(this IApplicationBuilder app)
    {
        /*
         * Agregar hadler global de excepciones
         * Agregar politicas de Gateway
         */
        SharedServiceContainer.UseSharedPolicies(app);

        
        return app;
    }
}