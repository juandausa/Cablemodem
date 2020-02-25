using Entidades;
using Infraestructura;
using System.Linq;

namespace Servicios.Impl
{
    public class ModeloService : IModeloService
    {
        private readonly IModeloRepository modeloRepository;

        public ModeloService(IModeloRepository modeloRepository)
        {
            this.modeloRepository = modeloRepository;
        }

        public Modelo AgregarModelo(Modelo modelo)
        {
            var modeloExistente = this.modeloRepository.Search(_modelo => modelo.Fabricante == _modelo.Fabricante && modelo.Nombre == _modelo.Nombre && modelo.VersionSoftware == _modelo.VersionSoftware).FirstOrDefault();
            if (modeloExistente is null)
            {
                return this.modeloRepository.Save(modelo);
            }

            return modeloExistente;
        }
    }
}
