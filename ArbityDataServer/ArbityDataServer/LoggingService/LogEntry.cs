using ArbityDataServer.LoggingService.Enums;
using System.Reflection;
using System.Text.Json;

namespace ArbityDataServer.LoggingService
{
    class LogEntry
    {
        public LogType LogLevel { get; private set; }
        public DateTime Time { get; private set; }
        public string Location { get; private set; }
        public string Message { get; private set; }

        public LogEntry(LogType logLevel, DateTime time, MethodBase location, string message)
        {
            LogLevel = logLevel;
            Location = $"{location.DeclaringType.FullName}.{location.Name}()";
            Time = time;
            Message = message;
        }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                
        public string ToString() => $" {LogLevel}\t| {Location} | {Message}";

        public string GetJson() => JsonSerializer.Serialize(this);
    }
}
