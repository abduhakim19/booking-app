using API.Contracts;
using System.Net.Mail;

namespace API.Utilities.Handlers
{
    public class EmailHandler : IEmailHandler
    {   // properti dari appseting.json untuk config
        public string _server;
        public int _port;
        public string _fromEmailAddress;

        public EmailHandler(string server, int port, string fromEmailAddress)
        {
            _server = server;
            _port = port;
            _fromEmailAddress = fromEmailAddress;
        }
        // Mengirim Email
        public void Send(string subject, string body, string toEmail)
        {
            var message = new MailMessage()
            {
                From = new MailAddress(_fromEmailAddress), 
                Subject = subject, 
                Body = body, 
                IsBodyHtml = true
            };

            message.To.Add(new MailAddress(toEmail));
            using var smtpClient = new SmtpClient(_server, _port);
            smtpClient.Send(message);

        }
    }
}
