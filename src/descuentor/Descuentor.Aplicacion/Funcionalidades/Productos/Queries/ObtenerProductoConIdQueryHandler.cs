using Descuentor.Aplicacion.Dtos;
using Descuentor.Dominio.Interfaces;
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
        
        ProductoHistorialPreciosDto productoVisualizacion = new()
        {
            Nombre = producto.Nombre ?? "",
            Descripcion = producto.Descripcion ?? "",
            Url = producto.Url ?? "",
            UrlImagen = producto.UrlImagen ?? "",
            PrecioInicial = producto.PrecioInicial ?? 0,
            HistorialPrecios = producto.HistorialPrecios?.ToList(),
            Tienda = producto.TiendaOnline
        };
        
        return productoVisualizacion;
    }
}