using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;

namespace QualiFlow.Identity.Component.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddQualiFlowIdentityComponent(this IServiceCollection services)
    {
        return services;
    }
}
