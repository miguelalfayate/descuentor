using Descuentor.Aplicacion.Interfaces;
using Descuentor.Dominio.Entidades;
using Descuentor.Dominio.Interfaces;
using MediatR;

namespace Descuentor.Aplicacion.Funcionalidades.Productos.Commands;

public class ActualizarProductoCommandHandler : IRequestHandler<ActualizarProductoCommand, bool>
{
    
    private readonly IProductoRepository _productoRepository;
    private readonly ICurrentUser _currentUser;

    public ActualizarProductoCommandHandler(IProductoRepository productoRepository, ICurrentUser currentUser)
    {
        _productoRepository = productoRepository;
        _currentUser = currentUser;
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
            PrecioInicial = request.PrecioInicial,
            UsuarioAplicacionId = !string.IsNullOrEmpty(_currentUser.Id) ? int.Parse(_currentUser.Id!) : 1
        };
        
        var productoModificado = await _productoRepository.UpdateAsync(producto);
        return productoModificado > 0;
    }
}