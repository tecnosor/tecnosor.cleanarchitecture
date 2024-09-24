using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using tecnosor.cleanarchitecture.common.domain.events;
using tecnosor.cleanarchitecture.common.domain.logging;
using tecnosor.cleanarchitecture.common.domain.maps;
using tecnosor.cleanarchitecture.common.domain.time;
using tecnosor.cleanarchitecture.common.infrastructure.events;
using tecnosor.cleanarchitecture.common.infrastructure.logging;
using tecnosor.cleanarchitecture.common.infrastructure.maps;
using tecnosor.cleanarchitecture.common.infrastructure.persistence.relational;
using tecnosor.cleanarchitecture.common.infrastructure.time;

namespace tecnosor.cleanarchitecture.common.infrastructure;

public static class Configuration
{
    public static IServiceCollection AddCommonInfraStructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddLogging(services, configuration);
        AddRelationalPersistenceService(services, configuration);
        AddIdentity(services, configuration);

        services.AddScoped<IMapperService, MapperService>();
        services.AddSingleton<IDateTimeProvider, DateTimeProviderImpl>();
        services.AddEventBus();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        return services;
    }

    private static IServiceCollection AddEventBus(this IServiceCollection services)
    {
        services.AddSingleton<EventBusServiceImpl>();
        services.AddSingleton<IEventBusService>(sp => sp.GetRequiredService<EventBusServiceImpl>());
        services.AddSingleton<IEventEmit>(sp => sp.GetRequiredService<EventBusServiceImpl>());
        return services;
    }

    private static void AddRelationalPersistenceService(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("UserConnection");
        services.AddDbContext<RelationalPersistenceContext>(options => options.UseNpgsql(connectionString));
    }

    private static void AddIdentity(IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthorization()
        .AddIdentityApiEndpoints<IdentityUser>()
        .AddEntityFrameworkStores<RelationalPersistenceContext>();
    }

    private static void AddLogging(IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<LoggingOptions>(configuration.GetSection("Tecnosor:CleanArchitecture:Logging"));
        logging.LoggerConfiguration.Configure(configuration);
        services.AddScoped(typeof(ILoggerService<>), typeof(LoggerServiceImpl<>));
    }

    private static void UseLogging(IServiceCollection services, IConfiguration configuration)
    {
        logging.LoggerConfiguration.Configure(configuration);
        services.AddScoped(typeof(ILoggerService<>), typeof(LoggerServiceImpl<>));
    }
}

public static class AppConfiguration
{
    public static WebApplication UseCommonInfrastructure(this WebApplication webApp, IConfiguration configuration)
    {
        webApp.UseMiddleware<RequestLoggingMiddleware>();
        webApp.UseMiddleware<UnmanagedExceptionMiddleware>();
        webApp.MapIdentityApi<IdentityUser>();
        return webApp;
    }
}