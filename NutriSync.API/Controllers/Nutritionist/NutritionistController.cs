using Microsoft.AspNetCore.Mvc;
using NutriSync.Application.DTOs.Nutritionist;
using NutriSync.Application.Services.Nutritionist;

namespace NutriSync.API.Controllers.Nutritionist;

[ApiController]
[Route("api/[controller]")]
public class NutritionistController(INutritionistService nutritionistService) : ControllerBase
{
    private readonly INutritionistService _nutritionistService = nutritionistService;

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var result = await _nutritionistService.GetNutritionistByIdAsync(id);
        if (result.Data == null) return NotFound(result.Message);
        return Ok(result.Data);
    }

    [HttpPost("create")]
    public async Task<IActionResult> Post([FromBody] CreateNutritionistDto createNutritionistDto)
    {
        var result = await _nutritionistService.CreateNutritionistAsync(createNutritionistDto);
        if (result?.Data == null) return BadRequest($"Erro: {result?.Message}");

        return Ok();
    }
}
