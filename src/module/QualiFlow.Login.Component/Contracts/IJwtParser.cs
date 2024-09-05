using System.Security.Claims;

namespace QualiFlow.Login.Component.Contracts;

public interface IJwtParser
{
    IEnumerable<Claim> Parse(string jwt);
}
