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
            mail.SetAuth("", "");
            mail.Send("", "<h1>测试信息,测试信息,测试信息,测试信息,测试信息</h1>");
            var b = 1;
        }
    }
}