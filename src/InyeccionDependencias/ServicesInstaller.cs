using Infraestructura;
using Infraestructura.Impl;
using Microsoft.Extensions.DependencyInjection;

namespace InyeccionDependencias
{
    public static class ServicesInstaller
    {
        public static void Install(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<ICablemodemRepository, CablemodemRepository>();
        }
    }
}
