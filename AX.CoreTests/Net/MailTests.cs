using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AX.Core.Net.Tests
{
    [TestClass()]
    public class MailTests
    {
        [TestMethod()]
        public void MailTest()
        {
            var mail = new AX.Core.Net.Mail("1051664725@qq.com", "lntgucvqofsgbddj", "Core测试");
            mail.Send("1051664725@qq.com", "Core测试", "测试信息,测试信息,测试信息,v,测试信息,v,,v测试信息", false);

            var email = new EMail();
            email.Send("1051664725@qq.com", "Core测试", "测试信息,测试信息,测试信息,v,测试信息,v,,v测试信息");
        }
    }
}