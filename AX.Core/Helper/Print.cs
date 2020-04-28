using System;
using System.Text;

namespace AX.Core.Helper
{
    public static class Print
    {
        static Print()
        {
            _sb = new StringBuilder();
        }

        private static StringBuilder _sb { get; set; }

        public static string GetPrintString()
        {
            return _sb.ToString();
        }

        public static void ClearPrintString()
        {
            _sb.Clear();
        }

        private static void PrintInfo(string msg)
        {
            Console.WriteLine($"[{DateTime.Now.ToString("MM-dd HH:mm:ss")}] {msg}");
        }

        public static void LogInfo(string msg)
        {
            _sb.AppendLine(msg);
            Info(msg);
        }

        public static void Info(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            PrintInfo(msg);
            Console.ResetColor();
        }

        public static void Err(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            PrintInfo(msg);
            Console.ResetColor();
        }

        public static void Waring(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            PrintInfo(msg);
            Console.ResetColor();
        }

        public static void Line()
        {
            Console.WriteLine($"********** ********** ********** ********** **********");
        }

        public static void EmptyLine()
        {
            Console.WriteLine($"");
        }
    }
}