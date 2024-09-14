using Blazored.LocalStorage;
using QualiFlow.Identity.Component.Contracts;

namespace QualiFlow.Identity.Component.Services;

internal class BlazorWasmJwtAccessor : IJwtAccessor
{
    private readonly ILocalStorageService _localStorageService;

    public BlazorWasmJwtAccessor(ILocalStorageService localStorageService)
    {
        _localStorageService = localStorageService;
    }

    public async ValueTask<string> ReadTokenAsync(string name)
    {
        return await _localStorageService.GetItemAsync<string>(name);
    }

    public async ValueTask WriteTokenAsync(string name, string token)
    {
        await _localStorageService.SetItemAsStringAsync(name, token);
    }
}
