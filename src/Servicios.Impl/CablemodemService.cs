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
            this.GetModelosCablemodem(fabricante, out IEnumerable<Cablemodem> cablemodems, out IEnumerable<Modelo> modelos);
            return this.FiltrarCablemodemsNoVerificados(cablemodems, modelos);
        }

        public IEnumerable<Cablemodem> GetVerificados(string fabricante = "")
        {
            this.logger.LogDebug("Se ingresa en obtener cablemodems verificados por el fabricante {0}", fabricante);
            this.GetModelosCablemodem(fabricante, out IEnumerable<Cablemodem> cablemodems, out IEnumerable<Modelo> modelos);
            return this.FiltrarCablomedmsVerfificados(cablemodems, modelos);
        }

        public bool PoseeCablemodemsDelFabricante(string fabricante)
        {
            if (string.IsNullOrWhiteSpace(fabricante))
            {
                throw new System.ArgumentException("El fabricante debe especificarse", nameof(fabricante));
            }

            return this.cablemodemRepository.Any(cable => cable.Fabricante == fabricante);
        }

        protected virtual void GetModelosCablemodem(string fabricante, out IEnumerable<Cablemodem> cablemodems, out IEnumerable<Modelo> modelos)
        {
            cablemodems = cablemodemRepository.Search(cablemodem => string.IsNullOrEmpty(fabricante) || cablemodem.Fabricante == fabricante);
            var modelosDeCablemodems = cablemodems.Select(cablemodem => cablemodem.Modelo);
            modelos = modeloRepository.Search(modelo => modelo.Fabricante == fabricante && modelosDeCablemodems.Any(modeloEnCablemodem => modeloEnCablemodem == modelo.Nombre));
        }

        protected virtual IEnumerable<Cablemodem> FiltrarCablemodemsNoVerificados(IEnumerable<Cablemodem> cablemodems, IEnumerable<Modelo> modelos)
        {
            return cablemodems.Where(cablemodem => !modelos.Any(modelo => modelo.Nombre == cablemodem.Modelo && modelo.VersionSoftware == cablemodem.VersionSoftware));
        }

        protected virtual IEnumerable<Cablemodem> FiltrarCablomedmsVerfificados(IEnumerable<Cablemodem> cablemodems, IEnumerable<Modelo> modelos)
        {
            return cablemodems.Where(cablemodem => modelos.Any(modelo => modelo.Nombre == cablemodem.Modelo && modelo.VersionSoftware == cablemodem.VersionSoftware));
        }
    }
}
