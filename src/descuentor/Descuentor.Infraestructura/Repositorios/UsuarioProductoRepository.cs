using Descuentor.Dominio.Entidades;
using Descuentor.Dominio.Interfaces;
using Descuentor.Infraestructura.Contextos;

namespace Descuentor.Infraestructura.Repositorios;

public class UsuarioProductoRepository :IUsuarioProductoRepository
{
    private readonly ApplicationDbContext _context;

    public UsuarioProductoRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<UsuarioProducto> AddAsync(UsuarioProducto usuarioProducto)
    {
        _context.UsuariosProductos.Add(usuarioProducto);
        await _context.SaveChangesAsync();
        return usuarioProducto;
    }
}