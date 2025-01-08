using MediatR;

namespace Descuentor.Aplicacion.Funcionalidades.Scraper.Commands;

public record ObtenerPreciosScrapeCommand(
    Dictionary<int, string> ProductosUrl) : IRequest<Dictionary<int, decimal>>;