using Descuentor.Aplicacion.Dtos;
using Descuentor.Dominio.Entidades;
using Descuentor.Dominio.Interfaces;
using Descuentor.Infraestructura.Contextos;
using Descuentor.Infraestructura.Servicios;
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

    public async Task<Producto> ObtenerProductoConHistorialById(int id)
    {
        var producto = await _context.Productos
            .Include(p => p.TiendaOnline)
            .Include(p => p.HistorialPrecios!.Take(50))
            .FirstOrDefaultAsync(p => p.Id == id);
        if (producto != null) return producto;
        return new Producto();
    }

    public async Task<int> UpdateAsync(Producto producto)
    {
        _context.Productos.Update(producto);
        var actualizacion = await _context.SaveChangesAsync();
        return actualizacion;
    }

    public async Task<(List<Producto> Productos, int TotalRegistros)> ObtenerProductosPaginacionConId(
        int pagina, 
        int cantidadRegistros, 
        string? campoOrden, 
        bool ordenAscendente, 
        int idUsuario)
    {
        var queryable = _context.Productos
            .Where(p => p.UsuariosMonitoreadores!.Any(up => up.UsuarioAplicacionId == idUsuario))
            .Include(p => p.TiendaOnline)
            .Include(p => p.HistorialPrecios!.OrderByDescending(hp => hp.FechaConsulta).Take(1))
            .AsQueryable();
        
        // Calcular el total de registros antes de la paginación
        var totalRegistros = await queryable.CountAsync();
        
        // Ordenar

        if (!string.IsNullOrEmpty(campoOrden))
        {
            var campo = campoOrden.ToLower();
            if (campo == "nombre")
            {
                if (ordenAscendente)
                    queryable = queryable.OrderBy(x => x.Nombre);
                else
                    queryable = queryable.OrderByDescending(x => x.Nombre);
            }
            else if (campo == "tiendanombre")
            {
                if (ordenAscendente)
                    queryable = queryable.OrderBy(x => x.TiendaOnline.Nombre);
                else
                    queryable = queryable.OrderByDescending(x => x.TiendaOnline.Nombre);
            }
            else if (campo == "precioinicial")
            {
                if (ordenAscendente)
                    queryable = queryable.OrderBy(x => x.PrecioInicial);
                else
                    queryable = queryable.OrderByDescending(x => x.PrecioInicial);
            }
            else if (campo == "precioactual")
            {
                if (ordenAscendente)
                    //queryable = queryable.OrderBy(x => x.HistorialPrecios!.Select(hp => hp.Precio).FirstOrDefault());
                    queryable = queryable.OrderBy(x => x.HistorialPrecios!.OrderByDescending(hp => hp.FechaConsulta).FirstOrDefault()!.Precio);
                else
                    //queryable = queryable.OrderByDescending(x => x.HistorialPrecios!.Select(hp => hp.Precio).FirstOrDefault());
                    queryable = queryable.OrderByDescending(x => x.HistorialPrecios!.OrderByDescending(hp => hp.FechaConsulta).FirstOrDefault()!.Precio);
            }
            else if (campo == "porcentajevariacion")
            {
                if (ordenAscendente)
                    queryable = queryable.OrderBy(x => (x.HistorialPrecios!.OrderByDescending(hp => hp.FechaConsulta).FirstOrDefault()!.Precio-x.PrecioInicial)/x.PrecioInicial);
                else
                    queryable = queryable.OrderByDescending(x => (x.HistorialPrecios!.OrderByDescending(hp => hp.FechaConsulta).FirstOrDefault()!.Precio-x.PrecioInicial)/x.PrecioInicial);
                ;
            }
            // Si no coincide con ningún campo, queryable se queda sin cambios
        }
        
        var productos = await queryable.Paginar(cantidadRegistros, pagina).ToListAsync();
        return (productos, totalRegistros);
    }

    public async Task<bool> EliminarProductoByIdAsync(int id)
    {
        var producto = await _context.Productos.Where(p => p.Id == id).FirstOrDefaultAsync();
        _context.Productos.Remove(producto!);
        
        var eliminacion = await _context.SaveChangesAsync();
        return eliminacion > 0;
    }

    public async Task<List<Producto>> ObtenerProductosConDescuentoAsync(Dictionary<int, decimal> historialPrecios)
    {
        var productos = await _context.Productos
            .Where(p => p.PrecioInicial > p.HistorialPrecios!.OrderByDescending(hp => hp.FechaConsulta).FirstOrDefault()!.Precio)
            .Include(p => p.HistorialPrecios!.OrderByDescending(hp => hp.FechaConsulta).Take(1))
            .Include(p => p.UsuariosMonitoreadores!)
            .ThenInclude(p => p.UsuarioAplicacion)
            .ToListAsync();
        return productos;
    }
}