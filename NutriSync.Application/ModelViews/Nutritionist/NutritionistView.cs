using NutriSync.Core.Enums;

namespace NutriSync.Application.ModelViews.Nutritionist;

public class NutritionistView
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string TenantId { get; set; } = string.Empty;
    public Status Status { get; set; } = Status.PendingPayment;
    public string Name { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Document { get; set; } = string.Empty;
    public string Crn { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
}
