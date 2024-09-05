using BlazorADO.Auth.Shared.Models;

namespace BlazorADO.Auth.Shared.ViewModels;

public interface ILoginViewModel
{
    public string EmailAddress { get; set; }
    public string Password { get; set; }
    public bool RememberMe { get; set; }

    Task<AuthenticationResponse> AuthenticateJWT();
    public Task<User> GetUserByJWTAsync(string jwtToken);
}