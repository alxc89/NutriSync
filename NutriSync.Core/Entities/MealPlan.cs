namespace NutriSync.Core.Entities;

public class MealPlan : Entity
{   
    public Guid PatientId { get; set; }
    public Patient Patient { get; set; } = null!;
    public Guid NutritionistId { get; set; }
    public Nutritionist Nutritionist { get; set; } = null!;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public ICollection<MealPlanItem> Items { get; set; } = [];
}

