using ArbityDataServer.LoggingService.Enums;

namespace ArbityDataServer.LoggingService
{
    public static class Logger
    {
        private static readonly string _filePath = @"Log.json";

        public static void Info(string message) => Log(LogType.Info, message);
        public static void Success(string message) => Log(LogType.Success, message);
        public static void Error(string message) => Log(LogType.Error, message);
        public static void Debug(string message) => Log(LogType.Debug, message);
        public static void Hidden(string message) => Log(LogType.Hidden, message);

        public static void Log(LogType logLevel, string message) 
        {
            LogEntry newLog = new LogEntry(logLevel, DateTime.Now, message);
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
                LogEntry log = new LogEntry(LogType.Error, DateTime.Now, ex.Message);
                Output(log);
            }
        }
    }
}
