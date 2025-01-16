using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Descuentor.Dominio.Interfaces;

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

    public decimal? PrecioInicial { get; set; }

    [ForeignKey(nameof(TiendaOnline))]
    public int TiendaOnlineId { get; set; }
    public TiendaOnline TiendaOnline { get; set; } = null!;
    
    [ForeignKey(nameof(Usuario))]
    public int UsuarioAplicacionId { get; set; }
    public IUsuario Usuario { get; set; } = null!;

    // Relación con HistorialPrecio
    
    public ICollection<HistorialPrecio>? HistorialPrecios { get; set; }
    
}
