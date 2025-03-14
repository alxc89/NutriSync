using NutriSync.Application.DTOs.Nutritionist;
using NutriSync.Application.ModelViews.Nutritionist;
using NutriSync.Core.Entities;
using NutriSync.Core.ValueObject;
using System.IO;
using System.Reflection.Emit;
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
            dto.Gender,
            new Address(dto.Address.Street, dto.Address.City, dto.Address.State, dto.Address.ZipCode)
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
