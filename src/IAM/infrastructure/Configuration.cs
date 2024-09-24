using Microsoft.EntityFrameworkCore;
using tecnosor.cleanarchitecture.common.infrastructure.persistence.relational;
using iam.domain;
using iam.infrastructure.persistence.relational;
using Microsoft.AspNetCore.Identity;

namespace iam.infrastructure;
// TODO: Sistema de roles
public static class Configuration
{
    public static IServiceCollection AddIAMInfraStructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddRelationalPersistenceService(services, configuration);
        services.AddTransient<IUserRepository, UserRepository>();
        return services;
    }

   

    private static void AddRelationalPersistenceService(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("IamConnection");
        services.AddDbContext<RelationalPersistenceContext>(options => options.UseNpgsql(connectionString));

        // TODO:
        /*
        services.AddIdentity<ApplicationUser, IdentityRole>()
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();

        services.AddAuthentication()
            .AddGoogle(options =>
            {
                options.ClientId = "your-client-id";
                options.ClientSecret = "your-client-secret";
            })
            .AddFacebook(options =>
            {
                options.AppId = "your-app-id";
                options.AppSecret = "your-app-secret";
            });
        */
    }
}