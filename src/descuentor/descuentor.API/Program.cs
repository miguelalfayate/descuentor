using System.Text.Json.Serialization;
using Descuentor.API.Servicios;
using Descuentor.Aplicacion;
using Descuentor.Aplicacion.Funcionalidades.Productos.Commands;
using Descuentor.Dominio.Interfaces;
using Descuentor.Infraestructura;
using Descuentor.Infraestructura.InsercionesDatos;
using Descuentor.Infraestructura.ModelosIdentity;
using Microsoft.OpenApi.Models;

namespace Descuentor.API;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddInfraestructura(builder.Configuration);
        builder.Services.AddAplicacion();

        builder.Services.AddControllers().AddJsonOptions(x =>
        {

            // ignore omitted parameters on models to enable optional params (e.g. User update)
            //x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            
            x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;

        });
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CrearProductoCommandHandler).Assembly));
        
        builder.Services.AddScoped<ICurrentUser, CurrentUser>();
        
        
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter a valid token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type=ReferenceType.SecurityScheme,
                            Id="Bearer"
                        }
                    },
                    new string[]{}
                }
            });
        });
            
        
        
        
        
        builder.Services.AddSingleton(TimeProvider.System);

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors(x => x
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()
            .WithExposedHeaders("*")
            .SetIsOriginAllowed(origin => true));
        
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