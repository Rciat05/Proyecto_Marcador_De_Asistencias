
using MailKit.Net.Smtp;
using MimeKit;

namespace Marcar_Asistencias.Services.Email
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SendEmail(string emailTo, string recepientName, string subject, string body)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(_configuration["Mailtrap:EmailUsername "],
                    _configuration["Mailtrap:EmailFrom"]
                    ));

                message.To.Add(new MailboxAddress(recepientName, emailTo));
                message.Subject = subject;
                message.Body = new TextPart("plain") { Text = body };

                using (var client = new SmtpClient())
                {
                    client.Connect(_configuration["Mailtrap:Host"],
                        int.Parse(_configuration["Mailtrap:Port"]), false);

                    client.Authenticate(_configuration["Mailtrap:Username"], _configuration["Mailtrap:Password"]);

                    client.Send(message);
                    client.Disconnect(true);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
