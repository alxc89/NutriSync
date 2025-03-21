using Microsoft.AspNetCore.Mvc;
using NutriSync.Application.DTOs.Order;
using NutriSync.Application.Services.Order;

namespace NutriSync.API.Controllers.Oder;

[ApiController]
[Route("api/[controller]")]
public class OrderController(IOrderService orderService) : ControllerBase
{
    private readonly IOrderService _orderService = orderService;

    [HttpPost("paymentNotification")]
    public async Task<IActionResult> Post([FromBody] PaymentNotificationDto paymentNotificationDto)
    {
        try
        {
            await _orderService.ProcessPaymentWebhook(paymentNotificationDto);
        }
        catch
        {
            throw new Exception("Erro ao processar webhook de pagamento.");
        }
        return Ok();
    }
}
