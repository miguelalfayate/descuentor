using Descuentor.Dominio.Interfaces;

namespace Descuentor.Infraestructura.Servicios;

public class TiendaOnlineFactory :ITiendaOnlineFactory
{
    private readonly Dictionary<string, string> _selectors = new Dictionary<string, string>
    {
        { "amazon.es", "#priceblock_ourprice, .a-price .a-offscreen" },
        { "es.wallapop.com", "span[class*='ItemDetailPrice']" }
        // Añade más tiendas y sus selectores aquí
    };
    
    public string ObtenerSelectorPrecio(string urlTienda)
    {
        foreach (var entry in _selectors)
        {
            if (urlTienda.Contains(entry.Key))
            {
                return entry.Value;
            }
        }

        // Retorna un selector por defecto o lanza una excepción si no se encuentra un selector adecuado
        return ".default-selector";
    }
}