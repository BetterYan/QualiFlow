using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;
using QualiFlow.Identity.Component.Contracts;
using QualiFlow.Identity.Component.Services;

namespace QualiFlow.Identity.Component.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddQualiFlowIdentityComponent(this IServiceCollection services)
    {
        services.AddOptions()
                .AddAuthorizationCore()
                .AddScoped<AuthenticationStateProvider, AccessTokenAuthenticationStateProvider>()
                .AddBlazoredLocalStorage()
                .AddSingleton<IJwtParser, BlazorWasmJwtParser>()
                .AddScoped<IJwtAccessor, BlazorWasmJwtAccessor>();
        return services;
    }
}
