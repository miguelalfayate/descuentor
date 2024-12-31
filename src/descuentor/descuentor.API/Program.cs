using Descuentor.API.Servicios;
using Descuentor.Aplicacion;
using Descuentor.Aplicacion.Funcionalidades.Productos.Commands;
using Descuentor.Aplicacion.Interfaces;
using Descuentor.Infraestructura;
using Descuentor.Infraestructura.InsercionesDatos;
using Descuentor.Infraestructura.ModelosIdentity;

namespace Descuentor.API;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddInfraestructura(builder.Configuration);
        builder.Services.AddAplicacion();

        builder.Services.AddControllers();
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CrearProductoCommandHandler).Assembly));
        
        builder.Services.AddScoped<ICurrentUser, CurrentUser>();
        
        
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        
        builder.Services.AddSingleton(TimeProvider.System);

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();
        
        app.MapControllers();

        app.MapGroup("/identity").MapIdentityApi<UsuarioAplicacion>();
        
        // Cuando se ejecuta la app se añaden las contraseñas a los usuarios
        using (var scope = app.Services.CreateScope())
        {
            var scopedProvider = scope.ServiceProvider;
            await UsuariosSeed.SeedUsersAsync(scopedProvider);
        }

        app.Run();
    }
}