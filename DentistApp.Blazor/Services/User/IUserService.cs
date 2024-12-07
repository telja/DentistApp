using DentistApp.Core.DTOs;

namespace DentistApp.Blazor.Services.User;

public interface IUserService
{
    Task<string?> LoginAsync(LoginUserDTO loginModel);
}