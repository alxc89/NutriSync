using Microsoft.AspNetCore.Http;
using NutriSync.Infra.Identity.Interfaces;

namespace NutriSync.Infra.Identity.Providers;

public sealed class TenantProvider : ITenantProvider
{
    private const string TenantIdHeaderName = "X-TenantId";
    private readonly IHttpContextAccessor _httpContextAccessor;

    public TenantProvider(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string GetTenantId()
    {
        if (_httpContextAccessor.HttpContext == null)
            return string.Empty;

        var path = _httpContextAccessor.HttpContext.Request.Path.ToString().ToLower();
        if (path.Equals("/api/nutritionist/create"))
            return string.Empty;

        var tenantIdHeader = _httpContextAccessor
            .HttpContext?
            .Request
            .Headers[TenantIdHeaderName];

        if (!tenantIdHeader.HasValue || string.IsNullOrWhiteSpace(tenantIdHeader.Value))
            throw new ApplicationException("TenantId não se encontra presente!");

        return tenantIdHeader.Value.ToString();
    }
}
