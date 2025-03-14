using Microsoft.AspNetCore.Mvc;
using NutriSync.Application.DTOs;
using NutriSync.Core.Interfaces;

namespace NutriSync.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        var result = await _authService.RegisterAsync(registerDto.Email, registerDto.Password);
        if (!result) return BadRequest("Registration failed.");

        return Ok("User registered successfully.");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto model)
    {
        var token = await _authService.LoginAsync(model.Email, model.Password);
        if (token == null) return Unauthorized("Invalid credentials.");

        return Ok(new { Token = token });
    }
}
