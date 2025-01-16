using MediatR;

namespace Descuentor.Aplicacion.Funcionalidades.Productos.Commands;

public record ActualizarProductoCommand(
    int Id,
    string Nombre,
    string Descripcion,
    int TiendaOnlineId,
    string UrlImagen,
    string Url,
    decimal PrecioInicial
    ) : IRequest<bool>
{
   
}