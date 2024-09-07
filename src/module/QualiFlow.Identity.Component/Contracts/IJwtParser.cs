using System.Security.Claims;

namespace QualiFlow.Identity.Component.Contracts;

public interface IJwtParser
{
    IEnumerable<Claim> Parse(string jwt);
}
