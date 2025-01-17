using Microsoft.AspNetCore.Mvc;

namespace Descuentor.Dominio.Interfaces;

public interface IUsuarioRepository
{
    Task<IUsuario> ObtenerUsuarioByEmail(string email);
    Task<IActionResult> CrearUsuarioConRol(string email, string password);
}