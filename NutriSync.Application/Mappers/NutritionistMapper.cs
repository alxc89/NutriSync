using NutriSync.Application.DTOs;
using NutriSync.Application.DTOs.Nutritionist;
using NutriSync.Application.ModelViews.Nutritionist;
using NutriSync.Core.Entities;
using NutriSync.Core.ValueObject;
namespace NutriSync.Application.Mappers;

public static class NutritionistMapper
{
    public static Nutritionist ToEntity(this CreateNutritionistDto dto)
    {
        return new Nutritionist(
            dto.Name,
            dto.Phone,
            dto.Email,
            dto.Document,
            dto.Crn,
            dto.BirthDate,
            dto.Gender
        );
    }

    public static NutritionistView ToView(this Nutritionist nutritionist)
    {
        return new NutritionistView
        {
            Status = nutritionist.Status,
            Name = nutritionist.Name,
            Phone = nutritionist.Phone,
            Email = nutritionist.Email,
            Document = nutritionist.Document,
            Crn = nutritionist.Crn,
            BirthDate = nutritionist.BirthDate
        };
    }

    //public static void UpdateFromDTO(this Nutritionist nutritionist, UpdateNutritionistDto dto)
    //{
    //    nutritionist.Update(dto.Name, dto.Phone, dto.Email, dto.Crn,
    //        new Address(dto.Address.Street, dto.Address.Number, dto.Address.City, dto.Address.State, dto.Address.ZipCode));
    //}
}
