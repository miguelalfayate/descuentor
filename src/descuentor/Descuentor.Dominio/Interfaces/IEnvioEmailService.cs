namespace Descuentor.Dominio.Interfaces;

public interface IEnvioEmailService
{
    Task EnviarEmailAsync(string email, string subject, string message);
}