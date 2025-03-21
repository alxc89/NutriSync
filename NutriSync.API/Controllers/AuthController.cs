using Microsoft.AspNetCore.Mvc;
using NutriSync.Application.DTOs;
using NutriSync.Application.Mappers;
using NutriSync.Application.Services.Nutritionist;
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

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto model,
        [FromHeader(Name = "X-TenantId")] string tenantId)
    {
        if (string.IsNullOrEmpty(tenantId))
            return BadRequest("Tenant ID is required.");

        var token = await _authService.LoginAsync(model.Email, model.Password);
        if (token == null) return Unauthorized("Invalid credentials.");

        return Ok(new { Token = token });
    }
}
