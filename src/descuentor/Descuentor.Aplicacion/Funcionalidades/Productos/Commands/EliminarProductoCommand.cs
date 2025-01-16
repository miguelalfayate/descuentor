using MediatR;

namespace Descuentor.Aplicacion.Funcionalidades.Productos.Commands;

public record EliminarProductoCommand(
    int Id
) : IRequest<bool>;

