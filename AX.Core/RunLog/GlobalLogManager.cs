using System;

namespace AX.Core.RunLog
{
    public class GlobalLogManager
    {
        public static string CreateMessage(LogLevel logLevel, string message)
        {
            return $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss,fff}] [{logLevel,6}] {message}";
        }
    }
}