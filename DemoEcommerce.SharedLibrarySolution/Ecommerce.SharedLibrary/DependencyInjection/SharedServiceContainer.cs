using Ecommerce.SharedLibrary.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Ecommerce.SharedLibrary.DependencyInjection;

public static class SharedServiceContainer
{
    public static IServiceCollection AddSharedServices<TContext>(this IServiceCollection services, 
        IConfiguration config, string fileName) where TContext : DbContext
    {
        // Generic dbContext
        services.AddDbContext<TContext>(opt =>
        {
            opt.UseNpgsql(config.GetConnectionString("EcommerceConnection"), npgOpt => 
                npgOpt.EnableRetryOnFailure(10));
        });
    
        // configuracion del Serilog logging
        Log.Logger = new LoggerConfiguration().MinimumLevel.Information()
            .WriteTo.Debug()
            .WriteTo.Console()
            .WriteTo.File(path: $"{fileName}-.text",
                restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information, 
                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {message:lj}{NewLine}{Exception}",
                rollingInterval: RollingInterval.Day).CreateLogger();

        JWTAuthenticationScheme.AddJWTAuthenticationScheme(services, config);
        
        return services;
    }

    public static IApplicationBuilder UseSharedPolicies(this IApplicationBuilder app)
    {
        app.UseMiddleware<GlobalExceptionMiddleware>();
        // app.UseMiddleware<ListenToOnlyApiGateway>();

        return app;
    }
}