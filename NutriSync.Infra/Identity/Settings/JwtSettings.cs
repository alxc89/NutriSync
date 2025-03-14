namespace NutriSync.Infra.Security.Settings;

public class JwtSettings
{
    public string SecretKey { get; set; } = "98798D9849849@98949D49F48553SC@3";
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public int ExpirationHours { get; set; }
}