using Descuentor.Dominio.Entidades;

namespace Descuentor.Dominio.Interfaces;

public interface INotificacionDescuentosService
{
    Task<bool> NotificarDescuentoEmailAsync(int usuarioId, List<Producto> productos);
}