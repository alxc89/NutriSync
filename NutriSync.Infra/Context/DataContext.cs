using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NutriSync.Core.Entities;
using NutriSync.Infra.Identity.Entities;
using NutriSync.Infra.Identity.Interfaces;
using System.Linq.Expressions;
using System.Reflection;

namespace NutriSync.Infra.Context;

public class DataContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    private readonly ITenantProvider? _tenantProvider;
    private readonly string _tenantId;

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }

    public DataContext(DbContextOptions<DataContext> options, ITenantProvider tenantProvider) : base(options)
    {
        _tenantProvider = tenantProvider;
        if (_tenantProvider != null)
            _tenantId = _tenantProvider.GetTenantId() ?? string.Empty;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Aplica o filtro global a todas as entidades que implementam ITenantEntity
        foreach (var entityType in modelBuilder.Model.GetEntityTypes().Where(e => typeof(Entity).IsAssignableFrom(e.ClrType)))
        {
            if (typeof(Entity).IsAssignableFrom(entityType.ClrType))
            {
                modelBuilder.Entity(entityType.ClrType).HasQueryFilter(CreateTenantFilter(entityType.ClrType));
            }
        }
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }

    private LambdaExpression CreateTenantFilter(Type entityType)
    {
        var parameter = Expression.Parameter(entityType, "e");
        var property = entityType.GetProperty("TenantId");

        if (property == null || property.PropertyType != typeof(string))
            return Expression.Lambda(Expression.Constant(true), parameter); // Se a entidade não tiver TenantId, não aplica filtro

        var member = Expression.Property(parameter, property);
        var constant = Expression.Constant(_tenantId, typeof(string));
        var equalExpression = Expression.Equal(member, constant);

        return Expression.Lambda(equalExpression, parameter);
    }

    public DbSet<Patient> Patients { get; set; }
    public DbSet<Nutritionist> Nutritionists { get; set; }
    public DbSet<Order> Orders { get; set; }
}
