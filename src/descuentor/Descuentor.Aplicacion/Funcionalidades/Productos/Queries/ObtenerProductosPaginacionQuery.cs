using Descuentor.Aplicacion.Dtos;
using MediatR;

namespace Descuentor.Aplicacion.Funcionalidades.Productos.Queries;

public class ObtenerProductosPaginacionQuery(
) : IRequest<(List<ProductoDto>, int)>
{
    public int Pagina { get; set; } = 1;
    public int NumeroProductosPagina { get; set; } = 5;
    public string? CampoOrden { get; set; } = null;
    public bool OrdenAscendente { get; set; } = true;
}