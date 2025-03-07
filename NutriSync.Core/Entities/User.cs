using NutriSync.Core.Enums;

namespace NutriSync.Core.Entities;

public class User : Entity
{
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty; // Armazene hash da senha, nunca em texto puro
    public UserRole Role { get; set; } // Define se é Nutricionista ou Paciente

    public Guid? NutritionistId { get; set; }
    public Nutritionist? Nutritionist { get; set; }

    public Guid? PatientId { get; set; }
    public Patient? Patient { get; set; }
}

