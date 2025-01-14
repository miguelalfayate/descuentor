using Descuentor.Dominio.Entidades;

namespace Descuentor.Aplicacion.Dtos;

public class ProductoHistorialPreciosDto
{
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string UrlImagen { get; set; } = string.Empty;
    public string TiendaNombre { get; set; } = string.Empty;
    public decimal PrecioInicial { get; set; } = 0;
    public ICollection<HistorialPrecio>? HistorialPrecios { get; set; }
}