using Descuentor.Dominio.Interfaces;
using Descuentor.Infraestructura.Contextos;
using Descuentor.Infraestructura.ModelosIdentity;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Descuentor.Infraestructura.Repositorios;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<UsuarioAplicacion> _userManager;

    public UsuarioRepository(ApplicationDbContext context, UserManager<UsuarioAplicacion> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<IUsuario> ObtenerUsuarioByEmail(string email)
    {
        var emailFormateado = email.ToUpper();
        var usuario = await _context.Users.FirstOrDefaultAsync(u => u.NormalizedEmail == emailFormateado);
        return usuario!;
    }

    public async Task<IActionResult> CrearUsuarioConRol(string email, string password)
    {
        var user = new UsuarioAplicacion()
        {
            UserName = email,
            Email = password
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