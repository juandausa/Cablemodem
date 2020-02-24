using Infraestructura;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.ViewModels;

namespace WebAPI.Controllers
{
    [Route("api/cablemodems")]
    [ApiController]
    public class CablemodemsController : ControllerBase
    {
        private readonly ICablemodemRepository cablemodemRepository;
        private readonly IModeloRepository modeloRepository;

        public CablemodemsController(ICablemodemRepository cablemodemRepository, IModeloRepository modeloRepository)
        {
            this.cablemodemRepository = cablemodemRepository;
            this.modeloRepository = modeloRepository;
        }

        /// <summary>
        /// Listado Cablemodems
        /// </summary>
        /// <returns>Devuelve el listado de cablemodems filtrado</returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] CablemodemFilter cablemodem)
        {
            return await Task.Run(() =>
            {
                var cablemodems = cablemodemRepository.Search(cable => (string.IsNullOrEmpty(cablemodem.Ip) || cable.Ip == cablemodem.Ip) && string.IsNullOrEmpty(cablemodem.MacAddress) || cable.MacAddress == cablemodem.MacAddress);
                return Ok(cablemodems);
            });
        }

        /// <summary>
        /// Listado de cablemodes que no cumple con versión y fabricante declarado.
        /// </summary>
        /// <returns>Devuelve el listado de cablemodes que cumplen con versión y fabricante declarado.</returns>
        [HttpGet("verificados/{fabricante}")]
        public async Task<IActionResult> GetCablemodemsVerificados(string fabricante)
        {
            return await Task.Run(() =>
            {
                var cablemodems = cablemodemRepository.Search(cablemodemVerificado => cablemodemVerificado.Fabricante == fabricante);
                var modelosEnCablemodems = cablemodems.Select(cable => cable.Modelo);
                var modelos = modeloRepository.Search(modelo => modelo.Fabricante == fabricante && modelosEnCablemodems.Any(mec => mec == modelo.Nombre)).ToList();
                var cablemodemsVerificados = cablemodems.Where(cable => modelos.Any(modelo => modelo.Nombre == cable.Modelo && modelo.VersionSoftware == cable.VersionSoftware));
                return Ok(cablemodemsVerificados.Select(cablemodem => new Cablemodem(cablemodem)));
            });
        }

        /// <summary>
        /// Listado de cablemodes que no cumple con versión y fabricante declarado.
        /// </summary>
        /// <returns>Devuelve el listado de cablemodes que cumplen con versión y fabricante declarado.</returns>
        [HttpGet("no-verificados/{fabricante}")]
        public async Task<IActionResult> GetCablemodemsNoVerificados(string fabricante)
        {
            return await Task.Run(() =>
            {
                var cablemodems = cablemodemRepository.Search(cablemodemVerificado => cablemodemVerificado.Fabricante == fabricante);
                var modelosEnCablemodems = cablemodems.Select(cable => cable.Modelo);
                var modelos = modeloRepository.Search(modelo => modelo.Fabricante == fabricante && modelosEnCablemodems.Any(mec => mec == modelo.Nombre)).ToList();
                var cablemodemsNoVerificados = cablemodems.Where(cable => !modelos.Any(modelo => modelo.Nombre == cable.Modelo && modelo.VersionSoftware == cable.VersionSoftware));
                return Ok(cablemodemsNoVerificados.Select(cablemodem => new Cablemodem(cablemodem)));
            });
        }
    }
}