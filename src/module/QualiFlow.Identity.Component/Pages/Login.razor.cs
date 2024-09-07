using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using QualiFlow.Identity.Component.Contracts;
using QualiFlow.Identity.Component.Pages.Models;

namespace QualiFlow.Identity.Component.Pages;

[AllowAnonymous]
public partial class Login
{
    readonly LoginModel _loginModel = new LoginModel();

    [Inject] NavigationManager navigationManager { get; set; }
    [Inject] AuthenticationStateProvider AuthenticationStateProvider { get; set; }
    [Inject] ICredentialsValidator CredentialsValidator { get; set; }
    [Inject] IJwtAccessor JwtAccessor { get; set; }
    [Inject] ISnackbar Snackbar { get; set; }

    public Task TryLogin()
    {
        return Task.CompletedTask;
    }
}