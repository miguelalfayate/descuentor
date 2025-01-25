namespace Descuentor.Shared.Dtos;

public class ProductoDto
{
    public int Id { get; set; }
    public string UrlImagen { get; set; } = null!;
    public string Nombre { get; set; } = null!;
    public string Url { get; set; } = null!;
    public string TiendaNombre { get; set; } = null!;
    public decimal? PrecioInicial { get; set; }
    public decimal? PrecioActual { get; set; }
    public decimal? PorcentajeVariacion { get; set; }
    
}