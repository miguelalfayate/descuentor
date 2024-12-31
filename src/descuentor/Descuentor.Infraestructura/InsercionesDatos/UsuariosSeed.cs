using Descuentor.Infraestructura.Contextos;
using Descuentor.Infraestructura.ModelosIdentity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.DependencyInjection;

namespace Descuentor.Infraestructura.InsercionesDatos;

public class UsuariosSeed : IEntityTypeConfiguration<UsuarioAplicacion>
{
    public void Configure(EntityTypeBuilder<UsuarioAplicacion> builder)
    {
        var admin = new UsuarioAplicacion
        {
            Id = 1,
            UserName = "admin@domain.com",
            NormalizedUserName = "ADMIN@DOMAIN.COM",
            Email = "admin@domain.com",
            NormalizedEmail = "ADMIN@DOMAIN.COM"
        };
        
        var user1 = new UsuarioAplicacion
        {
            Id = 2,
            UserName = "user1@domain.com",
            NormalizedUserName = "USER1@DOMAIN.COM",
            Email = "user1@domain.com",
            NormalizedEmail = "USER1@DOMAIN.COM"
        };
        
        builder.HasData(
            admin,
            user1);
    }
    
    // Método para añadir contraseñas a los usuarios cuando se ejecuta la aplicación API
    public static async Task SeedUsersAsync(IServiceProvider serviceProvider)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<UsuarioAplicacion>>();

        var adminUser = await userManager.FindByNameAsync("admin");
        if (adminUser != null)
        {
            await userManager.AddPasswordAsync(adminUser, "Admin1234!");
        }

        var normalUser = await userManager.FindByNameAsync("user1");
        if (normalUser != null)
        {
            await userManager.AddPasswordAsync(normalUser, "User1234!");
        }
    }
}