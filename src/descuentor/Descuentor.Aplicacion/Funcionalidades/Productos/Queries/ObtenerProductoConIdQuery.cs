using Descuentor.Aplicacion.Dtos;
using MediatR;

namespace Descuentor.Aplicacion.Funcionalidades.Productos.Queries;

public record ObtenerProductoConIdQuery(int Id) : IRequest<ProductoHistorialPreciosDto>;