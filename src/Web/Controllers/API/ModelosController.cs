using Infraestructura;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Web.ViewModels;

namespace Web.Controllers.API
{
    [Route("api/modelos")]
    [ApiController]
    public class ModelosController : ControllerBase
    {
        private readonly IModeloRepository ModeloRepository;
        public ModelosController(IModeloRepository ModeloRepository)
        {
            this.ModeloRepository = ModeloRepository;
        }

        /// <summary>
        /// List Modelos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] Modelo modelo)
        {
            return await Task.Run(() =>
            {
                var result = ModeloRepository.Search(_modelo => (string.IsNullOrEmpty(modelo.Nombre) || _modelo.Nombre == modelo.Nombre) && string.IsNullOrEmpty(modelo.Fabricante) || _modelo.Fabricante == modelo.Fabricante);
                return Ok(result);
            });
        }
    }
}