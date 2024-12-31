using MediatR;

namespace Descuentor.Aplicacion.Funcionalidades.Productos.Commands;

public record CrearProductoCommand(
    string Nombre,
    string Url,
    string? Descripcion,
    string? UrlImagen,
    decimal? PrecioActual,
    int TiendaOnlineId) : IRequest<int>;