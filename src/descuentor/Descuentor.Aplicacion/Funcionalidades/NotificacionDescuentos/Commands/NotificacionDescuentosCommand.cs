using MediatR;

namespace Descuentor.Aplicacion.Funcionalidades.NotificacionDescuentos.Commands;

public record NotificacionDescuentosCommand(
    Dictionary<int, decimal> HistorialPrecios) : IRequest<int>;