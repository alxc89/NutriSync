namespace NutriSync.Application.DTOs.Order;

public class PaymentNotificationDto
{
    public Guid OrderId { get; set; } = Guid.Empty;
}
