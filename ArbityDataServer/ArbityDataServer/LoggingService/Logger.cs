using ArbityDataServer.LoggingService.Enums;
using System.Reflection;

namespace ArbityDataServer.LoggingService
{
    public static class Logger
    {
        private static readonly string _filePath = @"Log.json";

        public static void Info(string message, MethodBase location) => Log(LogType.Info, location, message);
        public static void Success(string message, MethodBase location) => Log(LogType.Success, location, message);
        public static void Erorr(string message, MethodBase location) => Log(LogType.Error, location, message);
        public static void Failure(string message, MethodBase location) => Log(LogType.Failure, location, message);
        public static void Debug(string message, MethodBase location) => Log(LogType.Debug, location, message);
        public static void Hidden(string message, MethodBase location) => Log(LogType.Hidden, location, message);

        public static void Log(LogType logLevel, MethodBase location, string message) 
        {
            LogEntry newLog = new LogEntry(logLevel, DateTime.Now, location, message);
            if (newLog.LogLevel != LogType.Hidden)
            {
                Output(newLog);
            }
            WriteToFile(newLog);
        }

        private static void Output(LogEntry newLog)
        {
            switch (newLog.LogLevel)
            {
                case LogType.Info:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case LogType.Success:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case LogType.Error:
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    break;
                case LogType.Failure:
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    break;
                case LogType.Debug:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                default:
                    Console.ResetColor();
                    break;
            }
            Console.WriteLine(newLog.ToString());
            Console.ResetColor();
        }

        private static void WriteToFile(LogEntry newLog)
        {
            try
            {
                if(!File.Exists(_filePath))
                {
                    File.Create(_filePath).Close();
                }
                using (StreamWriter streamWriter = File.AppendText(_filePath))
                {
                    streamWriter.WriteLine(newLog.GetJson());
                }
            }
            catch (Exception ex)
            {
                LogEntry log = new LogEntry(LogType.Error, DateTime.Now, MethodBase.GetCurrentMethod(), ex.Message);
                Output(log);
            }
        }
    }
}
