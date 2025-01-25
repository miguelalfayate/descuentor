using Descuentor.Shared.Dtos;
using MediatR;

namespace Descuentor.Aplicacion.Funcionalidades.Productos.Queries;

public record ObtenerProductoConIdQuery(int Id) : IRequest<ProductoHistorialPreciosDto>;