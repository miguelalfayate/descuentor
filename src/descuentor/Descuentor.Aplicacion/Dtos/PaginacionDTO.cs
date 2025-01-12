namespace Descuentor.Aplicacion.Dtos;

public class PaginacionDto
{
    public int IdUsuario { get; set; } = 1;
    public int Pagina { get; set; } = 1;
    public int CantidadRegistros { get; set; } = 10;
    public string? CampoOrden { get; set; } = null;
    public bool OrdenAscendente { get; set; } = true;
}