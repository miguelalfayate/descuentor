using Descuentor.Dominio.Interfaces;
using Descuentor.Shared.Dtos;
using MediatR;

namespace Descuentor.Aplicacion.Funcionalidades.Productos.Queries;

public class ObtenerProductoConIdQueryHandler : IRequestHandler<ObtenerProductoConIdQuery, ProductoHistorialPreciosDto>
{
    private readonly IProductoRepository _productoRepository;

    public ObtenerProductoConIdQueryHandler(IProductoRepository productoRepository)
    {
        _productoRepository = productoRepository;
    }

    public async Task<ProductoHistorialPreciosDto> Handle(ObtenerProductoConIdQuery request, CancellationToken cancellationToken)
    {
        var producto = await _productoRepository.ObtenerProductoConHistorialById(request.Id);
        
        var historialPrecios = producto.HistorialPrecios?.Select(hp => new HistorialPrecioDto
        {
            FechaConsulta = hp.FechaConsulta,
            Precio = hp.Precio
        }).ToList();
        
        ProductoHistorialPreciosDto productoVisualizacion = new()
        {
            Nombre = producto.Nombre ?? "",
            Descripcion = producto.Descripcion ?? "",
            Url = producto.Url ?? "",
            UrlImagen = producto.UrlImagen ?? "",
            PrecioInicial = producto.PrecioInicial ?? 0,
            HistorialPrecios = historialPrecios,
            TiendaNombre = producto.TiendaOnline.Nombre ?? "",
            TiendaId = producto.TiendaOnline.Id
        };
        
        return productoVisualizacion;
    }
}