using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using BlazorADO.Auth.Shared.ViewModels;
using BlazorADO.Auth.Shared.Models;

namespace BlazorADO.Auth.Component;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly ILoginViewModel _loginViewModel;
    private readonly BlazorADO.Auth.Component.Services.IAccessTokenService _accessTokenService;

    public CustomAuthenticationStateProvider(BlazorADO.Auth.Component.Services.IAccessTokenService accessTokenService)
    {
        this._accessTokenService = accessTokenService;
    }

    public async override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        User currentUser = await GetUserByJWTAsync();
        if (currentUser != null && currentUser.EmailAddress != null)
        {
            var claimsPrincipal = GetClaimsPrinciple(currentUser);
            return new AuthenticationState(claimsPrincipal);
        }
        else
        {
            await _accessTokenService.RemoveAccessTokenAsync("jwt_token");
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }
    }

    public async Task MarkUserAsAuthenticated()
    {
        var user = await GetUserByJWTAsync();
        var claimsPrincipal = GetClaimsPrinciple(user);
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
    }

    public async Task<User> GetUserByJWTAsync()
    {
        var jwtToken = await _accessTokenService.GetAccessTokenAsync("jwt_token");
        if (jwtToken == null) return null;

        jwtToken = $@"""{jwtToken}""";
        return await _loginViewModel.GetUserByJWTAsync(jwtToken);
    }

    private ClaimsPrincipal GetClaimsPrinciple(User currentUser)
    {
        //create a claims
        var claimEmailAddress = new Claim(ClaimTypes.Name, currentUser.EmailAddress);
        var claimNameIdentifier = new Claim(ClaimTypes.NameIdentifier, Convert.ToString(currentUser.UserId));
        var claimRole = new Claim(ClaimTypes.Role, currentUser.Role == null ? "" : currentUser.Role);

        //create claimsIdentity
        var claimsIdentity = new ClaimsIdentity(new[] { claimEmailAddress, claimNameIdentifier, claimRole }, "serverAuth");
        //create claimsPrincipal
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

        return claimsPrincipal;
    }
}
