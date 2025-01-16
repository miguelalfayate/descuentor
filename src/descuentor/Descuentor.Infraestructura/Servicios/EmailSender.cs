
using System.Net;
using System.Net.Mail;
using Descuentor.Infraestructura.ModelosIdentity;
using Microsoft.AspNetCore.Identity;

namespace Descuentor.Infraestructura.Servicios
{
    public class EmailSender : IEmailSender<UsuarioAplicacion>
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // var mail = "fedlop002@gmail.com";
            // var pwd = "GRD!^9Jp2KuB";
            //
            // var client = new SmtpClient("smtp.gmail.com", 587)
            // {
            //     Credentials = new NetworkCredential(mail, pwd),
            //     EnableSsl = true
            // };
            // return client.SendMailAsync(new MailMessage(from: mail,
            //     to: email,
            //     subject: subject,
            //     body: htmlMessage
            // ));
            return Task.CompletedTask;
        }

        public Task SendConfirmationLinkAsync(UsuarioAplicacion user, string email, string confirmationLink)
        {
            return Task.CompletedTask;
        }

        public Task SendPasswordResetLinkAsync(UsuarioAplicacion user, string email, string resetLink)
        {
            return Task.CompletedTask;
        }

        public Task SendPasswordResetCodeAsync(UsuarioAplicacion user, string email, string resetCode)
        {
            return Task.CompletedTask;
        }
    }
}