using Descuentor.Dominio.Entidades;
using Descuentor.Dominio.Interfaces;
using MediatR;

namespace Descuentor.Aplicacion.Funcionalidades.Productos.Commands;

public class ActualizarProductoCommandHandler : IRequestHandler<ActualizarProductoCommand, bool>
{
    
    private readonly IProductoRepository _productoRepository;

    public ActualizarProductoCommandHandler(IProductoRepository productoRepository)
    {
        _productoRepository = productoRepository;
    }

    public async Task<bool> Handle(ActualizarProductoCommand request, CancellationToken cancellationToken)
    {
        var producto = new Producto
        {
            Id = request.Id,
            Nombre = request.Nombre,
            Descripcion = request.Descripcion,
            TiendaOnlineId = request.TiendaOnlineId,
            Url = request.Url,
            UrlImagen = request.UrlImagen,
            PrecioInicial = request.PrecioInicial
        };
        
        var productoModificado = await _productoRepository.UpdateAsync(producto);
        return productoModificado > 0;
    }
}