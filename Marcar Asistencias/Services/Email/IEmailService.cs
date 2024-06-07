namespace Marcar_Asistencias.Services.Email
{
    public interface IEmailService
    {
        public void SendEmail(string emailTo, string recepientName, string subject, string type);
    }
}
