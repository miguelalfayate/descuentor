using Descuentor.Aplicacion.Interfaces;
using Descuentor.Dominio.Entidades;
using Descuentor.Dominio.Interfaces;
using MediatR;

namespace Descuentor.Aplicacion.Funcionalidades.Productos.Commands;

public class CrearProductoCommandHandler : IRequestHandler<CrearProductoCommand, int>
{
    private readonly IProductoRepository _productoRepository;
    private readonly IUsuarioProductoRepository _usuarioProductoRepository;
    private readonly ICurrentUser _currentUser;

    public CrearProductoCommandHandler(IProductoRepository productoRepository, ICurrentUser currentUser, IUsuarioProductoRepository usuarioProductoRepository)
    {
        _productoRepository = productoRepository;
        _currentUser = currentUser;
        _usuarioProductoRepository = usuarioProductoRepository;
    }

    public async Task<int> Handle(CrearProductoCommand request, CancellationToken cancellationToken)
    {
        var producto = new Producto
        {
            Nombre = request.Nombre,
            Url = request.Url,
            Descripcion = request.Descripcion,
            UrlImagen = request.UrlImagen,
            TiendaOnlineId = request.TiendaOnlineId,
            PrecioActual = request.PrecioActual
        };

        var result = await _productoRepository.AddAsync(producto);
        var userId = _currentUser.Id;
        if (userId != null)
        {
            var usuarioProducto = new UsuarioProducto
            {
                UsuarioAplicacionId = int.Parse(userId),
                ProductoId = result.Id
            };
            await _usuarioProductoRepository.AddAsync(usuarioProducto);
        }
        return result.Id;
    }
}