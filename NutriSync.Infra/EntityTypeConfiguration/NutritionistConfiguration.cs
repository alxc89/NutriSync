using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NutriSync.Core.Entities;
using NutriSync.Core.Enums;

namespace NutriSync.Infra.EntityTypeConfiguration;

public class NutritionistConfiguration : IEntityTypeConfiguration<Nutritionist>
{
    public void Configure(EntityTypeBuilder<Nutritionist> builder)
    {
        #region table name
        builder.ToTable("Nutritionists");
        #endregion

        #region primay key
        builder.HasKey(p => p.Id);
        #endregion

        #region mapping properties
        builder.Property(n => n.Status)
            .HasDefaultValue(Status.PendingPayment)
            .IsRequired();

        builder.Property(n => n.Name)
                .IsRequired()
                .HasMaxLength(100);

        builder.Property(n => n.Phone)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(n => n.Email)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(n => n.Document)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(n => n.Crn)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(n => n.BirthDate)
            .HasColumnName("BirthDate");

        builder.Property(n => n.Gender)
            .HasColumnName("Gender");

        builder.OwnsOne(n => n.Address, address =>
        {
            address.Property(a => a.Street).IsRequired().HasMaxLength(200);
            address.Property(a => a.City).IsRequired().HasMaxLength(100);
            address.Property(a => a.State).IsRequired().HasMaxLength(50);
            address.Property(a => a.ZipCode).IsRequired().HasMaxLength(20);
        });

        builder.HasOne(n => n.User)
                .WithOne(n => n.Nutritionist)
                .HasForeignKey<Nutritionist>(n => n.UserId)
                .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(n => n.MealPlans)
            .WithOne(m => m.Nutritionist)
            .HasForeignKey(m => m.NutritionistId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(n => n.Appointments)
            .WithOne(a => a.Nutritionist)
            .HasForeignKey(a => a.NutritionistId)
            .OnDelete(DeleteBehavior.Restrict);
        #endregion

        #region unique constraint
        builder.HasIndex(n => n.Crn).IsUnique();
        #endregion

        #region check constraint

        #endregion
    }
}
