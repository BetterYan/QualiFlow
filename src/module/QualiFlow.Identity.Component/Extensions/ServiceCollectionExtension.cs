using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;
using QualiFlow.Identity.Component.Contracts;
using QualiFlow.Identity.Component.HttpMessageHandler;
using QualiFlow.Identity.Component.Services;

namespace QualiFlow.Identity.Component.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddQualiFlowIdentityComponent(this IServiceCollection services, string baseAddress)
    {
        services.AddHttpClient("normal");
        services.AddHttpClient("authed", (sp, client) => new HttpClient(new AuthenticatingApiHttpMessageHandler(sp.GetRequiredService<IJwtAccessor>()))
        {
            BaseAddress = new Uri(baseAddress)
        });

        services.AddMudServices();

        services.AddOptions()
                .AddAuthorizationCore()
                .AddScoped<AuthenticationStateProvider, AccessTokenAuthenticationStateProvider>()
                .AddBlazoredLocalStorage()
                .AddSingleton<IJwtParser, BlazorWasmJwtParser>()
                .AddScoped<IJwtAccessor, BlazorWasmJwtAccessor>();
        return services;
    }
}
