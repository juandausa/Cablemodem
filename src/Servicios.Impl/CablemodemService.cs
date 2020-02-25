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

        public IEnumerable<Cablemodem> GetNoVerificados(string fabricante)
        {
            this.logger.LogDebug("Se ingresa en obtener cablemodems no verificados por el fabricante {0}", fabricante);
            var cablemodems = cablemodemRepository.Search(cablemodemVerificado => string.IsNullOrEmpty(fabricante) || cablemodemVerificado.Fabricante == fabricante);
            var modelosEnCablemodems = cablemodems.Select(cable => cable.Modelo);
            var modelos = modeloRepository.Search(modelo => modelo.Fabricante == fabricante && modelosEnCablemodems.Any(mec => mec == modelo.Nombre)).ToList();
            return cablemodems.Where(cable => !modelos.Any(modelo => modelo.Nombre == cable.Modelo && modelo.VersionSoftware == cable.VersionSoftware));
        }

        public IEnumerable<Cablemodem> GetVerificados(string fabricante)
        {
            this.logger.LogDebug("Se ingresa en obtener cablemodems verificados por el fabricante {0}", fabricante);
            var cablemodems = cablemodemRepository.Search(cablemodemVerificado => string.IsNullOrEmpty(fabricante) || cablemodemVerificado.Fabricante == fabricante);
            var modelosEnCablemodems = cablemodems.Select(cable => cable.Modelo);
            var modelos = modeloRepository.Search(modelo => modelo.Fabricante == fabricante && modelosEnCablemodems.Any(mec => mec == modelo.Nombre)).ToList();
            return cablemodems.Where(cable => modelos.Any(modelo => modelo.Nombre == cable.Modelo && modelo.VersionSoftware == cable.VersionSoftware));
        }
    }
}
