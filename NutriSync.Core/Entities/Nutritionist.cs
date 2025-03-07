namespace NutriSync.Core.Entities;

public class Nutritionist : Entity
{
    public Guid UserId { get; set; } // Relaciona com a tabela de usuários
    public User User { get; set; } = null!;

    public string Name { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Crn { get; set; } = string.Empty;

    public ICollection<MealPlan> MealPlans { get; set; } = new List<MealPlan>();
    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}

