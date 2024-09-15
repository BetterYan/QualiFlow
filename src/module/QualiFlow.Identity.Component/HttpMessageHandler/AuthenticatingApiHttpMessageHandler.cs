using QualiFlow.Identity.Component.Contracts;
using QualiFlow.Identity.Core;
using QualiFlow.Identity.Core.Models;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace QualiFlow.Identity.Component.HttpMessageHandler;

public class AuthenticatingApiHttpMessageHandler : DelegatingHandler
{
    private readonly IJwtAccessor jwtAccessor;

    public AuthenticatingApiHttpMessageHandler(IJwtAccessor jwtAccessor)
    {
        this.jwtAccessor = jwtAccessor;
    }

    protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var accessToken = await jwtAccessor.ReadTokenAsync(TokenNames.AccessToken);

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        var response = await base.SendAsync(request, cancellationToken);

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            // Refresh token and retry once.
            var tokens = await RefreshTokenAsync(jwtAccessor, cancellationToken);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", tokens.AccessToken);

            // Retry.
            response = await base.SendAsync(request, cancellationToken);
        }

        return response;
    }

    private async Task<LoginResponse> RefreshTokenAsync(IJwtAccessor jwtAccessor, CancellationToken cancellationToken)
    {
        // Get refresh token.
        var refreshToken = await jwtAccessor.ReadTokenAsync(TokenNames.RefreshToken);

        // Setup request to get new tokens.
        var url = "api/v1/Identity/RefreshToken";
        var refreshRequestMessage = new HttpRequestMessage(HttpMethod.Post, url);
        refreshRequestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", refreshToken);

        // Send request.
        var response = await base.SendAsync(refreshRequestMessage, cancellationToken);

        // If the refresh token is invalid, we can't do anything.
        if (response.StatusCode == HttpStatusCode.Unauthorized)
            return new LoginResponse(false, null, null);

        // Parse response into tokens.
        var tokens = (await response.Content.ReadFromJsonAsync<LoginResponse>(cancellationToken: cancellationToken))!;

        // Store tokens.
        await jwtAccessor.WriteTokenAsync(TokenNames.RefreshToken, tokens.RefreshToken!);
        await jwtAccessor.WriteTokenAsync(TokenNames.AccessToken, tokens.AccessToken!);

        // Return tokens.
        return tokens;
    }
}
