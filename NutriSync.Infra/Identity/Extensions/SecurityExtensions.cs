using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace NutriSync.Infra.Security.Extensions;

public static class SecurityExtensions
{
    public static void AddSecurity(this IServiceCollection services)
    {
        services
            .AddAuthentication(IdentityConstants.ApplicationScheme)
            .AddIdentityCookies();

        //services.AddAuthorization();
    }
}
