using System.ComponentModel.DataAnnotations;
using Descuentor.Dominio.Entidades;
using Descuentor.Dominio.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Descuentor.Infraestructura.ModelosIdentity;

public class UsuarioAplicacion : IdentityUser<int>, IUsuario
{
    [MaxLength(50)]
    public string? Nombre { get; set; }
    
    [MaxLength(100)]
    public string? Apellidos { get; set; }

    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

    // Relación con ConfiguracionUsuario
    public ICollection<UsuarioConfiguracion>? Configuraciones { get; set; }
    
}
    
