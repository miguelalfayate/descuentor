namespace Descuentor.Aplicacion.Dtos;

public class ProductoDto
{
    public int Id { get; set; }
    public string UrlImagen { get; set; } = null!;
    public string Nombre { get; set; } = null!;
    public string Url { get; set; } = null!;
    public string TiendaNombre { get; set; } = null!;
    public decimal? PrecioInical { get; set; }
    public decimal? PrecioActual { get; set; }
    
}