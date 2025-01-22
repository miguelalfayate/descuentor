using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Descuentor.Aplicacion.Funcionalidades.Usuarios.Commands;

public record ActualizarUsuarioCommand(
    string Nombre,
    string Apellidos,
    string Telefono) : IRequest<IActionResult>;