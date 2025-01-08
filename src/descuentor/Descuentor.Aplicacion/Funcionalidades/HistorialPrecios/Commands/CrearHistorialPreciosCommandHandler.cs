using Descuentor.Dominio.Entidades;
using Descuentor.Dominio.Interfaces;
using MediatR;

namespace Descuentor.Aplicacion.Funcionalidades.HistorialPrecios.Commands;

public class CrearHistorialPreciosCommandHandler : IRequestHandler<CrearHistorialPreciosCommand, int>
{
    private readonly IHistorialPrecioRepository _historialPrecioRepository;

    public CrearHistorialPreciosCommandHandler(IHistorialPrecioRepository historialPrecioRepository)
    {
        _historialPrecioRepository = historialPrecioRepository;
    }

    public async Task<int> Handle(CrearHistorialPreciosCommand request, CancellationToken cancellationToken)
    {
        int contador = 0;
        foreach (var keyValuePair in request.HistorialPrecios)
        {
            var historialPrecio = new HistorialPrecio
            {
                ProductoId = keyValuePair.Key,
                Precio = keyValuePair.Value,
                FechaConsulta = DateTime.UtcNow
            };

            var result = await _historialPrecioRepository.AddAsync(historialPrecio);

            if (result.Id > 0)
            {
                contador++;
            }
        }
        return contador;
    }
}