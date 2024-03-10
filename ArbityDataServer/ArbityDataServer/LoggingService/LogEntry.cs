using ArbityDataServer.LoggingService.Enums;
using System.Text.Json;

namespace ArbityDataServer.LoggingService
{
    class LogEntry
    {
        public LogType LogLevel { get; private set; }
        public DateTime Date { get; private set; }
        public string Message { get; private set; }

        public LogEntry(LogType logLevel, DateTime time, string message)
        {
            LogLevel = logLevel;
            Date = time;
            Message = message;
        }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                
        public string ToString() => $" {LogLevel}\t| {Message}";

        public string GetJson() => JsonSerializer.Serialize(this);
    }
}
