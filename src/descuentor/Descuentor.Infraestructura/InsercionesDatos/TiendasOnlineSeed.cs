using Descuentor.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Descuentor.Infraestructura.InsercionesDatos;

public class TiendasOnlineSeed : IEntityTypeConfiguration<TiendaOnline>
{
    public void Configure(EntityTypeBuilder<TiendaOnline> builder)
    {
        builder.HasData(
            new TiendaOnline
            {
                Nombre = "Amazon",
                UrlProducto = "amazon.es"
            },
            new TiendaOnline
            {
                Nombre = "Wallapop",
                UrlProducto = "es.wallapop.com"
            });
    }
}