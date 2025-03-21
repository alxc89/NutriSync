using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using NutriSync.Core.Entities;

namespace NutriSync.Infra.EntityTypeConfiguration;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        #region table name
        builder.ToTable("Products");
        #endregion

        #region primay key
        builder.HasKey(p => p.Id);
        #endregion

        #region mapping properties
        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.Description)
            .IsRequired()
            .HasMaxLength(300);

        builder.Property(p => p.Price)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.HasMany(p => p.OrderItems)
            .WithOne()
            .HasForeignKey(oi => oi.ProductId)
            .OnDelete(DeleteBehavior.Restrict);
        #endregion

        #region unique constraint

        #endregion

        #region check constraint

        #endregion
    }
}
