using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Descuentor.Dominio.Entidades;

public class Producto
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string? Nombre { get; set; }
    
    public string? Descripcion { get; set; }

    [Required]
    public string Url { get; set; } = null!;

    public string? UrlImagen { get; set; }

    public decimal? PrecioActual { get; set; }

    [ForeignKey(nameof(TiendaOnline))]
    public int TiendaOnlineId { get; set; }
    [NotMapped]
    public TiendaOnline TiendaOnline { get; set; } = null!;

    // Relación con HistorialPrecio
    
    public ICollection<HistorialPrecio>? HistorialPrecios { get; set; }
    
    // Relación con UsuarioArticulo
    public ICollection<UsuarioProducto>? UsuariosMonitoreadores { get; set; }
}
