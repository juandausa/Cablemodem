using Infraestructura;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebAPI.ViewModels;

namespace WebAPI.Controllers
{
    [Route("api/cablemodems")]
    [ApiController]
    public class CablemodemsController : ControllerBase
    {
        private readonly ICablemodemRepository CablemodemRepository;
        public CablemodemsController(ICablemodemRepository CablemodemRepository)
        {
            this.CablemodemRepository = CablemodemRepository;
        }

        /// <summary>
        /// List Cablemodems
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] Cablemodem Cablemodem)
        {
            return await Task.Run(() =>
            {
                var result = CablemodemRepository.Search(_cablemodem => (string.IsNullOrEmpty(Cablemodem.Ip) || _cablemodem.Ip == Cablemodem.Ip) && string.IsNullOrEmpty(Cablemodem.MacAddress) || _cablemodem.MacAddress == Cablemodem.MacAddress);
                return Ok(result);
            });
        }
    }
}