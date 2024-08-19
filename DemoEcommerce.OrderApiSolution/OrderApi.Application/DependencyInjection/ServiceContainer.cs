using Ecommerce.SharedLibrary.Logs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderApi.Application.Interfaces;
using OrderApi.Application.Services;
using Polly;
using Polly.Retry;

namespace OrderApi.Application.DependencyInjection;

public static class ServiceContainer
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration config)
    {
        services.AddHttpClient<IOrderService, OrderService>(opt =>
        {
            opt.BaseAddress = new Uri(config["ApiGateway:BaseUrl"]!);
            opt.Timeout = TimeSpan.FromSeconds(1);
        });

        var retryStrategy = new RetryStrategyOptions()
        {
            ShouldHandle = new PredicateBuilder().Handle<HttpRequestException>(),
            BackoffType = DelayBackoffType.Constant,
            MaxRetryAttempts = 3,
            Delay = TimeSpan.FromMilliseconds(500),
            OnRetry = args =>
            {
                string msg = $"OnRetry, Attempt: {args.AttemptNumber} Outcome: {args.Outcome}";
                LogException.LogToDebugger(msg);
                LogException.LogToConsole(msg);
                return ValueTask.CompletedTask;
            }
        };
        
        services.AddResiliencePipeline<string>("my-retry-pipeline", builder =>
        {
            builder.AddRetry(retryStrategy);
        });
        
        return services;
    }
}