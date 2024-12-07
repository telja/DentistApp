using System.Net.Http.Json;
using System.Threading.Tasks;
using DentistApp.Core;
using DentistApp.Core.DTOs;

namespace DentistApp.Blazor.Services.User.Impl;

public class UserService : IUserService
{
    private readonly HttpClient _httpClient;

    public UserService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string?> LoginAsync(LoginUserDTO loginModel)
    {
        var response = await _httpClient.PostAsJsonAsync("api/Account/login", loginModel);

        if (response.IsSuccessStatusCode)
        {
            var responseData = await response.Content.ReadFromJsonAsync<TokenResponse>();
            return responseData?.Token;
        }

        return null;
    }
}
