using Microsoft.EntityFrameworkCore;
using NutriSync.Infra.Context;

namespace NutriSync.API.Extensions;

public static class AppExtensions
{
    public static void UseDataBaseConfiguration(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
        using var context = serviceScope.ServiceProvider.GetRequiredService<DataContext>();
        context.Database.Migrate();
        context.Database.EnsureCreated();
    }
}
