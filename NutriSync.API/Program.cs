using NutriSync.API.Documentation;
using NutriSync.API.Extensions;
using NutriSync.Infra.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddCors();
builder.AddConfiguration();
builder.AddDbContext();
builder.AddDependecyInjectionConfiguration();
builder.AddDocumentation();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
    app.AddConfigurationDevEnvironment();


app.UseDataBaseConfiguration();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseCors(x =>
{
    x.AllowAnyOrigin();
    x.AllowAnyMethod();
    x.AllowAnyHeader();
});

app.Run();
