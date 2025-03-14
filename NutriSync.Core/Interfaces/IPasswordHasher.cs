namespace NutriSync.Core.Interfaces;

public interface IPasswordHasher
{
    string Hash(string password);
    bool VeriFy(string hash, string password);
}
