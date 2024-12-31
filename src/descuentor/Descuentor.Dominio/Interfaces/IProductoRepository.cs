using Descuentor.Dominio.Entidades;

namespace Descuentor.Dominio.Interfaces;

public interface IProductoRepository
{
    Task<Producto> AddAsync(Producto producto);
}