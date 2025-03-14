namespace NutriSync.Core.Interfaces;

public interface IAuthService
{
    Task<bool> RegisterAsync(string email, string password);
    Task<bool> CreateUserFromNutritionistAsync(Guid id);
    Task<string?> LoginAsync(string email, string password);
}
