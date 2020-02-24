using Infraestructura;
using Microsoft.AspNetCore.Mvc;
using Servicios;
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
        private readonly ICablemodemService cablemodemService;

        public CablemodemsController(ICablemodemRepository cablemodemRepository, ICablemodemService cablemodemService)
        {
            this.cablemodemRepository = cablemodemRepository;
            this.cablemodemService = cablemodemService;
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
                return Ok(this.cablemodemService.GetVerificados(fabricante).Select(cablemodem => new Cablemodem(cablemodem)));
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
                return Ok(this.cablemodemService.GetNoVerificados(fabricante).Select(cablemodem => new Cablemodem(cablemodem)));
            });
        }
    }
}