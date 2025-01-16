using Descuentor.Dominio.Entidades;

namespace Descuentor.Aplicacion.Dtos;

public class ProductosConDescuentoDto
{
    public string Nombre { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string UrlImagen { get; set; } = string.Empty;
    public TiendaOnline Tienda { get; set; } = null!;
    public decimal? PrecioInicial { get; set; } = 0;
    public decimal PrecioActual { get; set; } = 0;
}