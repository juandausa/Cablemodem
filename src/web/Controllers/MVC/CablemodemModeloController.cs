using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Servicios;
using System.Linq;
using Web.ViewModels;

namespace Web.Controllers.MVC
{
    [Route("cablemodem-modelo")]
    public class CablemodemModeloController : Controller
    {
        private readonly ICablemodemService cablemodemService;

        public CablemodemModeloController(ICablemodemService cablemodemService)
        {
            this.cablemodemService = cablemodemService;
        }

        [HttpGet("no-verificado")]
        public ActionResult NoVerificado()
        {
            var cablemodem = this.cablemodemService.GetNoVerificados().Select(cablemodem => new Cablemodem(cablemodem));
            ViewBag.Cablemodems = cablemodem;
            ViewBag.CablemodemsCount = cablemodem.Count();
            return View();
        }

        [HttpPost]
        [Route("no-verificado", Name = "NoVerificadoFabricante")]
        public ActionResult NoVerificadoFabricante(IFormCollection formFields)
        {
            var fabricante = formFields["fabricante"].ToString();
            //if (this.cablemodemService.)
            var cablemodem = this.cablemodemService.GetNoVerificados(fabricante).Select(cablemodem => new Cablemodem(cablemodem));
            ViewBag.Cablemodems = cablemodem;
            ViewBag.CablemodemsCount = cablemodem.Count();
            return View("NoVerificado");
        }

        // GET: Cablemodem/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // POST: Cablemodem/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(NoVerificado));
            }
            catch
            {
                return View();
            }
        }
    }
}