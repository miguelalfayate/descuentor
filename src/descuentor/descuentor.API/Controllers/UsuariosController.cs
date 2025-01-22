using Descuentor.Aplicacion.Dtos;
using Descuentor.Aplicacion.Funcionalidades.Usuarios.Commands;
using Descuentor.Aplicacion.Funcionalidades.Usuarios.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Descuentor.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsuariosController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    public async Task<IActionResult> ObtenerUsuario()
    {
        var result = await _mediator.Send(new ObtenerUsuarioQuery());
        return result;
    }

    [HttpPost]
    public async Task<IActionResult> CrearUsuario(CrearUsuarioCommand command)
    {
        var result = await _mediator.Send(command);
        return result;
    }
    
    [HttpPut]
    [Authorize(Roles = "Usuario")]
    public async Task<IActionResult> ActualizarUsuario([FromBody]ActualizarUsuarioCommand command)
    {
        var result = await _mediator.Send(command);
        
        return result;
    }
}