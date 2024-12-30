using Descuentor.Aplicacion;
using Descuentor.Infraestructura;
using Descuentor.Infraestructura.ModelosIdentity;

namespace Descuentor.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddInfraestructura(builder.Configuration);
        builder.Services.AddAplicacion();

        builder.Services.AddControllers();
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

        app.Run();
    }
}