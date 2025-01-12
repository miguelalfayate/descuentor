using Descuentor.Dominio.Entidades;

namespace Descuentor.Dominio.Interfaces;

public interface IProductoRepository
{
    Task<Producto> AddAsync(Producto producto);
    Task<Dictionary<int, string>> GetAllProductosWithIdUrlAsync();
    Task<Producto> GetByIdAsync(int id);
    Task UpdateAsync(Producto producto);
    Task<(List<Producto> Productos, int TotalRegistros)> ObtenerProductosPaginacionConId(
        int pagina, 
        int cantidadRegistros,
        string? campoOrden, 
        bool ordenAscendente,
        int idUsuario);
}