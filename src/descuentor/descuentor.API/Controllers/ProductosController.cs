using Descuentor.API.Servicios;
using Descuentor.Aplicacion.Funcionalidades.Productos.Commands;
using Descuentor.Aplicacion.Funcionalidades.Productos.Queries;
using Descuentor.Shared.Dtos;
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
    
    [HttpPut("{id}")]
    public async Task<IActionResult> ActualizarProducto(int id, ProductoEditarDto producto)
    {
        var productoCommand = new ActualizarProductoCommand(
            id,
            producto.Nombre,
            producto.Descripcion,
            producto.TiendaOnlineId,
            producto.UrlImagen,
            producto.Url,
            producto.PrecioInicial
        );

        var result = await _mediator.Send(productoCommand);
        return Ok(result);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> EliminarProducto(int id)
    {
        var command = new EliminarProductoCommand(id);
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}