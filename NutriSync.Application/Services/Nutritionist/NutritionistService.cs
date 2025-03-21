using Microsoft.Extensions.Logging;
using NutriSync.Application.DTOs.Nutritionist;
using NutriSync.Application.Mappers;
using NutriSync.Application.ModelViews.Nutritionist;
using NutriSync.Application.Shared;
using NutriSync.Core.Interfaces;
using NutriSync.Core.Interfaces.Repositories;

namespace NutriSync.Application.Services.Nutritionist;

public class NutritionistService(INutritionistRepository repository, IAuthService authService, ILogger<NutritionistService> logger) : INutritionistService
{
    private readonly INutritionistRepository _repository = repository;
    private readonly ILogger<NutritionistService> _logger = logger;
    private readonly IAuthService _authService = authService;

    public async Task<ServiceResponse<bool>> CreateNutritionistAsync(CreateNutritionistDto createNutritionistDto)
    {
        var existsNutritionist = await _repository.AnyNutricionistAsync(createNutritionistDto.Crn);
        if (existsNutritionist)
            return ServiceResponseHelper.Error<bool>(409, "CRN já cadastrado, verifique!");

        var userCreated = await _authService
                .CreateUserFromNutritionistAsync(createNutritionistDto.Email, createNutritionistDto.Password);
        if (userCreated == Guid.Empty)
            return ServiceResponseHelper.Error<bool>(500, "Erro ao salvar nutricionista!");

        var nutritionist = createNutritionistDto.ToEntity();

        try
        {
            var nutritionistCreated = await _repository.SaveAsync(nutritionist);
            if (!nutritionistCreated)
                return ServiceResponseHelper.Error<bool>(500, "Erro ao salvar nutricionista!");

            return ServiceResponseHelper.Success(201, "Cadastro efetuado com sucesso!", true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao criar nutricionista: {Message}", ex.Message);
            return ServiceResponseHelper.Error<bool>(500, "Erro interno no servidor.");
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
