using Microsoft.EntityFrameworkCore;
using stolenCars.publication.domain;
using stolenCars.publication.infrastructure.persistence.relational;
using tecnosor.cleanarchitecture.common.infrastructure.persistence.relational;

namespace publication.infrastructure;

public static class Configuration
{
    public static IServiceCollection AddPublicationInfraStructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddRelationalPersistenceService(services, configuration);
        services.AddTransient<IPublicationRepository, UserRepository>();
        return services;
    }

    private static void AddRelationalPersistenceService(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("PublicationConnection");
        services.AddDbContext<RelationalPersistenceContext>(options => options.UseNpgsql(connectionString));
    }
}