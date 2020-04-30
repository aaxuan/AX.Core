using System.Net;
using System.Net.Mail;
using System.Text;

namespace AX.Core.Net
{
    public class Mail
    {
        public SmtpClient _client
        {
            get
            {
                SmtpClient client = new SmtpClient();
                client.Host = "smtp.qq.com";
                client.Port = 25;
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(_formAddress, _authCode);
                return client;
            }
        }

        public string _formAddress { get; set; } = "";

        public string _fromName { get; set; } = "";

        public string _authCode { get; set; } = "";

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
            message.IsBodyHtml = true;

            //发送
            _client.Send(message);
        }
    }
}