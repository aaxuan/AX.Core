//using AX.Core.Business.DataModel;

using System;

namespace NetCoreUseDemo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            new DBTest().Test();
            //new LogTest().Test();
            Console.ReadLine();
        }
    }
}