using Descuentor.Dominio.Entidades;

namespace Descuentor.Dominio.Interfaces;

public interface IUsuarioProductoRepository
{
    Task<UsuarioProducto> AddAsync(UsuarioProducto usuarioProducto);
}