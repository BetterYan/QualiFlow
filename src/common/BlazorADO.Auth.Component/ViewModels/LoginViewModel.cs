using System.ComponentModel.DataAnnotations;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using BlazorADO.Auth.Shared.Models;
using BlazorADO.Auth.Shared.ViewModels;

namespace BlazorADO.Auth.Component.ViewModels;

public class LoginViewModel : ILoginViewModel
{
    [Required]
    [EmailAddress]
    public string EmailAddress { get; set; }
    [Required]
    public string Password { get; set; }
    public bool RememberMe { get; set; }

    private readonly HttpClient _httpClient;

    public LoginViewModel()
    {
    }

    public LoginViewModel(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<AuthenticationResponse> AuthenticateJWT()
    {
        //creating authentication request
        var authenticationRequest = new AuthenticationRequest
        {
            EmailAddress = EmailAddress,
            Password = Password,
        };

        //authenticating the request
        var httpMessageResponse = await _httpClient.PostAsJsonAsync("user/authenticatejwt", authenticationRequest);

        return await httpMessageResponse.Content.ReadFromJsonAsync<AuthenticationResponse>();
    }

    public async Task<User> GetUserByJWTAsync(string jwtToken)
    {
        try
        {
            //preparing the http request
            using var requestMessage = new HttpRequestMessage(HttpMethod.Post, "user/getuserbyjwt")
            {
                Content = new StringContent(jwtToken)
                {
                    Headers =
                        {
                            ContentType = new MediaTypeHeaderValue("application/json")
                        }
                }
            };

            //making the http request
            var response = await _httpClient.SendAsync(requestMessage);

            //returning the user if found
            var returnedUser = await response.Content.ReadFromJsonAsync<User>();
            if (returnedUser != null) return returnedUser;
            else return null;
        }
        catch (Exception)
        {
            return null;
        }
    }
}
