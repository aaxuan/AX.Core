using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AX.Core.Net.Tests
{
    [TestClass()]
    public class MailTests
    {
        [TestMethod()]
        public void MailTest()
        {
            var mail = new Mail();
            mail.UseServersConfig(Mail.MailServesEnum.Netease163);
            mail.SetAuth("acuxuan@163.com", "");
            mail.Send("1051664725@qq.com", "<h1>测试信息,测试信息,测试信息,测试信息,测试信息</h1>");
            var b = 1;
        }
    }
}