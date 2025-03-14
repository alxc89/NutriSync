using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NutriSync.Core.Entities;
using NutriSync.Core.Enums;

namespace NutriSync.Infra.EntityTypeConfiguration;

public class PatientConfiguration : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        #region table name
        builder.ToTable("Patients");
        #endregion

        #region primay key
        builder.HasKey(p => p.Id);
        #endregion

        #region mapping properties
        builder.Property(p => p.Status)
            .HasDefaultValue(Status.PendingPayment)
            .IsRequired();

        builder.Property(p => p.Name)
            .HasColumnName("Name")
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.Phone)
            .HasColumnName("Phone")
            .HasMaxLength(50);

        builder.Property(p => p.Email)
            .HasColumnName("Email")
            .HasMaxLength(100);

        builder.Property(p => p.Document)
            .HasColumnName("Document")
            .HasMaxLength(50);

        builder.Property(p => p.BirthDate)
            .HasColumnName("BirthDate");

        builder.Property(p => p.Gender)
            .HasColumnName("Gender");

        builder.OwnsOne(n => n.Address, address =>
        {
            address.Property(a => a.Street).IsRequired().HasMaxLength(200);
            address.Property(a => a.City).IsRequired().HasMaxLength(100);
            address.Property(a => a.State).IsRequired().HasMaxLength(50);
            address.Property(a => a.ZipCode).IsRequired().HasMaxLength(20);
        });

        builder
            .HasOne(u => u.User)
            .WithOne(p => p.Patient)
            .OnDelete(DeleteBehavior.Cascade)
            .HasForeignKey<Patient>(p => p.UserId);

        builder.HasMany(p => p.MealPlans)
            .WithOne(p => p.Patient)
            .OnDelete(DeleteBehavior.Restrict)
            .HasForeignKey(p => p.PatientId);

        builder.HasMany(p => p.Appointments)
            .WithOne(p => p.Patient)
            .OnDelete(DeleteBehavior.Restrict)
            .HasForeignKey(p => p.PatientId);

        builder.HasMany(p => p.Measurements)
           .WithOne(p => p.Patient)
           .OnDelete(DeleteBehavior.Restrict)
           .HasForeignKey(p => p.PatientId);
        #endregion

        #region unique constraint
        builder.HasIndex(p => p.Document).IsUnique();
        #endregion

        #region check constraint
        builder.ToTable(p => p.HasCheckConstraint("CK_Patients_Document", "LENGTH(Document) > 0"));
        #endregion
    }
}
