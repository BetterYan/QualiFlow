using Microsoft.Extensions.DependencyInjection;

namespace QualiFlow.Identity.API.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddQualiFlowIdentity(this IServiceCollection services)
    {
        return services;
    }
}
