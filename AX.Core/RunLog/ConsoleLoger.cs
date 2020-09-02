using AX.Core.Extension;
using System;

namespace AX.Core.RunLog
{
    public class ConsoleLoger : ILoger
    {
        public ConsoleLoger(string name)
        {
            name.CheckIsNullOrWhiteSpace();
            Name = name;
        }

        public string Name { get; private set; }

        public void Debug(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(GlobalLogManager.CreateMessage(LogLevel.Debug, msg));
            Console.ResetColor();
        }

        public void Error(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(GlobalLogManager.CreateMessage(LogLevel.Error, msg));
            Console.ResetColor();
        }

        public void Info(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(GlobalLogManager.CreateMessage(LogLevel.Info, msg));
            Console.ResetColor();
        }

        public void Waring(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(GlobalLogManager.CreateMessage(LogLevel.Waring, msg));
            Console.ResetColor();
        }
    }
}