using Descuentor.Dominio.Entidades;
using Descuentor.Dominio.Interfaces;
using Descuentor.Infraestructura.ModelosIdentity;

namespace Descuentor.Infraestructura.Servicios;

public class NotificacionDescuentosService : INotificacionDescuentosService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IEnvioEmailService _envioEmailService;

    public NotificacionDescuentosService(IUsuarioRepository usuarioRepository, IEnvioEmailService envioEmailService)
    {
        _usuarioRepository = usuarioRepository;
        _envioEmailService = envioEmailService;
    }

    public async Task<bool> NotificarDescuentoEmailAsync(int usuarioId, List<Producto> productos)
    {
        var usuario = (UsuarioAplicacion)await _usuarioRepository.ObtenerUsuarioById(usuarioId);
        var email = usuario.Email;

        var mensaje = $@"
<html>
<head>
<meta charset=""UTF-8"">
    <style>
        body {{ font-family: Arial, sans-serif; }}
        .producto {{ margin-bottom: 20px; }}
        .producto h2 {{ color: #333; }}
        .producto p {{ color: #666; }}
    </style>
</head>
<body>
    <h1>Hola {(string.IsNullOrEmpty(usuario.Nombre) ? usuario.Email : usuario.Nombre)},</h1>
    <p>Te notificamos que los siguientes productos tienen un descuento:</p>
    <table>
    <thead>
        <tr>
            <th>Producto</th>
            <th>Precio Inicial</th>
            <th>Precio Actual</th>
            <th>% Descuento</th>
        </tr>
    </thead>
    <tbody>
        {string.Join("", productos.Select(p => $@"
        <tr class='producto'>
            <td><a href='{p.Url}'>{p.Nombre}</a></td>
            <td>{p.PrecioInicial:C}</td>
            <td>{p.HistorialPrecios.OrderByDescending(hp => hp.FechaConsulta).First().Precio:C}</td>
            <td>{((p.PrecioInicial - p.HistorialPrecios.OrderByDescending(hp => hp.FechaConsulta).First().Precio) / p.PrecioInicial * 100):F2}%</td>
        </tr>"))}
    </tbody>
</table>
    <p>¡Aprovecha estas ofertas!</p>
</body>
</html>";
        
        var subject = "Descuentor - Descuentos en tus productos favoritos";
        
        await _envioEmailService.EnviarEmailAsync(email!, subject, mensaje);

        return true;
    }
}