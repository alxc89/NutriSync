using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NutriSync.Core.Entities;
using NutriSync.Infra.Identity.Entities;
using System.Reflection;

namespace NutriSync.Infra.Context;

public class DataContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public DbSet<Patient> Patients { get; set; }
    public DbSet<Nutritionist> Nutritionists { get; set; }
}
