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
        [HttpGet("no-verificado/{fabricante}")]
        public ActionResult Index(string fabricante = "")
        {
            var cablemodem = this.cablemodemService.GetNoVerificados(fabricante).Select(cablemodem => new Cablemodem(cablemodem));
            ViewBag.Cablemodems = cablemodem;
            ViewBag.CablemodemsCount = cablemodem.Count();
            return View();
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

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}