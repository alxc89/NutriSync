using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NutriSync.Core.Entities;
using NutriSync.Core.Enums;

namespace NutriSync.Infra.EntityTypeConfiguration;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        #region table name
        builder.ToTable("OrderItems");
        #endregion

        #region primay key
        builder.HasKey(oi => new { oi.OrderId, oi.ProductId });
        #endregion

        #region mapping properties
        builder.Property(oi => oi.Description)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(oi => oi.Quantity)
            .IsRequired();

        builder.Property(oi => oi.UnitPrice)
            .IsRequired()
            .HasColumnType("decimal(18,2)");
        #endregion

        #region unique constraint

        #endregion

        #region check constraint

        #endregion
    }
}
