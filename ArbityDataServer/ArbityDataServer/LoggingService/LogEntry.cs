using ArbityDataServer.LoggingService.Enums;
using System.Text.Json;

namespace ArbityDataServer.LoggingService
{
    class LogEntry
    {
        public LogLevel LogLevel { get; private set; }
        public DateTime Time { get; private set; }
        public string Message { get; private set; }

        public LogEntry(LogLevel logLevel, DateTime time, string message)
        {
            LogLevel = logLevel;
            Time = time;
            Message = message;
        }

        public string ToString() => $"{LogLevel}\t| {Time} | {Message}";

        public string GetJson() => JsonSerializer.Serialize(this);
    }
}
