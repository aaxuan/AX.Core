using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace AX.Core.Net
{
    public class Mail
    {
        public enum MailServesEnum
        {
            Netease163,
            QQ,
        }

        public Encoding Encoding { get; } = Encoding.UTF8;

        private string FromAddress;
        private string AuthCode;
        private string SmtpServer;
        private int SmtpPort;

        public Mail()
        { }

        public void UseServersConfig(MailServesEnum mailServes)
        {
            switch (mailServes)
            {
                case MailServesEnum.Netease163: SmtpServer = "smtp.163.com"; break;
                case MailServesEnum.QQ: SmtpServer = "smtp.qq.com"; SmtpPort = 587; break;
                default: return;
            }
            return;
        }

        public void SetAuth(string fromAddress, string authCode)
        {
            FromAddress = fromAddress;
            AuthCode = authCode;
        }

        public void Send(string toAddress, string body, string subject = "系统通知")
        {
            string smtpServer = SmtpServer;

            SmtpClient smtpClient = null;
            if (SmtpPort <= 0)
            { smtpClient = new SmtpClient(smtpServer); }
            else
            { smtpClient = new SmtpClient(smtpServer, SmtpPort); }

            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(FromAddress, AuthCode);
            smtpClient.EnableSsl = true;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

            var mail = new MailMessage(FromAddress, toAddress);
            mail.Subject = subject;
            mail.BodyEncoding = Encoding;
            mail.IsBodyHtml = true;
            mail.Body = body;

            smtpClient.Send(mail);
        }

        public void BatchSend(List<string> toAddress, string body, string subject = "系统通知")
        {
            string smtpServer = SmtpServer;

            SmtpClient smtpClient = null;
            if (SmtpPort <= 0)
            { smtpClient = new SmtpClient(smtpServer); }
            else
            { smtpClient = new SmtpClient(smtpServer, SmtpPort); }

            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(FromAddress, AuthCode);
            smtpClient.EnableSsl = true;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

            var mail = new MailMessage();
            mail.From = new MailAddress(FromAddress);
            foreach (var item in toAddress)
            {
                mail.To.Add(item);
            }
            mail.Subject = subject;
            mail.BodyEncoding = Encoding;
            mail.IsBodyHtml = true;
            mail.Body = body;

            smtpClient.Send(mail);
        }
    }
}