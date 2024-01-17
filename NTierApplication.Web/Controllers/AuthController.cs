using Microsoft.AspNetCore.Mvc;
using NTierApplication.Service.ViewModels;
using NTierApplication.Service;

namespace NTierApplication.Web.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private IUserService _service;

    public AuthController(IUserService userService)
    {
        _service = userService;
    }
    [HttpPost("register")]
    public IActionResult Register(UserViewModel userViewModel)
    {
        var result = _service.Register(userViewModel);
        return Ok(new { result });
    }
    [HttpPost("login")]
    public IActionResult Login(LoginViewModel loginViewModel)
    {
        var result = _service.Login(loginViewModel);
        return Ok(new { result.access_token, result.refresh_token, result.token_type, result.expires } );
    }
}
