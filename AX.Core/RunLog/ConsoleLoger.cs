using System;

namespace AX.Core.RunLog
{
    public class ConsoleLoger : BaseLoger
    {
        public override void Info(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(CreateLogMsg(msg));
            Console.ResetColor();
        }

        public override void Err(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(CreateLogMsg(msg));
            Console.ResetColor();
        }

        public override void Waring(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(CreateLogMsg(msg));
            Console.ResetColor();
        }

        public override void Line()
        {
            Console.WriteLine(CreateLogMsg($"---------- ---------- ---------- ---------- ---------- ----------"));
        }

        public override void EmptyLine()
        {
            Console.WriteLine(string.Empty);
        }
    }
}