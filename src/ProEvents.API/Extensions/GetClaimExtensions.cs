using System.Security.Claims;

namespace ProEvents.API.Extensions;
public static class GetClaimExtensions
{
    public static string GetUserName(this ClaimsPrincipal claim)
    {
        return claim.FindFirst(c => c.Type == "username")?.Value;
    }

    public static long GetUserId(this ClaimsPrincipal claim)
    {
        return long.Parse(claim.FindFirst(c => c.Type == "id")?.Value);
    }
}
