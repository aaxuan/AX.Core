using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AX.Core.RunLog.Tests
{
    [TestClass()]
    public class BaseLogerTests
    {
        [TestMethod()]
        public void BaseLogerTest()
        {
            BaseLoger fileloger = new FileLoger();

            fileloger.Info("测试信息");
            fileloger.Waring("警告信息");
            fileloger.Err("错误信息");
            fileloger.Line();

            fileloger = new FileLoger("D:\\testlog");

            fileloger.Info("测试信息");
            fileloger.Waring("警告信息");
            fileloger.Err("错误信息");
            fileloger.Line();
        }
    }
}