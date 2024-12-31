using Descuentor.Aplicacion.Funcionalidades.Productos.Commands;
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
}