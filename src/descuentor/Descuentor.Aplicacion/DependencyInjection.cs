using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Descuentor.Aplicacion;

public static class DependencyInjection
{
    public static IServiceCollection AddAplicacion(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        services.AddMediatR(configuracion =>
        {
            configuracion.RegisterServicesFromAssemblies(assembly);
        });
        
        services.AddValidatorsFromAssembly(assembly);
        
        return services;
    }
}