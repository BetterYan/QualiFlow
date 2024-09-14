using Microsoft.Extensions.DependencyInjection;
using QualiFlow.Identity.API.Contracts;
using QualiFlow.Identity.API.Services;

namespace QualiFlow.Identity.API.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddQualiFlowIdentityApiServices(this IServiceCollection services)
    {
        services.AddSingleton<ISecretHasher, DefaultSecretHasher>();
        services.AddScoped<IAccessTokenIssuer, DefaultAccessTokenIssuer>();
        services.AddScoped<IUserCredentialsValidator, DefaultUserCredentialsValidator>();
        return services;
    }
}
