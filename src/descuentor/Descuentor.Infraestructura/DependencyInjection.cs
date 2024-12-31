using Descuentor.Infraestructura.Contextos;
using Descuentor.Infraestructura.ModelosIdentity;
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
        
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
                options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            })
            .AddBearerToken(IdentityConstants.BearerScheme)
            .AddIdentityCookies(o => { });
        
            // .AddCookie(IdentityConstants.ApplicationScheme, options =>
            // {
            //     options.LoginPath = "/identity/login";
            //     options.LogoutPath = "/identity/logout";
            // })
            // .AddCookie(IdentityConstants.ExternalScheme, options =>
            // {
            //     options.Cookie.Name = IdentityConstants.ExternalScheme;
            //     options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
            // });

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
        
        return services;
    }
}