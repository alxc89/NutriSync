using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NutriSync.Core.Interfaces;
using NutriSync.Core.Interfaces.Repositories;
using NutriSync.Infra.Identity.Entities;
using NutriSync.Infra.Security.Settings;

namespace NutriSync.Infra.Identity.Auth;

public class AuthService(UserManager<User> userManager, INutritionistRepository nutritionistRepository, IOptions<JwtSettings> jwtSettings) : IAuthService
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly JwtSettings _jwtSettings = jwtSettings.Value;
    private readonly INutritionistRepository _repository = nutritionistRepository;

    public async Task<bool> RegisterAsync(string email, string password)
    {
        var user = new IdentityUser { UserName = email, Email = email };
        var result = await _userManager.CreateAsync(user, password);

        if (result.Succeeded)
            //await _userManager.AddToRoleAsync(user);
            return true;

        return false;
    }

    public async Task<string?> LoginAsync(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null || !await _userManager.CheckPasswordAsync(user, password))
            return null;

        var userRoles = await _userManager.GetRolesAsync(user);

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id),
            new(ClaimTypes.Email, user.Email!),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        if (userRoles.Any())
            claims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            _jwtSettings.Issuer,
            _jwtSettings.Audience,
            claims,
            expires: DateTime.UtcNow.AddHours(_jwtSettings.ExpirationHours),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<bool> CreateUserFromNutritionistAsync(Guid id)
    {
        var nutritionist = await _repository.GetNutricionistByIdAsync(id);
        if (nutritionist == null)
            return false;

        var user = new User(
            nutritionist.Name,
            nutritionist.Email
        );

        await _userManager.CreateAsync(user, user.TemporaryPassword);
        // Atualiza o ID do usuário no nutricionista
        nutritionist.SetUserId(user.Id);
        await _repository.UpdateAsync(nutritionist);

        // Enviar e-mail com a senha temporária
        //await _emailService.SendTemporaryPasswordAsync(nutritionist.Email, user.TemporaryPassword);
        return true;
    }
}