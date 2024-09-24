using Microsoft.AspNetCore.Identity;

namespace iam.infrastructure.persistence.relational;

internal sealed class ApplicationRole : IdentityRole
{
    public string Description { get; set; }

    public ApplicationRole() : base() { }

    public ApplicationRole(string roleName, string description) : base(roleName)
    {
        Description = description;
    }
}