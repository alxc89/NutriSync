using NutriSync.Core.Entities;
using NutriSync.Core.Interfaces.Repositories;
using NutriSync.Infra.Context;

namespace NutriSync.Infra.Repositories;

public class OrderRepository(DataContext dataContext) : IOrderRepository
{
    private readonly DataContext _context = dataContext;
    public Task<Order> GetByIdAsync(Guid orderId)
    {
        throw new NotImplementedException();
    }

    public async Task<Order> SaveAsync(Order order)
    {
        try
        {
            await _context
                .Orders
                .AddAsync(order);
            await _context.SaveChangesAsync();
            return order;
        }
        catch
        {
            throw new Exception("Erro interno!");
        }
    }

    public Task<Order> UpdateAsync(Order order)
    {
        throw new NotImplementedException();
    }
}
