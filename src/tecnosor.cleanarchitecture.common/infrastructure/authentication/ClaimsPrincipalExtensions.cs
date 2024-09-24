using Microsoft.IdentityModel.JsonWebTokens;
using System.Security.Claims;

namespace tecnosor.cleanarchitecture.common.infrastructure.authentication;

internal static class ClaimsPrincipalExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal? principal)
    {
        string? userId = principal?.FindFirstValue(JwtRegisteredClaimNames.Sub);

        return Guid.TryParse(userId, out Guid parsedUserId) ?
            parsedUserId :
            throw new ApplicationException("User id is unavailable");
    }
}

// TODO: Continue