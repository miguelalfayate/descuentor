using Descuentor.Dominio.Interfaces;
using Descuentor.Infraestructura.ModelosIdentity;
using Descuentor.Shared.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Descuentor.Infraestructura.Repositorios;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly UserManager<UsuarioAplicacion> _userManager;

    public UsuarioRepository(UserManager<UsuarioAplicacion> userManager)
    {
        _userManager = userManager;
    }

    public async Task<object> CrearUsuarioConRol(string email, string password)
    {
        var user = new UsuarioAplicacion()
        {
            UserName = email,
            Email = email
            // Otros campos que necesites
        };
        
        var result = await _userManager.CreateAsync(user, password);
        
        if (!result.Succeeded)
        {
            return new BadRequestObjectResult(result.Errors);
        }
        await _userManager.AddToRoleAsync(user, "Usuario"); // O el rol que desees

        return new OkObjectResult(user);
    }

    public async Task<object> ModificarUsuario(int id, string nombre, string apellidos, string telefono)
    {
        var usuario = _userManager.Users.FirstOrDefault(u => u.Id == id);
        
        usuario!.Nombre = nombre;
        usuario.Apellidos = apellidos;
        usuario.PhoneNumber = telefono;
        
        var result = await _userManager.UpdateAsync(usuario);
        
        if (!result.Succeeded)
        {
            return new BadRequestObjectResult(result.Errors);
        }

        return new OkObjectResult(usuario);
    }

    public async Task<object> ObtenerUsuarioPorId(int id)
    {
        // var usuario = _userManager.Users.FirstOrDefault(u => u.Id == id);
        var usuario = await _userManager.FindByIdAsync(id.ToString());
        
        var usuarioEditarDto = new UsuarioEditarDto()
        {
            Nombre = usuario.Nombre,
            Apellidos = usuario.Apellidos,
            Telefono = usuario.PhoneNumber
        };

        return new OkObjectResult(usuarioEditarDto);
    }
}