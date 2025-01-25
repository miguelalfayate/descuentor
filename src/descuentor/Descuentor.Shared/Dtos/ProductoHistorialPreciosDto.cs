//using Descuentor.Dominio.Entidades;

namespace Descuentor.Shared.Dtos;

public class ProductoHistorialPreciosDto
{
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string UrlImagen { get; set; } = string.Empty;
    public string TiendaNombre { get; set; }  = string.Empty;
    public int TiendaId { get; set; }
    public decimal PrecioInicial { get; set; } = 0;
    public ICollection<HistorialPrecioDto>? HistorialPrecios { get; set; }
}