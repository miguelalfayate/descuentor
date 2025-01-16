using System.Net;
using System.Net.Mail;
using Descuentor.Dominio.Interfaces;

namespace Descuentor.Infraestructura.Servicios;

public class EnvioEmailService : IEnvioEmailService
{
    public Task EnviarEmailAsync(string email, string subject, string htmlMessage)
    {
        var username = "040ec699b7d160";
        var pwd = "dc13edff67f09f";
        
        var client = new SmtpClient("sandbox.smtp.mailtrap.io", 2525)
        {
            Credentials = new NetworkCredential(username, pwd),
            EnableSsl = true
        };

        var mailMessage = new MailMessage(from: "descuentor@example.com",
            to: email,
            subject: subject,
            body: htmlMessage
        );
        mailMessage.IsBodyHtml = true;
        
        return client.SendMailAsync(mailMessage);
    }
}