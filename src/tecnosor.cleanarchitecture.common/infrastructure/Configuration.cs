using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using tecnosor.cleanarchitecture.common.domain.events;
using tecnosor.cleanarchitecture.common.domain.maps;
using tecnosor.cleanarchitecture.common.infrastructure.events;
using tecnosor.cleanarchitecture.common.infrastructure.maps;
using tecnosor.cleanarchitecture.common.infrastructure.persistence.relational;

namespace tecnosor.cleanarchitecture.common.infrastructure;

public static class Configuration
{
    public static IServiceCollection AddCommonInfraStructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddRelationalPersistenceService(services, configuration);
        AddIdentity(services, configuration);
        services.AddScoped<IMapperService, MapperService>();
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
}

public static class AppConfiguration
{
    public static WebApplication UseCommonInfrastructure(this WebApplication webApp, IConfiguration configuration)
    {
        webApp.MapIdentityApi<IdentityUser>();
        return webApp;
    }
}