using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NutriSync.Core.Entities;
using NutriSync.Core.Enums;

namespace NutriSync.Infra.EntityTypeConfiguration;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        #region table name
        builder.ToTable("Orders");
        #endregion

        #region primay key
        builder.HasKey(p => p.Id);
        #endregion

        #region mapping properties
        builder.Property(o => o.NutritionistId)
            .IsRequired();

        builder.HasOne(o => o.Nutritionist)
            .WithMany()
            .HasForeignKey(o => o.NutritionistId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(o => o.PaidAt)
            .IsRequired(false);

        builder.Property(o => o.Status)
            .IsRequired()
            .HasDefaultValue(OrderStatus.Pending);

        builder.HasMany(o => o.Items)
            .WithOne(i => i.Order)
            .HasForeignKey(i => i.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
        #endregion

        #region unique constraint
        
        #endregion

        #region check constraint

        #endregion
    }
}
