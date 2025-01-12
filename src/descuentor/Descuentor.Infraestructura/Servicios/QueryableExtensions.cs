namespace Descuentor.Infraestructura.Servicios;

public static class QueryableExtensions
{
    public static IQueryable<T> Paginar<T>(this IQueryable<T> queryable, int cantidadRegistros, int pagina)
    {
        return queryable
            // Calcula el número de registros a omitir según la página actual y la cantidad de registros por página
            .Skip((pagina - 1) * cantidadRegistros)
            // Toma la cantidad de registros indicada para la página
            .Take(cantidadRegistros);
    }
}