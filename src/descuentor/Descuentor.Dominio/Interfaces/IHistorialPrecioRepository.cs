using Descuentor.Dominio.Entidades;

namespace Descuentor.Dominio.Interfaces;

public interface IHistorialPrecioRepository
{
    Task<HistorialPrecio> AddAsync(HistorialPrecio historialPrecio);
}