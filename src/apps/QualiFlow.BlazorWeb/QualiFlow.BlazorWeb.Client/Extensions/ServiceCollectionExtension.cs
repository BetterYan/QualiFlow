using QualiFlow.Identity.Component.Extensions;

namespace QualiFlow.BlazorWeb.Client.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddQualiFlowServices(this IServiceCollection services)
    {
        services.AddOptions();
        return services;
    }
}
