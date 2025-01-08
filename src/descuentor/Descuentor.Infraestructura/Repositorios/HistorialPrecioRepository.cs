using Descuentor.Dominio.Entidades;
using Descuentor.Dominio.Interfaces;
using Descuentor.Infraestructura.Contextos;

namespace Descuentor.Infraestructura.Repositorios;

public class HistorialPrecioRepository : IHistorialPrecioRepository
{
    private readonly ApplicationDbContext _context;

    public HistorialPrecioRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<HistorialPrecio> AddAsync(HistorialPrecio historialPrecio)
    {
        _context.HistorialesPrecio.Add(historialPrecio);
        await _context.SaveChangesAsync();
        return historialPrecio;
    }
}