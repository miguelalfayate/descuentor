namespace Descuentor.Dominio.Interfaces;

public interface IScrapingService
{
    Task<Dictionary<int, decimal>> ObtenerPrecios(Dictionary<int, string> productos);
}