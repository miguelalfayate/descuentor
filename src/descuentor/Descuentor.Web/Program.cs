using Blazored.LocalStorage;
using Descuentor.Aplicacion.Interfaces;
using Descuentor.Web.Components;
using Descuentor.Web.Proveedores;
using Descuentor.Web.Servicios;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Descuentor.Web;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();
        
        // Add services to the container.
        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor();
        builder.Services.AddScoped<HttpClient>();
        builder.Services.AddScoped<TokenService>(); // Registrar TokenService
        
        builder.Services.AddScoped<IAuthService, AuthService>();
        
        builder.Services.AddAuthorizationCore();
        builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
        
        builder.Services.AddBlazoredLocalStorage();
        
        // Configure authentication
        // builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        //     .AddCookie(options =>
        //     {
        //         options.LoginPath = "/login";
        //     });
        
        builder.Services.AddAuthentication()
            .AddBearerToken(IdentityConstants.BearerScheme);
        // builder.Services.AddAuthentication(IdentityConstants.BearerScheme);

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();
        
        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();
        
        app.UseAntiforgery();

        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        app.Run();
    }
}