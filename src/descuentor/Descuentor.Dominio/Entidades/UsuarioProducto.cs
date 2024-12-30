using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Descuentor.Dominio.Interfaces;

namespace Descuentor.Dominio.Entidades;

public class UsuarioProducto
{
    [Key]
    public int Id { get; set; }

    [ForeignKey(nameof(UsuarioAplicacion))]
    public int UsuarioAplicacionId { get; set; }
    [NotMapped]
    public IUsuario UsuarioAplicacion { get; set; } = null!;

    [ForeignKey(nameof(Producto))]
    [Required]
    public int ProductoId { get; set; }
    public Producto Producto { get; set; } = null!;

    public DateTime FechaInicioMonitoreo { get; set; } = DateTime.UtcNow;
}