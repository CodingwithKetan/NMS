namespace NMS.WebAPI.Helpers;

public class AppConfig
{
    private readonly IConfiguration _configuration;

    public AppConfig(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    // Method to get JWT Issuer
    public string GetJwtIssuer() => _configuration["Jwt:Issuer"];

    // Method to get JWT Audience
    public string GetJwtAudience() => _configuration["Jwt:Audience"];

    // Method to get JWT Key
    public string GetJwtKey() => _configuration["Jwt:Key"];

    // Method to get SQL Connection String
    public string GetSqlConnectionString() => _configuration.GetConnectionString("DefaultConnection");
}
