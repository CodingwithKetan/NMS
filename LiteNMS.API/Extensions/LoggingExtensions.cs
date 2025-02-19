using LiteNMS.Utils.Logger;
using Serilog;

namespace LiteNMS.API.Extensions;

public static class LoggingExtensions
{
    public static void ConfigureLogging(this WebApplicationBuilder builder)
    {
        Log.Logger = AppLogger.CreateLogger();
        builder.Host.UseSerilog();
    }
}