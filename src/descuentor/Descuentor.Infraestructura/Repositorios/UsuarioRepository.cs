using System.Security.Claims;
using Descuentor.Dominio.Interfaces;
using Descuentor.Infraestructura.ModelosIdentity;
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

    public async Task<IActionResult> CrearUsuarioConRol(string email, string password)
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
}