using NutriSync.Core.Entities;
using NutriSync.Core.Interfaces.Repositories;

namespace NutriSync.Infra.Repositories;

public class OrderRepository : IOrderRepository
{
    public Task<Order> GetByIdAsync(Guid orderId)
    {
        throw new NotImplementedException();
    }

    public Task<Order> SaveAsync(Order order)
    {
        throw new NotImplementedException();
    }

    public Task<Order> UpdateAsync(Order order)
    {
        throw new NotImplementedException();
    }
}
