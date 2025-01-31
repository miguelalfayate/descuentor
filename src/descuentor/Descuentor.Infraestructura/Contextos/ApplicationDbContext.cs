using Descuentor.Dominio.Entidades;
using Descuentor.Infraestructura.InsercionesDatos;
using Descuentor.Infraestructura.ModelosIdentity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace Descuentor.Infraestructura.Contextos;

public class ApplicationDbContext : IdentityDbContext<UsuarioAplicacion, RolAplicacion, int>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        try
        {
            var databaseCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
            if (databaseCreator != null)
            {
                // Create the database if it doesn't exist
                if (!databaseCreator.CanConnect())
                {
                    databaseCreator.Create();
                }

                // Create tables if they don't exist
                if (!databaseCreator.HasTables())
                {
                    databaseCreator.CreateTables();
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Database creation failed: {ex.Message}");
        }
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        // Configuración para la inserción de datos de las tablas de Roles y Usuarios
        // y la relación entre ellos
        builder.ApplyConfiguration(new RolesSeed());
        builder.ApplyConfiguration(new UsuariosSeed());
        builder.ApplyConfiguration(new UsuariosRolesSeed());
        builder.ApplyConfiguration(new TiendasOnlineSeed());
        
        // Mapeo IUsuario a UsuarioAplicacion
        builder.Entity<Producto>()
            .HasOne(p => (UsuarioAplicacion)p.Usuario)
            .WithMany()
            .HasForeignKey(p => p.UsuarioAplicacionId);
    }
    
    public DbSet<Producto> Productos { get; set; }
    public DbSet<HistorialPrecio> HistorialesPrecio { get; set; }
    public DbSet<Configuracion> Configuraciones { get; set; }
    public DbSet<TiendaOnline> TiendasOnline { get; set; }
    public DbSet<UsuarioConfiguracion> UsuariosConfiguraciones { get; set; }
    
    
    
}