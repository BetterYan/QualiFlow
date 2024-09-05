namespace QualiFlow.Web.Client.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddQualiFlowServices(this IServiceCollection services)
    {
        services.AddOptions();

        return services;
    }
}
