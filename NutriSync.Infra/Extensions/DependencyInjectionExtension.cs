using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using NutriSync.Application.Services.Nutritionist;
using NutriSync.Application.Services.Order;
using NutriSync.Application.Services.Patient;
using NutriSync.Core.Interfaces;
using NutriSync.Core.Interfaces.Repositories;
using NutriSync.Infra.Context;
using NutriSync.Infra.Identity.Auth;
using NutriSync.Infra.Identity.Entities;
using NutriSync.Infra.Identity.Interfaces;
using NutriSync.Infra.Identity.Providers;
using NutriSync.Infra.Repositories;

namespace NutriSync.Infra.Extensions;
public static class DependencyInjectionExtension
{
    public static void AddDependecyInjectionConfiguration(this WebApplicationBuilder builder)
    {
        builder.Services.AddIdentity<User, IdentityRole<Guid>>()
            .AddEntityFrameworkStores<DataContext>()
            .AddDefaultTokenProviders();

        builder.Services.AddHttpContextAccessor();
        builder.Services.AddScoped<ITenantProvider, TenantProvider>();
        //builder.Services.AddScoped<TenantProvider>();

        builder.Services.AddTransient<IAuthService, AuthService>();

        builder.Services.AddTransient<INutritionistService, NutritionistService>();
        builder.Services.AddTransient<INutritionistRepository, NutritionistRepository>();

        builder.Services.AddTransient<IPatientService, PatientService>();

        builder.Services.AddTransient<IOrderService, OrderService>();
        builder.Services.AddTransient<IOrderRepository, OrderRepository>();
    }
}
