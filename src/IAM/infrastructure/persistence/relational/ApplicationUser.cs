using iam.domain;
using Microsoft.AspNetCore.Identity;

namespace iam.infrastructure.persistence.relational;

internal sealed class ApplicationUser : IdentityUser
{
    public EProvider ProviderName { get; set; } // Para los External Providers
    public bool IsExternalUser { get; set; }
}
