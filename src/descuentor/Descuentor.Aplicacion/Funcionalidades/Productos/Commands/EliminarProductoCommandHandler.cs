using Descuentor.Dominio.Interfaces;
using MediatR;

namespace Descuentor.Aplicacion.Funcionalidades.Productos.Commands;

public class EliminarProductoCommandHandler: IRequestHandler<EliminarProductoCommand, bool>
{
    private readonly IProductoRepository _productoRepository;

    public EliminarProductoCommandHandler(IProductoRepository productoRepository)
    {
        _productoRepository = productoRepository;
    }

    public async Task<bool> Handle(EliminarProductoCommand request, CancellationToken cancellationToken)
    {
        var eliminado = await _productoRepository.EliminarProductoByIdAsync(request.Id);
        return eliminado;
    }
}