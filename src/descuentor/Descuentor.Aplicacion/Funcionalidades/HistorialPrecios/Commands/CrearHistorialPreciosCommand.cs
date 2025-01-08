using MediatR;

namespace Descuentor.Aplicacion.Funcionalidades.HistorialPrecios.Commands;

public record CrearHistorialPreciosCommand(Dictionary<int, decimal> HistorialPrecios) : IRequest<int>;
