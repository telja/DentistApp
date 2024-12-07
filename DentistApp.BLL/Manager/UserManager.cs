using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DentistApp.DAL;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using DentistApp.BLL.ManagerInterfaces;
using DentistApp.DAL.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DentistApp.BLL.Manager;

public class UserManager : IUserManager
{
    private readonly IUserQueries _userQueries;
    private readonly UserManager<IdentityUser> _identityUserManager;
    private readonly string _jwtKey;
    private readonly string _jwtIssuer;
    public UserManager(IUserQueries userQueries, UserManager<IdentityUser> identityUserManager, IConfiguration configuration)
    {
        _userQueries = userQueries;
        _identityUserManager = identityUserManager;
        _jwtKey = configuration["Jwt:Key"] ?? throw new ArgumentNullException("Jwt:Key");
        _jwtIssuer = configuration["Jwt:Issuer"] ?? throw new ArgumentNullException("Jwt:Issuer");
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
    
    public async Task<string?> LoginUserAsync(string email, string password)
    {
        var user = await _identityUserManager.FindByEmailAsync(email);
        if (user == null || !await _identityUserManager.CheckPasswordAsync(user, password))
        {
            return null; // Vjerodajnice nisu točne
        }

        // Generiranje JWT tokena
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_jwtKey);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email)
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            Issuer = _jwtIssuer,
            Audience = _jwtIssuer,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}