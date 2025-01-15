namespace Descuentor.Aplicacion.Dtos;

public class ProductoEditarDto
{
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public string UrlImagen { get; set; } = null!;
        public string Url { get; set; } = null!;
        public decimal PrecioInicial { get; set; }
}