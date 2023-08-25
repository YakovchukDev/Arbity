
using ArbityDataServer.LoggingService.Enums;

namespace ArbityDataServer.LoggingService
{
    public static class LogManager
    {
        private static string _filePath = "Log.txt";

        #region LogLevel

        public static void Debug(string message)
        {
            LogEntry newLog = new LogEntry(LogLevel.Debug, DateTime.Now, message); 
            SetDebugColor();
            Output(newLog);
            ResetColor();
            Save(newLog);
        }
        public static void Info(string message)
        {
            LogEntry newLog = new LogEntry(LogLevel.Info, DateTime.Now, message); 
            SetInfoColor();
            Output(newLog);
            ResetColor();
            Save(newLog);
        }

        public static void Error(string message)
        {
            LogEntry newLog = new LogEntry(LogLevel.Error, DateTime.Now, message);
            SetErrorColor();
            Output(newLog);
            ResetColor();
            Save(newLog);
        }

        public static void Success(string message)
        {
            LogEntry newLog = new LogEntry(LogLevel.Success, DateTime.Now, message);
            SetSuccessColor();
            Output(newLog);
            ResetColor();
            Save(newLog);
        }

        public static void Failure(string message)
        {
            LogEntry newLog = new LogEntry(LogLevel.Failure, DateTime.Now, message);
            SetFailureColor();
            Output(newLog);
            ResetColor();
            Save(newLog);
        }

        public static void Hidden(string message)
        {
            LogEntry newLog = new LogEntry(LogLevel.Hidden, DateTime.Now, message);
            Save(newLog);
        }

        #endregion

        private static void Output(LogEntry newLog)
        {
            Console.WriteLine(newLog.ToString());
        }

        private static void Save(LogEntry newLog) 
        {
            using (StreamWriter streamWriter = File.AppendText(_filePath))
            {
                streamWriter.WriteLine(newLog.GetJson());
            }
        }

        #region ConsoleColor

        private static void ResetColor()
        {
            Console.ResetColor();
        }

        private static void SetDebugColor()
        {
            Console.ForegroundColor = ConsoleColor.White;
        }

        private static void SetInfoColor()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
        }

        private static void SetFailureColor()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
        }

        private static void SetErrorColor()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
        }

        private static void SetSuccessColor()
        {
            Console.ForegroundColor = ConsoleColor.Green;
        }

        #endregion
    }
}
