using System.ComponentModel.DataAnnotations;

namespace Descuentor.Dominio.Entidades;

public class TiendaOnline
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [StringLength(50)]
    public string? Nombre { get; set; }
    
    [Required, Url]
    [StringLength(255)]
    public string? UrlProducto { get; set; }
    
    public ICollection<Producto>? Productos { get; set; }
}