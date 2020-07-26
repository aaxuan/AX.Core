using System.Net;
using System.Net.Mail;
using System.Text;

namespace AX.Core.Net
{
    //服务器名称: outlook.office365.com
    //端口: 993
    //加密方法: TLS

    //服务器名称: smtp.office365.com
    //端口: 587
    //加密方法: STARTTLS

    public class Mail
    {
        private readonly SmtpClient _client;
        private readonly string _fromName;
        private readonly string _formAddress;

        public Mail(string formAddress, string authCode, string fromName)
        {
            _client = new SmtpClient();
            _client.Host = "smtp.qq.com";
            _client.Port = 25;
            _client.EnableSsl = true;
            _client.UseDefaultCredentials = false;
            _client.Credentials = new NetworkCredential(formAddress, authCode);
            _formAddress = formAddress;
            _fromName = fromName;
        }

        public void Send(string toAddress, string subject, string body, bool isHtml = true)
        {
            var message = new MailMessage();
            message.SubjectEncoding = Encoding.UTF8;
            message.BodyEncoding = Encoding.UTF8;
            message.Priority = MailPriority.High;

            //发送人
            message.From = new MailAddress(_formAddress, _fromName);
            //接受人
            message.To.Add(toAddress);
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = isHtml;

            //发送
            _client.Send(message);
        }
    }

    public class EMail
    {
        private SmtpClient _client
        {
            get
            {
                SmtpClient client = new SmtpClient();
                client.Host = "smtp.qq.com";
                client.Port = 587;
                client.EnableSsl = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = true;
                client.Credentials = new NetworkCredential(_formAddress, _authCode);
                return client;
            }
        }

        private string _formAddress { get; set; } = "1051664725@qq.com";

        private string _fromName { get; set; } = "AIFund通知";

        private string _authCode { get; set; } = "lntgucvqofsgbddj";

        public void Send(string toAddress, string subject, string body)
        {
            var message = new MailMessage();
            message.SubjectEncoding = Encoding.UTF8;
            message.BodyEncoding = Encoding.UTF8;
            message.Priority = MailPriority.High;

            //发送人
            message.From = new MailAddress(_formAddress, _fromName);
            //接受人
            message.To.Add(toAddress);
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = false;

            //发送
            _client.Send(message);
        }
    }

    public class EMail2
    {
        public void Send(string email)
        {
            SmtpClient client = new SmtpClient("smtp.qq.com");
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("1051664725@qq.com", "lntgucvqofsgbddj");

            MailAddress from = new MailAddress("1051664725@qq.com", "显示名称", Encoding.UTF8);//初始化发件人

            MailAddress to = new MailAddress(email, "", Encoding.UTF8);//初始化收件人

            //设置邮件内容
            MailMessage message = new MailMessage(from, to);
            message.Body = "asdasdasdasdasd";

            //发送邮件
            client.Send(message);
        }
    }
}