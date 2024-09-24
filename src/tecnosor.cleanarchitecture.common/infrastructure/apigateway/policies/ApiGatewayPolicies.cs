using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.RateLimiting;

namespace tecnosor.cleanarchitecture.common.infrastructure.apigateway.policies;
public static class ApiGatewayPolicies
{
    public static IServiceCollection AddCuotaPolicies(this IServiceCollection services, IConfiguration configuration)
    {
        const bool ct_cuotaEnabled = false;
        const int  ct_cuotaMaxLimit = 1000;
        const int  ct_cuotaMinutesInterval = 1;
        const int  ct_cuotaQueueLimit = 50;

        // Leer la configuración del appsettings.json
        bool cuotaEnabled = configuration.GetValue<bool>("Tecnosor:CleanArchitecture:Cuota:Enabled", ct_cuotaEnabled);
        int  cuotaMaxLimit = configuration.GetValue<int>("Tecnosor:CleanArchitecture:Cuota:MaxLimit", ct_cuotaMaxLimit);
        int  cuotaMinutesInterval = configuration.GetValue<int>("Tecnosor:CleanArchitecture:Cuota:MinutesInterval", ct_cuotaMinutesInterval);
        int  cuotaQueueLimit = configuration.GetValue<int>("Tecnosor:CleanArchitecture:Cuota:QueueLimit", ct_cuotaQueueLimit);

        services.AddRateLimiter(options =>
         {
             options.AddPolicy("StandardQuota", httpContext =>
             {
                return RateLimitPartition.GetFixedWindowLimiter(
                    partitionKey: httpContext.Request.Path,
                    factory: _ => new FixedWindowRateLimiterOptions
                    {
                        PermitLimit = 500,
                        Window = TimeSpan.FromMinutes(1),
                        QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                        QueueLimit = 5,
                    });
             });
         });
        return services;
    }
}
