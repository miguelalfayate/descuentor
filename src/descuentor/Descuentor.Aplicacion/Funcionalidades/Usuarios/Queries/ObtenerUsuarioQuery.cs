using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Descuentor.Aplicacion.Funcionalidades.Usuarios.Queries;

public record ObtenerUsuarioQuery() : IRequest<IActionResult>;