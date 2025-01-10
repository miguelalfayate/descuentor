using Descuentor.Web.Autenticacion;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace Descuentor.Web;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

        // Añadido para agregar autenticación en la web
        builder.Services.AddScoped<CustomAuthStateProvider>();
        builder.Services.AddScoped<StorageChangeMonitor>();
        builder.Services.AddScoped<AuthenticationStateProvider>(provider => 
            provider.GetRequiredService<CustomAuthStateProvider>());
        builder.Services.AddAuthorizationCore();
        
        await builder.Build().RunAsync();
    }
}