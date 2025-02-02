using Descuentor.Dominio.Interfaces;
using Descuentor.Infraestructura.Contextos;
using Descuentor.Infraestructura.ModelosIdentity;
using Descuentor.Infraestructura.Repositorios;
using Descuentor.Infraestructura.Servicios;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Descuentor.Infraestructura;

public static class DependencyInjection
{
    public static IServiceCollection AddInfraestructura(this IServiceCollection services, IConfiguration configuration)
    {
        // services.AddDbContext<ApplicationDbContext>(options =>
        //     options.UseNpgsql(configuration.GetConnectionString("defaultConnection"))
        // );
        
        var connectionString = configuration.GetConnectionString(
            configuration.GetValue<string>("ASPNETCORE_ENVIRONMENT") == "Development" 
                ? "developmentConnection" 
                : "productionConnection");

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString)
        );

        services.AddDataProtection();

        services.AddAuthentication()
            .AddBearerToken(IdentityConstants.BearerScheme);

        // services.AddAuthentication(options =>
        //     {
        //         options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
        //         options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
        //         options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
        //     })
        //     .AddIdentityCookies(o => { });

        services.AddIdentityCore<UsuarioAplicacion>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                //options.Tokens.AuthenticatorTokenProvider = TokenOptions.DefaultAuthenticatorProvider;
            })
            .AddRoles<RolAplicacion>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddSignInManager<SignInManager<UsuarioAplicacion>>()
            .AddDefaultTokenProviders();

        services.AddTransient<IEmailSender<UsuarioAplicacion>, EmailSender>();
        services.AddTransient<IEnvioEmailService, EnvioEmailService>();
        services.AddTransient<IScrapingService, ScrapingService>();
        services.AddTransient<ITiendaOnlineFactory, TiendaOnlineFactory>();
        services.AddTransient<INotificacionDescuentosService, NotificacionDescuentosService>();

        services.AddScoped<IProductoRepository, ProductoRepository>();
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        services.AddScoped<IHistorialPrecioRepository, HistorialPrecioRepository>();
        return services;
    }
}