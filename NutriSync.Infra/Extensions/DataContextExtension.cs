using Microsoft.EntityFrameworkCore;
using NutriSync.Infra.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace NutriSync.Infra.Extensions;

public static class DataContextExtension
{
    public static void AddDbContext(this WebApplicationBuilder builder)
    {
        builder
            .Services
            .AddDbContext<DataContext>(options =>
            {
                options.UseNpgsql(Configuration.ConnectionString,
                    b => b.MigrationsAssembly("NutriSync.Infra"));
            });
    }
}
