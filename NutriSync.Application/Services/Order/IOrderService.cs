using NutriSync.Application.DTOs.Order;
using NutriSync.Application.Shared;

namespace NutriSync.Application.Services.Order;

public interface IOrderService
{
    Task<ServiceResponse<Guid>> CreateOrderAsync(Guid nutritionistId);
    Task ProcessPaymentWebhook(PaymentNotificationDto paymentDto);
}
