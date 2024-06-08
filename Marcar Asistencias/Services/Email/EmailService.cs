
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

        public void SendEmail(string emailTo, string recepientName, string subject, string type)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(_configuration["Mailtrap:EmailUsername "],
                    _configuration["Mailtrap:EmailFrom"]
                    ));

                message.To.Add(new MailboxAddress(type, emailTo));
                message.Subject = subject;

				var builder = new BodyBuilder();
				var templatePath = "";

				if (type == "Vacation")
				{
					templatePath = Path.Combine(
					Directory.GetCurrentDirectory(),
					"EmailTemplates",
					"Vacaciones.html"
					);
				}
				else if (type == "Comment")
				{
					templatePath = Path.Combine(
					Directory.GetCurrentDirectory(),
					"EmailTemplates",
					"Comentarios.html"
					);
				}
                else if (type == "Ausencia")
                {
                    templatePath = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "EmailTemplates",
                    "Ausencias.html"
                    );
                }
                else if (type == "Empleado")
                {
                    templatePath = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "EmailTemplates",
                    "Empleados.html"
                    );
                }
                else if (type == "Horarios")
                {
                    templatePath = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "EmailTemplates",
                    "Horarios.html"
                    );
                }

                var templateContent = File.ReadAllText(templatePath);
				templateContent = templateContent.Replace("@Comentario", recepientName);

				builder.HtmlBody = templateContent;

				message.Body = builder.ToMessageBody();

				using (var client = new SmtpClient())
                {
                    client.Connect(_configuration["Mailtrap:Host"],
                        int.Parse(_configuration["Mailtrap:Port"]), false);

                    client.Authenticate(_configuration["Mailtrap:Username"], _configuration["Mailtrap:Password"]);

                    client.Send(message);
                    client.Disconnect(true);
                }
            }
			catch (Exception)
			{
				throw;
			}
		}
    }
}
