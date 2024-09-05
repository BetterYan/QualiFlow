using System.ComponentModel.DataAnnotations;
using BlazorADO.Auth.Shared.ViewModels;

namespace BlazorADO.Auth.Component.ViewModels;

public class RegisterViewModel : IRegisterViewModel
{
    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    public string EmailAddress { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 8)]
    public string Password { get; set; }

    public string ReenterPassword { get; set; }

    private readonly HttpClient _httpClient;

    public RegisterViewModel()
    {
    }
    public RegisterViewModel(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public Task RegisterUser()
    {
        throw new NotImplementedException();
    }
}
