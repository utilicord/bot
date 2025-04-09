using Discord;
using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace UtiliCord
{
    public class Logging
    {
        public static LoggingLevelSwitch LogLevelSwitch { get; set; }

        public static void Configure()
        {
            LogLevelSwitch = new LoggingLevelSwitch(IsDebugMode ? LogEventLevel.Debug : LogEventLevel.Information);

            Directory.CreateDirectory("logs");
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Quartz", LogEventLevel.Information) // quartz is already pretty verbose, even in debug mode
                .WriteTo.Console()
                .WriteTo.Async(f => f.File("data/logs/ErrorLog.log", LogEventLevel.Warning, rollingInterval: RollingInterval.Day))
                .MinimumLevel.ControlledBy(LogLevelSwitch)
                .CreateLogger();
        }

        public static Task ProcessDNetLog(LogMessage msg)
        {
            Log.Write(msg.Severity switch
            {
                LogSeverity.Critical => LogEventLevel.Fatal,
                LogSeverity.Error => LogEventLevel.Error,
                LogSeverity.Warning => LogEventLevel.Warning,
                LogSeverity.Info => LogEventLevel.Information,
                LogSeverity.Verbose => LogEventLevel.Verbose,
                LogSeverity.Debug => LogEventLevel.Debug,
                _ => LogEventLevel.Information,
            }, "{Source}: {Message}", msg.Source, msg.Exception?.ToString() ?? msg.Message);

            return Task.CompletedTask;
        }

        public static bool IsDebugMode
        {
            get
            {
#if DEBUG
                return true;
#else
                return false;
#endif
            }
        }
    }
}
