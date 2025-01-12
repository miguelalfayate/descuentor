namespace Descuentor.API.Servicios;

public static class HttpContextExtensions
{
    /// <summary>
    /// Método de extensión para insertar parámetros de paginación en las cabeceras de la respuesta HTTP.
    /// </summary>
    /// <typeparam name="T">Tipo de los elementos en la consulta (generalmente entidades).</typeparam>
    /// <param name="context">El contexto HTTP para acceder a la respuesta.</param>
    /// <param name="numeroProductos">Número total de datos a paginar.</param>
    /// <param name="cantidadRegistrosAMostrar">Número de registros por página.</param>
    /// <returns>Tarea asíncrona que representa la operación.</returns>
    public static void InsertarParametrosPaginacionEnRespuesta(
        this HttpContext context, int numeroProductos, int cantidadRegistrosAMostrar)
    {
        // Comprobar si el contexto es nulo, si es así lanzar una excepción.
        if (context == null) throw new ArgumentNullException(nameof(context));

        // Calcular el número total de páginas, redondeando hacia arriba.
        var totalPaginas = Math.Ceiling((double)numeroProductos / cantidadRegistrosAMostrar);

        // Insertar el conteo de registros y el total de páginas en las cabeceras de la respuesta HTTP.
        // Estas cabeceras pueden ser usadas por el cliente para gestionar la paginación.
        context.Response.Headers?.Append("conteo", numeroProductos.ToString());
        context.Response.Headers?.Append("totalPaginas", totalPaginas.ToString());
    }
}