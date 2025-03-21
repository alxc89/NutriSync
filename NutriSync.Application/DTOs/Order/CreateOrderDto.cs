namespace NutriSync.Application.DTOs.Order;

public class CreateOrderDto
{
    public Guid NutritionistId { get; set; } = Guid.Empty;
    public Guid ProductId { get; set; }
}
