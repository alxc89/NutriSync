using Microsoft.Extensions.Logging;
using NutriSync.Application.DTOs.Nutritionist;
using NutriSync.Application.Mappers;
using NutriSync.Application.ModelViews.Nutritionist;
using NutriSync.Application.Services.Order;
using NutriSync.Application.Shared;
using NutriSync.Core.Interfaces.Repositories;

namespace NutriSync.Application.Services.Nutritionist;

public class NutritionistService(INutritionistRepository repository, IOrderService orderService, ILogger<NutritionistService> logger) : INutritionistService
{
    private readonly INutritionistRepository _repository = repository;
    private readonly ILogger<NutritionistService> _logger = logger;
    private readonly IOrderService _orderService = orderService;

    public async Task<ServiceResponse<NutritionistView>> CreateNutritionistAsync(CreateNutritionistDto createNutritionistDto)
    {
        var existsNutritionist = await _repository.AnyNutricionistAsync(createNutritionistDto.Crn);
        if (existsNutritionist)
            return ServiceResponseHelper.Error<NutritionistView>(409, "CRN já cadastrado, verifique!");

        var nutritionist = createNutritionistDto.ToEntity();

        try
        {
            var nutritionistCreated = await _repository.SaveAsync(nutritionist);

            // Criar uma ordem de assinatura para o nutricionista
            var idOrder = await _orderService.CreateOrderAsync(nutritionistCreated.Id);
            NutritionistView nutritionistView = nutritionistCreated.ToView();

            return ServiceResponseHelper.Success(201, "Cadastro efetuado com sucesso!", nutritionistView);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao criar nutricionista: {Message}", ex.Message);
            return ServiceResponseHelper.Error<NutritionistView>(500, "Erro interno no servidor.");
        }
    }

    public async Task<ServiceResponse<NutritionistView>> GetNutritionistByIdAsync(Guid nutritionistId)
    {
        try
        {
            var nutritionist = await _repository.GetNutricionistByIdAsync(nutritionistId);
            if (nutritionist == null)
                return ServiceResponseHelper.Error<NutritionistView>(409, "Nutricionista não encontrado!");

            return ServiceResponseHelper.Success(201, "Cadastro efetuado com sucesso!", nutritionist.ToView());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao criar nutricionista: {Message}", ex.Message);
            return ServiceResponseHelper.Error<NutritionistView>(500, "Erro interno no servidor.");
        }
    }
}
