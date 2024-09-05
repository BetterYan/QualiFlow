namespace BlazorADO.Auth.Component.Services;

public interface IAccessTokenService
{
    Task<string> GetAccessTokenAsync(string tokenName);
    Task SetAccessTokenAsync(string tokenName, string tokenValue);
    Task RemoveAccessTokenAsync(string tokenName);
}
