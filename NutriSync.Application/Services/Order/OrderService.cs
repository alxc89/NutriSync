using Microsoft.Extensions.Logging;
using NutriSync.Application.DTOs.Order;
using NutriSync.Application.ModelViews.Nutritionist;
using NutriSync.Application.Services.Nutritionist;
using NutriSync.Application.Shared;
using NutriSync.Core.Entities;
using NutriSync.Core.Enums;
using NutriSync.Core.Interfaces;
using NutriSync.Core.Interfaces.Repositories;

namespace NutriSync.Application.Services.Order;

public class OrderService(IOrderRepository repository, IAuthService authService, ILogger<OrderService> logger) : IOrderService
{
    private readonly IOrderRepository _repository = repository;
    //private readonly IPaymentGateway _paymentGateway = paymentGateway;
    private readonly IAuthService _authService = authService;
    private readonly ILogger<OrderService> _logger = logger;

    public async Task<ServiceResponse<Guid>> CreateOrderAsync(Guid nutritionistId)
    {
        var order = new Core.Entities.Order(nutritionistId);
        order.AddItem(Guid.NewGuid(), "Licença de Uso", 1, 100.00m); // Exemplo de valor
        
        try
        {
            var orderCreated = await _repository.SaveAsync(order);
            // Gera link de pagamento no gateway
            //var paymentLink = await _paymentGateway.GeneratePaymentLinkAsync(order);
            return ServiceResponseHelper.Success(201, "Cadastro efetuado com sucesso!", orderCreated.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao criar a ordem de compra do Nutricionista: {Message}", ex.Message);
            return ServiceResponseHelper.Error<Guid>(500, "Erro interno no servidor.");
        }
    }

    public async Task ProcessPaymentWebhook(PaymentNotificationDto paymentDto)
    {
        var order = await _repository.GetByIdAsync(paymentDto.OrderId);
        if (order == null || order.Status != OrderStatus.Pending) return;

        order.MarkAsPaid();
        await _repository.UpdateAsync(order);
        // Após pagamento, criar usuário no sistema, implementar regra para gerar usuário com senha aleatória.
        // Implementar envio de e-mail com senha para o nutricionista
        await _authService.CreateUserFromNutritionistAsync(order.NutritionistId);
    }
}

