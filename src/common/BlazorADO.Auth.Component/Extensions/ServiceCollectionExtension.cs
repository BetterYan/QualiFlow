using BlazorADO.Auth.Component.ViewModels;
using BlazorADO.Auth.Shared.ViewModels;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorADO.Auth.Component.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddBlazorAuthServices(this IServiceCollection services)
    {
        // authetication & authorization
        services.AddAuthorizationCore();
        services.AddCascadingAuthenticationState();
        services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
        services.AddScoped<Services.IAccessTokenService, Services.WebAppAccessTokenService>();

        // authentication http clients
        services.AddHttpClient<ILoginViewModel, LoginViewModel>("LoginViewModelClient");
        services.AddHttpClient<IRegisterViewModel, RegisterViewModel>("RegisterViewModelClient");

        return services;
    }
}
