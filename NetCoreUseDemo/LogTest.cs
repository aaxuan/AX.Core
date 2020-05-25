using AX.Core.RunLog;

namespace NetCoreUseDemo
{
    public class LogTest
    {
        private static BaseLoger consoleLog { get; } = new ConsoleLoger();

        public void Test()
        {
            consoleLog.Waring("开始日志类测试");

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

            consoleLog.Waring("结束日志类测试");
        }
    }
}