using NutriSync.Infra;

namespace NutriSync.API.Extensions;

public static class ConfigurationExtensions
{
    public static void AddConfiguration(this WebApplicationBuilder builder)
    {
        var configuration = builder.Configuration;
        Configuration.ConnectionString = Environment.GetEnvironmentVariable("DB_CONNECTION")
            ?? configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
    }
}
