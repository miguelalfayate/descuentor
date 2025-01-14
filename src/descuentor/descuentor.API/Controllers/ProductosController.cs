using Descuentor.API.Servicios;
using Descuentor.Aplicacion.Funcionalidades.Productos.Commands;
using Descuentor.Aplicacion.Funcionalidades.Productos.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Descuentor.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Usuario")]
public class ProductosController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductosController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProducto(CrearProductoCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> ObtenerProductoPaginacion([FromQuery]ObtenerProductosPaginacionQuery query)
    {
        var (productos, numeroProductos) = await _mediator.Send(query);
        
        var numeroProductosPagina = query.NumeroProductosPagina;
        HttpContext.InsertarParametrosPaginacionEnRespuesta(numeroProductos,numeroProductosPagina);
        
        return Ok(productos);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> ObtenerProducto(int id)
    {
        var query = new ObtenerProductoConIdQuery(id);
        var producto = await _mediator.Send(query);
        return Ok(producto);
    }
}