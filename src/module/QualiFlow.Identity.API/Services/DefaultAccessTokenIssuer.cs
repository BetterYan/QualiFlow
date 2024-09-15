using Microsoft.Extensions.Options;
using QualiFlow.Identity.API.Contracts;
using QualiFlow.Identity.API.Models;
using QualiFlow.Identity.API.Utility;
using QualiFlow.Identity.Core.Entities;
using QualiFlow.Identity.Core.Models;
using System.Security.Claims;

namespace QualiFlow.Identity.API.Services;

public class DefaultAccessTokenIssuer : IAccessTokenIssuer
{
    private readonly JwtCreationOptions _jwtCreationOptions;

    public DefaultAccessTokenIssuer(IOptions<JwtCreationOptions> options)
    {
        _jwtCreationOptions = options.Value;
    }

    public IssuedTokens IssueTokens(User user)
    {
        var nameClaim = new Claim("Name", user.Name);
        var claims = new List<Claim> { nameClaim };
        _jwtCreationOptions.User = new UserPrivileges { Claims = claims };
        _jwtCreationOptions.ExpireAt = DateTime.UtcNow.AddHours(2);
        var accessToken = JwtBearer.CreateToken(_jwtCreationOptions);
        var refreshToken = JwtBearer.CreateToken(_jwtCreationOptions);
        return new IssuedTokens(accessToken, refreshToken);
    }
}
