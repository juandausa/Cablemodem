using Entidades;
using Infraestructura;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace Servicios.Impl
{
    public class ModeloService : IModeloService
    {
        private readonly IModeloRepository modeloRepository;
        private readonly ILogger<ModeloService> logger;

        public ModeloService(IModeloRepository modeloRepository, ILogger<ModeloService> logger)
        {
            this.modeloRepository = modeloRepository;
            this.logger = logger;
        }

        public Modelo AgregarModelo(Modelo modelo)
        {
            this.logger.LogDebug("Se intentará agregar un modelo {0}", modelo.ToString());
            var modeloExistente = this.modeloRepository.Search(_modelo => modelo.Fabricante == _modelo.Fabricante && modelo.Nombre == _modelo.Nombre && modelo.VersionSoftware == _modelo.VersionSoftware).FirstOrDefault();
            if (modeloExistente is null)
            {
                return this.modeloRepository.Save(modelo);
            }

            return modeloExistente;
        }
    }
}
