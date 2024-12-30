using Descuentor.Dominio.Entidades;

namespace Descuentor.Dominio.Interfaces;

public interface IUsuario
{
    public string Nombre { get; set; }
    public string Apellidos { get; set; }
    DateTime FechaCreacion { get; set; }
    public ICollection<UsuarioConfiguracion>? Configuraciones { get; set; }
    public ICollection<UsuarioProducto>? ArticulosMonitoreados { get; set; }
}