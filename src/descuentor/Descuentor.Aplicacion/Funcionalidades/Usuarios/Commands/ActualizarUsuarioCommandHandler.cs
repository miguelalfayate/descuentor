using Descuentor.Dominio.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Descuentor.Aplicacion.Funcionalidades.Usuarios.Commands;

public class ActualizarUsuarioCommandHandler : IRequestHandler<ActualizarUsuarioCommand, IActionResult>
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly ICurrentUser _currentUser;

    public ActualizarUsuarioCommandHandler(IUsuarioRepository usuarioRepository, ICurrentUser currentUser)
    {
        _usuarioRepository = usuarioRepository;
        _currentUser = currentUser;
    }
    
    public async Task<IActionResult> Handle(ActualizarUsuarioCommand request, CancellationToken cancellationToken)
    {
        var usuarioAplicacionId = !string.IsNullOrEmpty(_currentUser.Id) ? int.Parse(_currentUser.Id!) : 1;
        
        var respuesta = await _usuarioRepository.ModificarUsuario(usuarioAplicacionId, request.Nombre, request.Apellidos, request.Telefono);
        
        return respuesta;
    }
}