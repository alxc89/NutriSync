using NutriSync.Core.Entities;

namespace NutriSync.Core.Interfaces.Repositories;

public interface IOrderRepository
{
    Task<Order> SaveAsync(Order order);
    Task<Order> GetByIdAsync(Guid orderId);
    Task<Order> UpdateAsync(Order order);
}
