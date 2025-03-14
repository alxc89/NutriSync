using Microsoft.AspNetCore.Identity;
using NutriSync.Core.Entities;

namespace NutriSync.Infra.Identity.Entities;

public class User : IdentityUser<Guid>
{
    public User(string userName, string userEmail)
    {
        Id = Guid.NewGuid();
        UserName = userName;
        Email = userEmail;
        //TemporaryPassword = GenerateTemporaryPassword();
        TemporaryPassword = "123@nutriSync";
    }

    public Nutritionist? Nutritionist { get; set; }
    public Patient? Patient { get; set; }
    public string TemporaryPassword { get; set; }

    protected string GenerateTemporaryPassword(int length = 12)
    {
        const string upperChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const string lowerChars = "abcdefghijklmnopqrstuvwxyz";
        const string numbers = "0123456789";
        const string specialChars = "!@#$%^&*()-_=+";

        var random = new Random();
        var passwordChars = new List<char>();

        // Garante pelo menos um caractere de cada tipo
        passwordChars.Add(upperChars[random.Next(upperChars.Length)]);
        passwordChars.Add(lowerChars[random.Next(lowerChars.Length)]);
        passwordChars.Add(numbers[random.Next(numbers.Length)]);
        passwordChars.Add(specialChars[random.Next(specialChars.Length)]);

        // Preenche o restante da senha com caracteres aleatórios misturados
        string allChars = upperChars + lowerChars + numbers + specialChars;
        for (int i = passwordChars.Count; i < length; i++)
        {
            passwordChars.Add(allChars[random.Next(allChars.Length)]);
        }

        // Embaralha a senha para garantir uma ordem aleatória
        return new string(passwordChars.OrderBy(x => random.Next()).ToArray());
    }
}
