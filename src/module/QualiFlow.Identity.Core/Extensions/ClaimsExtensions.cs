using System.Security.Claims;

namespace QualiFlow.Identity.Core.Extensions;

public static class ClaimsExtensions
{
    public static bool IsExpired(this IEnumerable<Claim> claims)
    {
        var expString = claims.FirstOrDefault(x => x.Type == "exp")?.Value.Trim();
        var exp = !string.IsNullOrEmpty(expString) ? long.Parse(expString) : 0;
        var expireAt = DateTimeOffset.FromUnixTimeSeconds(exp);
        return expireAt < DateTimeOffset.UtcNow;
    }
}
