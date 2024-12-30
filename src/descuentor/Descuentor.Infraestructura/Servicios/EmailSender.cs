
using Descuentor.Infraestructura.ModelosIdentity;
using Microsoft.AspNetCore.Identity;

namespace Descuentor.Infraestructura.Servicios
{
    public class EmailSender : IEmailSender<UsuarioAplicacion>
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // Implementa el envío de correo electrónico aquí
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