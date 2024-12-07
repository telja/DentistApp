using DentistApp.BLL.ManagerInterfaces;
using DentistApp.Core.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace DentistApp.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IUserManager _userManager;

    public AccountController(IUserManager userManager)
    {
        _userManager = userManager;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserDTO model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _userManager.RegisterUserAsync(model.Email, model.Password, model.ConfirmPassword);

        if (result == "User registered successfully.")
            return Ok(result);

        return BadRequest(result);
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserDTO model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var token = await _userManager.LoginUserAsync(model.Email, model.Password);

        if (token == null)
            return Unauthorized("Invalid email or password.");

        return Ok(new { Token = token });
    }
}