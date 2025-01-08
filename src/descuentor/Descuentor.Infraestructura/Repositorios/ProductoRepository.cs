using Descuentor.Dominio.Entidades;
using Descuentor.Dominio.Interfaces;
using Descuentor.Infraestructura.Contextos;
using Microsoft.EntityFrameworkCore;

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

    public async Task<Dictionary<int, string>> GetAllProductosWithIdUrlAsync()
    {
        var productos = _context.Productos
            .ToDictionaryAsync(kvp => kvp.Id, kvp => kvp.Url);
        return await productos;
    }

    public Task<Producto> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Producto producto)
    {
        throw new NotImplementedException();
    }
}