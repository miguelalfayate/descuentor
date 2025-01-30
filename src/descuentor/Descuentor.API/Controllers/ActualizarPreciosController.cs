using Descuentor.Aplicacion.Funcionalidades.HistorialPrecios.Commands;
using Descuentor.Aplicacion.Funcionalidades.NotificacionDescuentos.Commands;
using Descuentor.Aplicacion.Funcionalidades.Productos.Queries;
using Descuentor.Aplicacion.Funcionalidades.Scraper.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Descuentor.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class ActualizarPreciosController : ControllerBase
{
    private readonly IMediator _mediator;

    public ActualizarPreciosController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    public async Task<IActionResult> ActualizarPrecios()
    {
        var productosUrls = await _mediator.Send(new ObtenerTodasUrlQuery());

        var preciosScrape = await _mediator.Send(new ObtenerPreciosScrapeCommand(productosUrls));
        
        // Dictionary<int, decimal> preciosScrape = new();
        // preciosScrape.Add(49, 220);
        // preciosScrape.Add(47, 250);
        // preciosScrape.Add(50, 10);
        // preciosScrape.Add(51, 100);
        
        var precios = await _mediator.Send(new CrearHistorialPreciosCommand(preciosScrape));
        
        var emailsEnviados = await _mediator.Send(new NotificacionDescuentosCommand(preciosScrape));
        
        return Ok(preciosScrape);
    }
}