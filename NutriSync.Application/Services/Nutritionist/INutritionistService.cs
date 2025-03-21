using NutriSync.Application.DTOs.Nutritionist;
using NutriSync.Application.ModelViews.Nutritionist;
using NutriSync.Application.Shared;

namespace NutriSync.Application.Services.Nutritionist;

public interface INutritionistService
{
    Task<ServiceResponse<bool>> CreateNutritionistAsync(CreateNutritionistDto createNutritionistDto);
    Task<ServiceResponse<NutritionistView>> GetNutritionistByIdAsync(Guid nutritionistId);
}
