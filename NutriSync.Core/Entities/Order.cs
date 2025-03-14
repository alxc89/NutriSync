using NutriSync.Core.Enums;

namespace NutriSync.Core.Entities;

public class Order : Entity
{
    public Guid NutritionistId { get; private set; } = Guid.Empty;
    public Nutritionist Nutritionist { get; private set; } = null!;
    public DateTime? PaidAt { get; private set; } = null;
    public decimal TotalAmount => CalculateTotal();
    public OrderStatus Status { get; private set; } = OrderStatus.Pending;
    public ICollection<OrderItem> Items { get; private set; } = [];

    private Order() { }

    public Order(Guid nutritionistId)
    {
        NutritionistId = nutritionistId;
        CreatedAt = DateTime.UtcNow;
        Status = OrderStatus.Pending;
    }

    public void AddItem(Guid productId, string description, int quantity, decimal unitPrice)
    {
        if (quantity <= 0 || unitPrice <= 0)
            throw new InvalidOperationException("Quantidade e preço devem ser maiores que zero.");

        var item = new OrderItem(Id, productId, description, quantity, unitPrice);
        Items.Add(item);
    }

    public void MarkAsPaid()
    {
        if (Status != OrderStatus.Pending)
            throw new InvalidOperationException("Somente pedidos pendentes podem ser pagos.");

        Status = OrderStatus.Paid;
        PaidAt = DateTime.UtcNow;
    }

    public void Cancel()
    {
        if (Status == OrderStatus.Paid)
            throw new InvalidOperationException("Não é possível cancelar um pedido já pago.");

        Status = OrderStatus.Canceled;
    }

    private decimal CalculateTotal()
    {
        decimal total = 0;
        foreach (var item in Items)
        {
            total += item.TotalPrice;
        }
        return total;
    }
}
