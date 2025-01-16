using Descuentor.Dominio.Interfaces;
using MediatR;

namespace Descuentor.Aplicacion.Funcionalidades.NotificacionDescuentos.Commands;

public class NotificacionDescuentosCommandHandler : IRequestHandler<NotificacionDescuentosCommand, int>
{

    private readonly INotificacionDescuentosService _notificacionDescuentosService;
    private readonly IProductoRepository _productoRepository;

    public NotificacionDescuentosCommandHandler(INotificacionDescuentosService notificacionDescuentosService, IProductoRepository productoRepository)
    {
        _notificacionDescuentosService = notificacionDescuentosService;
        _productoRepository = productoRepository;
    }

    public async Task<int> Handle(NotificacionDescuentosCommand request, CancellationToken cancellationToken)
    {
        var listaProductos = await _productoRepository.ObtenerProductosConDescuentoAsync(request.HistorialPrecios);
        
        
        return listaProductos.Count;
    }
}