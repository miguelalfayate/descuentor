using Descuentor.Dominio.Interfaces;
using Descuentor.Infraestructura.Contextos;
using Microsoft.EntityFrameworkCore;

namespace Descuentor.Infraestructura.Repositorios;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly ApplicationDbContext _context;

    public UsuarioRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IUsuario> ObtenerUsuarioById(int id)
    {
        var usuario = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        return usuario!;
    }
}