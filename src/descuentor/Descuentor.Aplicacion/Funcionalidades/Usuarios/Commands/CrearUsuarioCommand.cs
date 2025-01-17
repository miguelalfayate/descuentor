using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Descuentor.Aplicacion.Funcionalidades.Usuarios.Commands;

public record CrearUsuarioCommand(
    string Email,
    string Password) : IRequest<IActionResult>;