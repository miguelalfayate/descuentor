using System.ComponentModel.DataAnnotations;

namespace Descuentor.Dominio.Entidades;

public class Configuracion
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Clave { get; set; } = null!;

    [Required, MaxLength(255)]
    public string Descripcion { get; set; } = null!;
}