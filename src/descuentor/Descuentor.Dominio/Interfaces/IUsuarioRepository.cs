namespace Descuentor.Dominio.Interfaces;

public interface IUsuarioRepository
{
    Task<IUsuario> ObtenerUsuarioById(int id);
}