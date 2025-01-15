using Descuentor.Aplicacion.Dtos;
using Descuentor.Aplicacion.Interfaces;
using Descuentor.Dominio.Interfaces;
using MediatR;

namespace Descuentor.Aplicacion.Funcionalidades.Productos.Queries;

public class
    ObtenerProductosPaginacionQueryHandler : IRequestHandler<ObtenerProductosPaginacionQuery, (List<ProductoDto>, int)>
{
    private readonly IProductoRepository _productoRepository;
    private readonly ICurrentUser _currentUser;

    public ObtenerProductosPaginacionQueryHandler(IProductoRepository productoRepository, ICurrentUser currentUser)
    {
        _productoRepository = productoRepository;
        _currentUser = currentUser;
    }

    public async Task<(List<ProductoDto>, int)> Handle(ObtenerProductosPaginacionQuery request,
        CancellationToken cancellationToken)
    {
        int idUsuario = 1;

        try
        {
            idUsuario = int.Parse(_currentUser.Id!);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        var (productos, numeroRegistros) = await _productoRepository.ObtenerProductosPaginacionConId(
            request.Pagina,
            request.NumeroProductosPagina,
            request.CampoOrden,
            request.OrdenAscendente,
            idUsuario);

        List<ProductoDto> productosVisualizacion = productos
            .Select(p => new ProductoDto
            {
                Id = p.Id,
                Nombre = p.Nombre!,
                Url = p.Url,
                UrlImagen = p.UrlImagen!,
                PrecioInicial = p.PrecioInicial,
                PrecioActual = p.HistorialPrecios?.FirstOrDefault()!.Precio,
                TiendaNombre = p.TiendaOnline.Nombre!,
                PorcentajeVariacion = Math.Round(((p.HistorialPrecios?.FirstOrDefault()?.Precio ?? 0) - (p.PrecioInicial ?? 0)) / (p.PrecioInicial ?? 1) * 100, 2)
            }).ToList();

        return (productosVisualizacion, numeroRegistros);
    }
}