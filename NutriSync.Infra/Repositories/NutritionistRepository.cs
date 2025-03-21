﻿using Microsoft.EntityFrameworkCore;
using NutriSync.Core.Entities;
using NutriSync.Core.Interfaces.Repositories;
using NutriSync.Infra.Context;

namespace NutriSync.Infra.Repositories;

public class NutritionistRepository(DataContext context) : INutritionistRepository
{
    private readonly DataContext _context = context;

    public async Task<bool> SaveAsync(Nutritionist nutritionist)
    {
        try
        {
            await _context
                .Nutritionists
                .AddAsync(nutritionist);
            await _context.SaveChangesAsync();
            return true;
        }
        catch
        {
            throw new Exception("Erro interno!");
        }
    }

    public Task<Nutritionist?> UpdateAsync(Nutritionist nutritionist)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> AnyNutricionistAsync(string crn)
    {
        try
        {
            return await _context
                .Nutritionists
                .AnyAsync(n => n.Crn.Equals(crn));
        }
        catch (Exception)
        {
            throw new Exception("Erro interno!");
        }
    }

    public Task DeleteAsync(long id)
    {
        throw new NotImplementedException();
    }

    public async Task<Nutritionist?> GetNutricionistByIdAsync(Guid id)
    {
        try
        {
            var nutritionist = await _context
                 .Nutritionists
                 .FirstOrDefaultAsync(n => n.Id.Equals(id));

            return nutritionist;
        }
        catch
        {
            throw new Exception("Erro interno!");
        }
    }
}
