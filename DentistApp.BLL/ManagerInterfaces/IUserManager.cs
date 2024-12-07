namespace DentistApp.BLL.ManagerInterfaces;

public interface IUserManager
{
    Task<string> RegisterUserAsync(string email, string password, string confirmPassword);
    Task<string?> LoginUserAsync(string email, string password);
}