namespace Descuentor.Dominio.Interfaces;

public interface IUsuarioRepository
{
    Task<object> CrearUsuarioConRol(string email, string password);
    Task<object> ModificarUsuario(int id, string nombre, string apellidos, string telefono);
    Task<object> ObtenerUsuarioPorId(int id);
}