using iam.domain;
using iam.infrastructure.persistence.relational;
using Microsoft.AspNetCore.Identity;

namespace iam.infrastructure.mapping;

internal static class UserMapper
{
    public static async Task<ApplicationUser> ToApplicationUserAsync(User user, UserManager<ApplicationUser> userManager)
    {
        var appUser = await userManager.FindByIdAsync(user.UserName.ToString()) ?? new ApplicationUser();

        appUser.UserName = user.UserName.ToString();
        appUser.Email = user.Email.ToString();
        appUser.Roles.Clear();

        // Asignar roles
        var currentRoles = await userManager.GetRolesAsync(appUser);
        var newRoles = user.Roles.Select(r => r.Name).ToList();

        var rolesToRemove = currentRoles.Except(newRoles);
        var rolesToAdd = newRoles.Except(currentRoles);

        foreach (var role in rolesToRemove)
        {
            await userManager.RemoveFromRoleAsync(appUser, role);
        }

        foreach (var role in rolesToAdd)
        {
            await userManager.AddToRoleAsync(appUser, role);
        }

        return appUser;
    }

    // Mapea de ApplicationUser al dominio
    public static async Task<User> ToDomainUserAsync(ApplicationUser appUser, UserManager<ApplicationUser> userManager)
    {
        var userName = new UserName(appUser.UserName);
        var email = new Email(appUser.Email);

        // Obtener roles
        var roles = await userManager.GetRolesAsync(appUser);
        var domainRoles = roles.Select(role => new Role(role)).ToList();

        var user = new User(userName, email);
        foreach (var role in domainRoles)
        {
            user.AddRole(role);
        }

        return user;
    }
}