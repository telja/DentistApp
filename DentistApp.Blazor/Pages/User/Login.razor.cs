using DentistApp.Blazor.Services.User;
using DentistApp.Core.DTOs;
using Microsoft.AspNetCore.Components;

namespace DentistApp.Blazor.Pages.User;

public class LoginBase : ComponentBase
{
    [Inject]
    protected IUserService UserService { get; set; }

    protected LoginUserDTO loginModel = new LoginUserDTO();

    protected async Task HandleLogin()
    {
        var token = await UserService.LoginAsync(loginModel);

        if (!string.IsNullOrEmpty(token))
        {
            Console.WriteLine("Login successful!");
            // Možeš sačuvati token u localStorage ili koristiti za autorizaciju
        }
        else
        {
            Console.WriteLine("Login failed.");
        }
    }
}