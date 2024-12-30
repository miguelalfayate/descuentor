using Descuentor.Dominio.Entidades;
using Descuentor.Infraestructura.ModelosIdentity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Descuentor.Infraestructura.Contextos;

public class ApplicationDbContext : IdentityDbContext<UsuarioAplicacion, RolAplicacion, int>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<Producto> Productos { get; set; }
    public DbSet<UsuarioProducto> UsuariosProductos { get; set; }
    public DbSet<HistorialPrecio> HistorialesPrecio { get; set; }
    public DbSet<Configuracion> Configuraciones { get; set; }
    public DbSet<TiendaOnline> TiendasOnline { get; set; }
    public DbSet<UsuarioConfiguracion> UsuariosConfiguraciones { get; set; }
    
    
    
}