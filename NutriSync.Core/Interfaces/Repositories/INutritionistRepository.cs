using NutriSync.Core.Entities;

namespace NutriSync.Core.Interfaces.Repositories;

public interface INutritionistRepository
{
    Task<Nutritionist> SaveAsync(Nutritionist nutritionist);
    Task<Nutritionist?> GetNutricionistByIdAsync(Guid id);
    Task<bool> AnyNutricionistAsync(string crn);
    Task<Nutritionist?> UpdateAsync(Nutritionist nutritionist);
    Task DeleteAsync(long id);
}
