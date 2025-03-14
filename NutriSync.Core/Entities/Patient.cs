using NutriSync.Core.Enums;
using NutriSync.Core.Interfaces;
using NutriSync.Core.ValueObject;

namespace NutriSync.Core.Entities;

public class Patient : Entity
{
    public Status Status { get; set; } = Status.Pending;
    public string Name { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Document { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public Gender Gender { get; set; }

    public Address Address { get; set; } = null!;

    public Guid UserId { get; set; } // Relaciona com a tabela de usuários
    public UserBase User { get; set; } = null!; // Relaciona com a tabela de usuários

    public ICollection<PatientMeasurement> Measurements { get; set; } = [];
    public ICollection<MealPlan> MealPlans { get; set; } = [];
    public ICollection<Appointment> Appointments { get; set; } = [];
}

