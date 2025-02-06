using Microsoft.AspNetCore.Mvc;
using NMS.WebAPI.Dtos;
using Service.Contract;

namespace NMS.WebAPI.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    public AuthController(IUserService userService) { _userService = userService; }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] LoginDto loginDto)
    {
        await _userService.RegisterAsync(loginDto.Username, loginDto.Password);
        return Ok("User registered successfully");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var token = await _userService.AuthenticateAsync(loginDto.Username, loginDto.Password);
        return Ok(new { Token = token });
    }
}
