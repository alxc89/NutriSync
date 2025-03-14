using Microsoft.OpenApi.Models;

namespace NutriSync.API.Documentation;

public static class SwaggerConfig
{
    public static void AddDocumentation(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(x =>
        {
            x.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "NutriSync",
                Description = "API da Aplicação NutriSync",
                Version = "1",
                Contact = new OpenApiContact
                {
                    Name = "Alex Cardoso",
                    Email = "alex.caardoso@hotmail.com",
                    Url = new Uri("https://github.com/alxc89")
                }
            });
        });
    }

    public static void AddConfigurationDevEnvironment(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.RoutePrefix = string.Empty;
            c.SwaggerEndpoint("./swagger/v1/swagger.json", "NutriSync v1");
        });
    }
}
