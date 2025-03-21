namespace NutriSync.Core.Entities;

public class OrderItem : Entity
{
    public Guid OrderId { get; private set; } = Guid.Empty;
    public Guid ProductId { get; private set; } = Guid.Empty;
    public string Description { get; private set; } = string.Empty;
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }
    public decimal TotalPrice => Quantity * UnitPrice;

    public Order Order { get; set; } = null!;

    private OrderItem() { }

    public OrderItem(Guid orderId, Guid productId, string description, int quantity, decimal unitPrice)
    {
        OrderId = orderId;
        ProductId = productId;
        Description = description;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }
}