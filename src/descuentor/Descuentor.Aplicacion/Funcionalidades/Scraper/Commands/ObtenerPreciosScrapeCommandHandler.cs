using Descuentor.Dominio.Interfaces;
using MediatR;

namespace Descuentor.Aplicacion.Funcionalidades.Scraper.Commands;

public class ObtenerPreciosScrapeCommandHandler : IRequestHandler<ObtenerPreciosScrapeCommand, Dictionary<int, decimal>>
{
    private readonly IScrapingService _scrapingService;

    public ObtenerPreciosScrapeCommandHandler(IScrapingService scrapingService)
    {
        _scrapingService = scrapingService;
    }

    public Task<Dictionary<int, decimal>> Handle(ObtenerPreciosScrapeCommand request, CancellationToken cancellationToken)
    {
        var preciosProductos = _scrapingService.ObtenerPrecios(request.ProductosUrl);
        return preciosProductos;
    }
}