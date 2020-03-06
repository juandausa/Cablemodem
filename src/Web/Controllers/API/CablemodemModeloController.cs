using Microsoft.AspNetCore.Mvc;
using Servicios;
using System.Linq;
using System.Threading.Tasks;
using Web.ViewModels;

namespace Web.Controllers.API
{
    [Route("api/cablemodem-modelo")]
    [ApiController]
    public class CablemodemModeloController : ControllerBase
    {
        private readonly ICablemodemService cablemodemService;

        public CablemodemModeloController(ICablemodemService cablemodemService)
        {
            this.cablemodemService = cablemodemService;
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