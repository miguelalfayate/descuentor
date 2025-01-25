using Blazored.LocalStorage;
using Descuentor.WebAssembly.Interfaces;
using Descuentor.WebAssembly.Servicios;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using IAuthService = Descuentor.WebAssembly.Interfaces.IAuthService;

namespace Descuentor.WebAssembly;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
        
        builder.Services.AddScoped<ITokenService, TokenService>(); // Registrar TokenService
        builder.Services.AddScoped<IAuthService, AuthService>();
        
        builder.Services.AddBlazoredLocalStorage();

        await builder.Build().RunAsync();
    }
}