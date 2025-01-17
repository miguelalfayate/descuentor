using Descuentor.Dominio.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Descuentor.Aplicacion.Funcionalidades.Usuarios.Commands;

public class CrearUsuarioCommandHandler : IRequestHandler<CrearUsuarioCommand, IActionResult>
{
    private readonly IUsuarioRepository _usuarioRepository;

    public CrearUsuarioCommandHandler(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }
    
    public async Task<IActionResult> Handle(CrearUsuarioCommand request, CancellationToken cancellationToken)
    {
        var respuesta = await _usuarioRepository.CrearUsuarioConRol(request.Email, request.Password);
        
        return respuesta;
    }
}