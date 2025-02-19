using Serilog;

namespace LiteNMS.API.Extensions;

public static class MiddlewareExtensions
{
    public static void ConfigureMiddleware(this WebApplication app)
    {
        // Enforce HTTPS
        app.UseHttpsRedirection();

        // Logging
        app.UseSerilogRequestLogging();

        // Authorization
        app.UseAuthorization();

        // Map Controllers
        app.MapControllers();

    }
}