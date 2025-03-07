using NutriSync.Core.Enums;

namespace NutriSync.Core.Entities;

public class MealPlanItem : Entity
{
    public Guid MealPlanId { get; set; }
    public MealPlan MealPlan { get; set; } = null!;
    public MealTime MealTime { get; set; }
    public string FoodItem { get; set; } = string.Empty;
    public string PortionSize { get; set; } = string.Empty;
}
