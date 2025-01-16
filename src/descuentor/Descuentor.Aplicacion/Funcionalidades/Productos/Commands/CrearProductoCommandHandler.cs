using Descuentor.Aplicacion.Interfaces;
using Descuentor.Dominio.Entidades;
using Descuentor.Dominio.Interfaces;
using MediatR;

namespace Descuentor.Aplicacion.Funcionalidades.Productos.Commands;

public class CrearProductoCommandHandler : IRequestHandler<CrearProductoCommand, int>
{
    private readonly IProductoRepository _productoRepository;
    private readonly IHistorialPrecioRepository _historialPrecioRepository;
    private readonly ICurrentUser _currentUser;

    public CrearProductoCommandHandler(IProductoRepository productoRepository, ICurrentUser currentUser, IHistorialPrecioRepository historialPrecioRepository)
    {
        _productoRepository = productoRepository;
        _currentUser = currentUser;
        _historialPrecioRepository = historialPrecioRepository;
    }

    public async Task<int> Handle(CrearProductoCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUser.Id;
        
        var producto = new Producto
        {
            Nombre = request.Nombre,
            Url = request.Url,
            Descripcion = request.Descripcion,
            UrlImagen = request.UrlImagen,
            TiendaOnlineId = request.TiendaOnlineId,
            PrecioInicial = request.PrecioInicial,
            UsuarioAplicacionId = !string.IsNullOrEmpty(userId) ? int.Parse(userId!) : 1
        };

        var result = await _productoRepository.AddAsync(producto);
        
        var historialPrecio = new HistorialPrecio
        {
            ProductoId = result.Id,
            Precio = request.PrecioInicial ?? 0m,
            FechaConsulta = DateTime.UtcNow
        };
        
        await _historialPrecioRepository.AddAsync(historialPrecio);
        
        return result.Id;
    }
}