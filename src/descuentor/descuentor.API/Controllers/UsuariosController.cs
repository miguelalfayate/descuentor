using Descuentor.Aplicacion.Funcionalidades.Usuarios.Commands;
using MediatR;
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

    [HttpPost]
    public async Task<IActionResult> AplicarRolUsuario(CrearUsuarioCommand command)
    {
        var result = await _mediator.Send(command);
        return result;
    }
}