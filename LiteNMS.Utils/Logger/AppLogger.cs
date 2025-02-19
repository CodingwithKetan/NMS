using Serilog;

namespace LiteNMS.Utils.Logger;

public static class AppLogger
{
    public static ILogger CreateLogger()
    {
        return new LoggerConfiguration()
            .MinimumLevel.Debug() // Log from Debug level upwards
            .WriteTo.Console() // Log to console
            .WriteTo.File("Logs/litenms_log-.txt", rollingInterval: RollingInterval.Day) // Daily log files
            .Enrich.FromLogContext() // Add contextual information automatically
            .CreateLogger();
    }
}
