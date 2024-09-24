using Microsoft.AspNetCore.Identity;

namespace iam.infrastructure.persistence.relational;

public static class DbInitializer
{
    public static async Task SeedRolesAsync(RoleManager<ApplicationRole> roleManager)
    {
        string[] roleNames = { "NormalUser", "PremiumUser", "ITSupport", "BusinessOperator", "Admin" };
        foreach (var roleName in roleNames)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new ApplicationRole(roleName, $"{roleName} role"));
            }
        }
    }   
}