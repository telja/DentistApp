namespace DentistApp.BLL.ManagerInterfaces;

public interface IUserManager
{
    Task<string> RegisterUserAsync(string email, string password, string confirmPassword);
}