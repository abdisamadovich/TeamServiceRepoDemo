using Microsoft.AspNetCore.Mvc;
using NTierApplication.Service.ViewModels;
using NTierApplication.Service;

namespace NTierApplication.Web.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IUserService UserService;

    public AuthController(IUserService userService)
    {
        UserService = userService;
    }

    [HttpPost("register")]
    public UserViewModel CreateNew(UserViewModel userViewModel)
    {
        userViewModel.Password = BCrypt.Net.BCrypt.HashPassword(userViewModel.Password);
        UserService.CreateNew(userViewModel);
        return userViewModel;
    }

    [HttpPost("login")]
    public IActionResult LogIn(LoginViewModel loginViewModel)
    {
        var res = UserService.Login(loginViewModel);

        bool check = false;

        foreach (var user in res)
        {
            if (BCrypt.Net.BCrypt.Verify(loginViewModel.Password, user.Password))
                check = true;
        }


        if (check)
            return Ok(loginViewModel);

        return BadRequest();
    }
}
