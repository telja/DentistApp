using DentistApp.DAL;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using DentistApp.BLL.ManagerInterfaces;
using DentistApp.DAL.Repository;

namespace DentistApp.BLL.Manager;

public class UserManager : IUserManager
{
    private readonly IUserQueries _userQueries;
    private readonly UserManager<IdentityUser> _identityUserManager;

    public UserManager(IUserQueries userQueries, UserManager<IdentityUser> identityUserManager)
    {
        _userQueries = userQueries;
        _identityUserManager = identityUserManager;
    }

    public async Task<string> RegisterUserAsync(string email, string password, string confirmPassword)
    {
        if (password != confirmPassword)
            return "Passwords do not match.";

        if (await _userQueries.IsEmailTakenAsync(email))
            return "Email is already taken.";

        var user = new IdentityUser
        {
            UserName = email,
            Email = email
        };

        var result = await _identityUserManager.CreateAsync(user, password);

        if (result.Succeeded)
            return "User registered successfully.";

        return string.Join(", ", result.Errors.Select(e => e.Description));
    }
}