using System.Security.Claims;

namespace Shared.Extensions;

public static class ClaimsExtensions
{
    public static string GetUserEmail(this ClaimsPrincipal? principal)
    {
        return principal?.Claims?.FirstOrDefault(p => p.Type == ClaimTypes.Email)?.Value ?? string.Empty;
    }
}
