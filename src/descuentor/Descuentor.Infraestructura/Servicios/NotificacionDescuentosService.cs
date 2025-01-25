using Descuentor.Dominio.Entidades;
using Descuentor.Dominio.Interfaces;
using Descuentor.Infraestructura.ModelosIdentity;
using Microsoft.AspNetCore.Identity;

namespace Descuentor.Infraestructura.Servicios;

public class NotificacionDescuentosService : INotificacionDescuentosService
{
    private readonly UserManager<UsuarioAplicacion> _userManager;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IEnvioEmailService _envioEmailService;

    public NotificacionDescuentosService(IUsuarioRepository usuarioRepository, IEnvioEmailService envioEmailService, UserManager<UsuarioAplicacion> userManager)
    {
        _usuarioRepository = usuarioRepository;
        _envioEmailService = envioEmailService;
        _userManager = userManager;
    }

    public async Task<bool> NotificarDescuentoEmailAsync(int usuarioId, List<Producto> productos)
    {
        var usuario = _userManager.Users.FirstOrDefault(u => u.Id == usuarioId);
        var email = usuario!.Email;
        //var logoUrl = "https://raw.githubusercontent.com/miguelalfayate/descuentor/refs/heads/main/src/ChromeExtension/logo.png";

        var mensaje = @$"
<!DOCTYPE html>
<html lang=""es"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <style>
        body {{
            font-family: 'Arial', sans-serif;
            max-width: 600px;
            margin: 0 auto;
            padding: 20px;
            background-color: #f4f4f4;
            color: #333;
            line-height: 1.6;
        }}
        .email-container {{
            background-color: white;
            border-radius: 10px;
            box-shadow: 0 4px 6px rgba(0,0,0,0.1);
            overflow: hidden;
        }}
        .logo {{
            width: 250px;
            margin: 10px auto;
            display: block;
        }}
        .header {{
            background-color: #f8f8f8;
            padding: 15px;
            text-align: center;
        }}
        .product-list {{
            padding: 15px;
        }}
        .product {{
            margin-bottom: 40px;
            border-bottom: 1px solid #e0e0e0;
            padding-bottom: 15px;
        }}
        .product:last-child {{
            border-bottom: none;
        }}
        .product-image {{
            max-width: 200px;
            max-height: 200px;
            object-fit: cover;
            border-radius: 8px;
            margin-bottom: 30px;
            display: block;
            margin-left: auto;
            margin-right: auto;
        }}
.product-name {{
   font-size: 16px;
   color: #0066cc;
   margin-bottom: 30px;
   text-decoration: underline;
   display: block;
   text-align: center;
}}
        .price-container {{
            display: flex;
            justify-content: space-between;
            align-items: center;
            margin-top: 20px;
            margin-bottom: 30px;
        }}
        .original-price {{
            text-decoration: line-through;
            color: #7f8c8d;
            font-size: 14px;
        }}
        .current-price {{
            color: #27ae60;
            font-weight: bold;
            font-size: 18px;
        }}
        .discount-badge {{
            background-color: #27ae60;
            color: white;
            padding: 3px 8px;
            border-radius: 20px;
            font-size: 12px;
        }}
        .footer {{
            text-align: center;
            padding: 15px;
            background-color: #f8f8f8;
            color: #7f8c8d;
        }}
    </style>
</head>
<body>
    <div class=""email-container"">
        <div class=""header"">
            <img src=""https://raw.githubusercontent.com/miguelalfayate/descuentor/refs/heads/main/src/ChromeExtension/logo.png"" alt=""Logo de la Tienda"" class=""logo"">
            <h2 style=""font-family: serif;"">¡Ofertas Especiales para Ti!</h2>
        </div>

        <div class=""product-list"">
            <h3>Hola {(string.IsNullOrEmpty(usuario.Nombre) ? usuario.Email : usuario.Nombre)},</h3>
            <p style='margin-bottom: 60px;'>Te presentamos estos increíbles productos con descuento:</p>

            {string.Join("", productos.Select(p => $@"
            <div class='product'>
                <a href='{p.Url}'><img src='{p.UrlImagen}' alt='{p.Nombre}' class='product-image'></a>
                <a href='{p.Url}' class='product-name'>{p.Nombre}</a>
                <div class='price-container'>
                    <span class='original-price'>{p.PrecioInicial:C}</span>
                    <span class='current-price'>{p.HistorialPrecios!.OrderByDescending(hp => hp.FechaConsulta).First().Precio:C}</span>
                    <span class='discount-badge'>
                        {(p.PrecioInicial - p.HistorialPrecios!.OrderByDescending(hp => hp.FechaConsulta).First().Precio) / p.PrecioInicial * 100:F0}% Descuento
                    </span>
                </div>
            </div>"))}
        </div>

        <div class=""footer"">
            <p>¡Aprovecha estas ofertas antes de que terminen!</p>
            <p>Para más información visítanos en nuestra <a href=""https://www.tu-pagina-web.com"">página web</a></p>
        </div>
    </div>
</body>
</html>";
        
        var subject = "Descuentor - Descuentos en tus productos favoritos";
        
        await _envioEmailService.EnviarEmailAsync(email!, subject, mensaje);

        return true;
    }
}