using Descuentor.Dominio.Interfaces;
using MediatR;

namespace Descuentor.Aplicacion.Funcionalidades.Productos.Queries;

public class ObtenerTodasUrlQueryHandler: IRequestHandler<ObtenerTodasUrlQuery, Dictionary<int, string>>
{
    private readonly IProductoRepository _productoRepository;

    public ObtenerTodasUrlQueryHandler(IProductoRepository productoRepository)
    {
        _productoRepository = productoRepository;
    }

    public async Task<Dictionary<int, string>> Handle(ObtenerTodasUrlQuery request, CancellationToken cancellationToken)
    {
        var productos = await _productoRepository.GetAllProductosWithIdUrlAsync();

        return productos;
    }
}