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
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("defaultConnection"))
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
        services.AddTransient<IScrapingService, ScrapingService>();
        services.AddTransient<ITiendaOnlineFactory, TiendaOnlineFactory>();

        services.AddScoped<IProductoRepository, ProductoRepository>();
        services.AddScoped<IUsuarioProductoRepository, UsuarioProductoRepository>();
        services.AddScoped<IHistorialPrecioRepository, HistorialPrecioRepository>();
        return services;
    }
}