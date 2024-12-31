using Descuentor.Dominio.Entidades;
using Descuentor.Dominio.Interfaces;
using Descuentor.Infraestructura.Contextos;

namespace Descuentor.Infraestructura.Repositorios;

public class ProductoRepository : IProductoRepository
{
    private readonly ApplicationDbContext _context;

    public ProductoRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Producto> AddAsync(Producto producto)
    {
        _context.Productos.Add(producto);
        await _context.SaveChangesAsync();
        return producto;
    }
}