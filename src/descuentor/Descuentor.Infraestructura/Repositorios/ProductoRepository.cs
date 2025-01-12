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

    public Task<Producto> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Producto producto)
    {
        throw new NotImplementedException();
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

            if (campo == "id")
            {
                if (ordenAscendente)
                    queryable = queryable.OrderBy(x => x.Id);
                else
                    queryable = queryable.OrderByDescending(x => x.Id);
            }
            else if (campo == "nombre")
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
                    queryable = queryable.OrderBy(x => x.HistorialPrecios!.FirstOrDefault()!.Precio);
                else
                    queryable = queryable.OrderByDescending(x => x.HistorialPrecios!.FirstOrDefault()!.Precio);
            }
            else if (campo == "porcentajevariacion")
            {
                if (ordenAscendente)
                    queryable = queryable.OrderBy(x => (x.HistorialPrecios!.FirstOrDefault()!.Precio-x.PrecioInicial)/x.PrecioInicial);
                else
                    queryable = queryable.OrderByDescending(x => (x.HistorialPrecios!.FirstOrDefault()!.Precio-x.PrecioInicial)/x.PrecioInicial);
                ;
            }
            // Si no coincide con ningún campo, queryable se queda sin cambios
        }
        
        var productos = await queryable.Paginar(cantidadRegistros, pagina).ToListAsync();
        return (productos, totalRegistros);
    }
}