using Microsoft.JSInterop;

namespace BlazorADO.Auth.Component.Services;

public class WebAppAccessTokenService : IAccessTokenService
{
    private readonly IJSRuntime _jsRuntime;

    public WebAppAccessTokenService(IJSRuntime jSRuntime)
    {
        this._jsRuntime = jSRuntime;
    }

    public async Task<string> GetAccessTokenAsync(string tokenName)
    {
        return await _jsRuntime.InvokeAsync<string>("localStorage.getItem", tokenName);
    }

    public async Task RemoveAccessTokenAsync(string tokenName)
    {
        await _jsRuntime.InvokeAsync<string>("localStorage.removeItem", tokenName);
    }

    public async Task SetAccessTokenAsync(string tokenName, string tokenValue)
    {
        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", tokenName, tokenValue);
    }
}
