using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NutriSync.Core.Interfaces;
using NutriSync.Infra.Identity.Entities;
using NutriSync.Infra.Security.Settings;

namespace NutriSync.Infra.Identity.Auth;

public class AuthService(UserManager<User> userManager, RoleManager<IdentityRole<Guid>> roleManager,
    IOptions<JwtSettings> jwtSettings) : IAuthService
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly JwtSettings _jwtSettings = jwtSettings.Value;
    private readonly RoleManager<IdentityRole<Guid>> _roleManager = roleManager;

    /// <summary>
    /// Registrar usuário para pacientes
    /// </summary>
    /// <param name="name"></param>
    /// <param name="email"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public async Task<bool> CreateUserFromPatientAsync(string name, string email, string tenantId)
    {
        var user = new User(email, tenantId);
        var result = await _userManager.CreateAsync(user, user.TemporaryPassword);

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

        // Obtendo o TenantId (depende de como está armazenado)
        var tenantId = email; // Supondo que seja uma propriedade da sua entidade User

        var claims = new List<Claim>
        {
            new("TenantId", tenantId), // Incluindo TenantId no token
            new("Email", user.Email!)   // Incluindo o e-mail no token
        };

        // Adicionando roles ao token
        claims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims, // Incluindo as claims no token
            expires: DateTime.UtcNow.AddHours(_jwtSettings.ExpirationHours),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<Guid> CreateUserFromNutritionistAsync(string email, string password)
    {
        var user = new User(
            email
        );

        var result = await _userManager.CreateAsync(user, password);
        if (!result.Succeeded)
            throw new ApplicationException(string.Join(", ", result.Errors.Select(x => x.Description)));

        // Garantir que a role "Nutritionist" existe
        if (!await _roleManager.RoleExistsAsync("Nutritionist"))
            await _roleManager.CreateAsync(new IdentityRole<Guid>("Nutritionist"));

        // Atribuir a role ao usuário
        await _userManager.AddToRoleAsync(user, "Nutritionist");
        return user.Id;
    }
}