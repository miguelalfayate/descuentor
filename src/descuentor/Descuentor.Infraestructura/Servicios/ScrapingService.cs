using System.Collections.Concurrent;
using Descuentor.Dominio.Interfaces;
using Microsoft.Playwright;

namespace Descuentor.Infraestructura.Servicios;

public class ScrapingService : IScrapingService
{
    private readonly ITiendaOnlineFactory _tiendaOnlineFactory;

    public ScrapingService(ITiendaOnlineFactory tiendaOnlineFactory)
    {
        _tiendaOnlineFactory = tiendaOnlineFactory;
    }   

    public async Task<Dictionary<int, decimal>> ObtenerPrecios(Dictionary<int, string> productosUrl)
    {
        var result = new ConcurrentDictionary<int, decimal>();

        await Parallel.ForEachAsync(
            productosUrl,
            new ParallelOptions { MaxDegreeOfParallelism = 10 },
            async (kvp, token) =>
            {
                var price = await ObtenerPrecioProducto(kvp.Value);
                result.TryAdd(kvp.Key, price);
            });

        return new Dictionary<int, decimal>(result);
    }

    private async Task<decimal> ObtenerPrecioProducto(string url)
    {
        using var playwright = await Playwright.CreateAsync();

        if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
        {
            var exitCode = Microsoft.Playwright.Program.Main(new[] { "install" });
            if (exitCode != 0)
            {
                throw new Exception($"Playwright exited with code {exitCode}");
            }
        }
        
        await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = true,
            //ExecutablePath = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production" ? "/path/to/chromium" : null
        });

        var page = await browser.NewPageAsync();

        // Navega a la URL
        Console.WriteLine($"Navegando a {url}...");
        try
        {
            await page.GotoAsync(url);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al navegar a la URL {url}: {ex.Message}");
        }

        // Espera que el precio esté presente en la página
        var selector = _tiendaOnlineFactory.ObtenerSelectorPrecio(url);
        var priceElement = await page.QuerySelectorAsync(selector);

        decimal priceValue = 0;
        if (priceElement != null)
        {
            // Extrae el contenido del elemento con la clase 'a-price-whole'
            string price = await priceElement.InnerTextAsync();
            price = price.Replace("€", "").Trim();
            Console.WriteLine($"Precio encontrado: {price}");
            //priceValue = decimal.Parse(price);
            decimal.TryParse(price, out priceValue);
        }
        else
        {
            Console.WriteLine("No se encontró el precio.");
        }

        // Cierra el navegador
        await browser.CloseAsync();

        return priceValue;
    }
}