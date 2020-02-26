using Entidades;
using Infraestructura;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace Servicios.Impl
{
    public class CablemodemService : ICablemodemService
    {
        private readonly ICablemodemRepository cablemodemRepository;
        private readonly IModeloRepository modeloRepository;
        private readonly ILogger<CablemodemService> logger;

        public CablemodemService(ICablemodemRepository cablemodemRepository, IModeloRepository modeloRepository, ILogger<CablemodemService> logger)
        {
            this.cablemodemRepository = cablemodemRepository;
            this.modeloRepository = modeloRepository;
            this.logger = logger;
        }

        public IEnumerable<Cablemodem> GetNoVerificados(string fabricante = "")
        {
            this.logger.LogDebug("Se ingresa en obtener cablemodems no verificados por el fabricante {0}", fabricante);
            GetModelosCablemodem(fabricante, out IEnumerable<Cablemodem> cablemodems, out List<Modelo> modelos);
            return cablemodems.Where(cable => !modelos.Any(modelo => modelo.Nombre == cable.Modelo && modelo.VersionSoftware == cable.VersionSoftware));
        }

        public IEnumerable<Cablemodem> GetVerificados(string fabricante = "")
        {
            this.logger.LogDebug("Se ingresa en obtener cablemodems verificados por el fabricante {0}", fabricante);
            GetModelosCablemodem(fabricante, out IEnumerable<Cablemodem> cablemodems, out List<Modelo> modelos);
            return cablemodems.Where(cable => modelos.Any(modelo => modelo.Nombre == cable.Modelo && modelo.VersionSoftware == cable.VersionSoftware));
        }

        public bool PoseeCablemodemsDelFabricante(string fabricante)
        {
            if (string.IsNullOrWhiteSpace(fabricante))
            {
                throw new System.ArgumentException("El fabricante debe especificarse", nameof(fabricante));
            }

            return this.cablemodemRepository.Any(cable => cable.Fabricante == fabricante);
        }

        private void GetModelosCablemodem(string fabricante, out IEnumerable<Cablemodem> cablemodems, out List<Modelo> modelos)
        {
            cablemodems = cablemodemRepository.Search(cablemodemVerificado => string.IsNullOrEmpty(fabricante) || cablemodemVerificado.Fabricante == fabricante);
            var modelosEnCablemodems = cablemodems.Select(cable => cable.Modelo);
            modelos = modeloRepository.Search(modelo => modelo.Fabricante == fabricante && modelosEnCablemodems.Any(mec => mec == modelo.Nombre)).ToList();
        }
    }
}
