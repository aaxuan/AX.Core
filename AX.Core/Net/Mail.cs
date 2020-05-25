using System.Net;
using System.Net.Mail;
using System.Text;

namespace AX.Core.Net
{
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
}