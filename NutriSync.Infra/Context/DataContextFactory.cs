using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace NutriSync.Infra.Context;

public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
{
    public DataContext CreateDbContext(string[] args)
    {
        // Carrega o arquivo de configuração
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<DataContext>();

        // Obtém a connection string do appsettings.json
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        optionsBuilder.UseNpgsql(connectionString);

        // Retorna um DataContext sem TenantProvider para ser usado no design-time
        return new DataContext(optionsBuilder.Options);
    }
}
