namespace NutriSync.Core.Interfaces;

public interface IAuthService
{
    Task<bool> CreateUserFromPatientAsync(string name, string email, string password);
    Task<Guid> CreateUserFromNutritionistAsync(string email, string password);
    Task<string?> LoginAsync(string email, string password);
}
