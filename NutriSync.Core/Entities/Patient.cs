using NutriSync.Core.Enums;

namespace NutriSync.Core.Entities;

public class Patient : Entity
{
    public Guid UserId { get; set; } // Relaciona com a tabela de usuários
    public User User { get; set; } = null!;

    public string Name { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public Gender Gender { get; set; }

    public ICollection<PatientMeasurement> Measurements { get; set; } = new List<PatientMeasurement>();
    public ICollection<MealPlan> MealPlans { get; set; } = new List<MealPlan>();
    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}

