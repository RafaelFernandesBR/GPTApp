using Serilog;
using Serilog.Events;

public static class LoggerConfig
{
    public static ILogger CreateLogger()
    {
        var file = Path.Combine(FileSystem.AppDataDirectory, "gpt-android.log");

        return new LoggerConfiguration()
        .MinimumLevel.Debug()
        .Enrich.FromLogContext()
        .WriteTo.File(file, encoding: System.Text.Encoding.UTF8)
        .CreateLogger();
    }
}
