using Microsoft.AspNetCore.Components.Authorization;
using QualiFlow.Identity.Component.Contracts;
using QualiFlow.Identity.Core;
using System.Security.Claims;
using QualiFlow.Identity.Core.Extensions;

namespace QualiFlow.Identity.Component.Services;

public class AccessTokenAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly IJwtAccessor _jwtAccessor;
    private readonly IJwtParser _jwtParser;

    public AccessTokenAuthenticationStateProvider(IJwtAccessor jwtAccessor, IJwtParser jwtParser)
    {
        _jwtAccessor = jwtAccessor;
        _jwtParser = jwtParser;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var authToken = await _jwtAccessor.ReadTokenAsync(TokenNames.AccessToken);

        if (string.IsNullOrEmpty(authToken))
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

        var claims = _jwtParser.Parse(authToken).ToList();
        var isExpired = claims.IsExpired();

        if (isExpired)
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

        var identity = new ClaimsIdentity(claims, "jwt");
        var user = new ClaimsPrincipal(identity);

        return new AuthenticationState(user);
    }

    public void NotifyAuthenticationStateChanged()
    {
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
}
