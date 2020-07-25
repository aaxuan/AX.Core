using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AX.Core.RunLog.Tests
{
    [TestClass()]
    public class BaseLogerTests
    {
        [TestMethod()]
        public void BaseLogerTest()
        {
            BaseLoger consoleloger = new ConsoleLoger();
            consoleloger.UseLogLog = true;

            consoleloger.Info("测试信息");
            consoleloger.Waring("警告信息");
            consoleloger.Err("错误信息");
            consoleloger.EmptyLine();
            consoleloger.Line();

            consoleloger.ClearAllLog(); 
            consoleloger.Info("测试信息");

            var log = consoleloger.GetAllLog();
            Assert.IsNotNull(log);

            //BaseLoger fileloger = new FileLoger();

            //fileloger.Info("测试信息");
            //fileloger.Waring("警告信息");
            //fileloger.Err("错误信息");
            //fileloger.EmptyLine();
            //fileloger.Line();

            //fileloger = new FileLoger("D:\\testlog");

            //fileloger.Info("测试信息");
            //fileloger.Waring("警告信息");
            //fileloger.Err("错误信息");
            //fileloger.EmptyLine();
            //fileloger.Line();
        }
    }
}