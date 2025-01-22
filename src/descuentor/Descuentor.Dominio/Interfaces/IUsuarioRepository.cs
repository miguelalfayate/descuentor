using Microsoft.AspNetCore.Mvc;

namespace Descuentor.Dominio.Interfaces;

public interface IUsuarioRepository
{
    Task<IActionResult> CrearUsuarioConRol(string email, string password);
    Task<IActionResult> ModificarUsuario(int id, string nombre, string apellidos, string telefono);
    Task<IActionResult> ObtenerUsuarioPorId(int id);
}