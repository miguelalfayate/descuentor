using MediatR;

namespace Descuentor.Aplicacion.Funcionalidades.Productos.Queries;

public record ObtenerTodasUrlQuery() : IRequest<Dictionary<int, string>>;