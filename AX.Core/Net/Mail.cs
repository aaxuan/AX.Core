using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace AX.Core.Net
{
    public class Mail
    {
        public Encoding Encoding { get; set; } = AxCoreGlobalSettings.Encodeing;
        public bool IsBodyHtml = false;

        private string FromAddress;
        private string AuthCode;
        private bool UseAuth { get { if (string.IsNullOrWhiteSpace(AuthCode) == false) { return true; } return false; } }
        private string SmtpServer;
        private int SmtpPort;

        public Mail()
        { }

        public Mail UseNetease163Server()
        {
            SmtpServer = "smtp.163.com";
            return this;
        }

        public Mail UseQQServer()
        {
            SmtpServer = "smtp.qq.com";
            SmtpPort = 587;
            return this;
        }

        public Mail UseServers(string smtpServer, int smtpPort)
        {
            SmtpServer = smtpServer;
            SmtpPort = smtpPort;
            return this;
        }

        public Mail SetAuth(string fromAddress, string authCode)
        {
            FromAddress = fromAddress;
            AuthCode = authCode;
            return this;
        }

        public void Send(string toAddress, string body, string subject = null)
        {
            BatchSend(new List<string>() { toAddress }, body, subject);
        }

        public void BatchSend(List<string> toAddress, string body, string subject = null)
        {
            if (string.IsNullOrWhiteSpace(subject))
            { subject = AxCoreGlobalSettings.MailDefaultSubject; }

            string smtpServer = SmtpServer;

            SmtpClient smtpClient;
            if (SmtpPort <= 0)
            { smtpClient = new SmtpClient(smtpServer); }
            else
            { smtpClient = new SmtpClient(smtpServer, SmtpPort); }

            if (UseAuth)
            {
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(FromAddress, AuthCode);
            }

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
            mail.IsBodyHtml = IsBodyHtml;
            mail.Body = body;

            smtpClient.Send(mail);
        }
    }
}