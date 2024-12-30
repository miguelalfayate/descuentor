using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Descuentor.Dominio.Entidades;

public class HistorialPrecio
{
    [Key]
    public int Id { get; set; }

    [Required]
    public DateTime FechaConsulta { get; set; }

    [Required]
    public decimal Precio { get; set; }

    [ForeignKey(nameof(Producto))]
    [Required]
    public int ProductoId { get; set; }
    [NotMapped]
    public Producto Producto { get; set; } = null!;
}