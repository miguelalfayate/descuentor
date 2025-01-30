using Descuentor.Dominio.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Descuentor.Aplicacion.Funcionalidades.Usuarios.Queries;

public class ObtenerUsuarioQueryHandler : IRequestHandler<ObtenerUsuarioQuery, IActionResult>
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly ICurrentUser _currentUser;

    public ObtenerUsuarioQueryHandler(IUsuarioRepository usuarioRepository, ICurrentUser currentUser)
    {
        _usuarioRepository = usuarioRepository;
        _currentUser = currentUser;
    }

    public async Task<IActionResult> Handle(ObtenerUsuarioQuery request, CancellationToken cancellationToken)
    {
        var usuarioAplicacionId = !string.IsNullOrEmpty(_currentUser.Id) ? int.Parse(_currentUser.Id!) : 1;
        var resultado = await _usuarioRepository.ObtenerUsuarioPorId(usuarioAplicacionId);
        
        return new OkObjectResult(resultado);
    }
}