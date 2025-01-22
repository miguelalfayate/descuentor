namespace Descuentor.Aplicacion.Dtos;

public class ApiOkResponse
{
    public UsuarioEditarDto Value { get; set; }
    public List<object> Formatters { get; set; }
    public List<object> ContentTypes { get; set; }
    public object DeclaredType { get; set; }
    public int StatusCode { get; set; }
}