using Descuentor.Infraestructura.ModelosIdentity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Descuentor.Infraestructura.InsercionesDatos;

public class RolesSeed : IEntityTypeConfiguration<RolAplicacion>
{
    public void Configure(EntityTypeBuilder<RolAplicacion> builder)
    {
        builder.HasData(
            new RolAplicacion
            {
                Id = 1,
                Name = "Admin",
                NormalizedName = "ADMIN"
            },
            new RolAplicacion
            {
                Id = 2,
                Name = "Usuario",
                NormalizedName = "USUARIO"
            }
        );
    }
}