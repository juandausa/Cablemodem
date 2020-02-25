using Infraestructura;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Web.ViewModels;

namespace Web.Controllers.API
{
    [Route("api/cablemodems")]
    [ApiController]
    public class CablemodemsController : ControllerBase
    {
        private readonly ICablemodemRepository cablemodemRepository;

        public CablemodemsController(ICablemodemRepository cablemodemRepository)
        {
            this.cablemodemRepository = cablemodemRepository;
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
    }
}