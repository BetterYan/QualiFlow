using BlazorADO.Auth.Component.Extensions;

namespace QualiFlow.Web.Client.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddQualiFlowServices(this IServiceCollection services)
    {
        services.AddOptions();
        services.AddBlazorAuthServices();

        return services;
    }
}
