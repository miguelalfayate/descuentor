using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Descuentor.Dominio.Interfaces;

namespace Descuentor.Dominio.Entidades;

public class UsuarioConfiguracion
{
    [Key]
    public int Id { get; set; }

    [ForeignKey(nameof(UsuarioAplicacion))]
    [Required]
    public int UsuarioAplicacionId { get; set; }
    [NotMapped]
    public IUsuario UsuarioAplicacion { get; set; } = null!;

    [ForeignKey(nameof(Configuracion))]
    [Required]
    public int ConfiguracionId { get; set; }
    public Configuracion Configuracion { get; set; } = null!;

    public string? Valor { get; set; }
}