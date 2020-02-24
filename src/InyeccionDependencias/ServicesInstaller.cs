using Infraestructura;
using Infraestructura.Impl;
using Microsoft.Extensions.DependencyInjection;
using Servicios;
using Servicios.Impl;

namespace InyeccionDependencias
{
    public static class ServicesInstaller
    {
        public static void Install(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<ICablemodemRepository, CablemodemRepository>();
            serviceCollection.AddScoped<IModeloRepository, ModeloRepository>();
            serviceCollection.AddScoped<ICablemodemService, CablemodemService>();
        }
    }
}
