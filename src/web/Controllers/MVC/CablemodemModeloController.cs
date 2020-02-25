using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Servicios;
using System;
using System.Linq;
using Web.ViewModels;

namespace Web.Controllers.MVC
{
    [Route("cablemodem-modelo")]
    public class CablemodemModeloController : Controller
    {
        private readonly ICablemodemService cablemodemService;
        private readonly IModeloService modeloService;

        public CablemodemModeloController(ICablemodemService cablemodemService, IModeloService modeloService)
        {
            this.cablemodemService = cablemodemService;
            this.modeloService = modeloService;
        }

        [HttpGet]
        [Route("", Name = "Index")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("no-verificado", Name = "NoVerificadoFabricante")]
        public ActionResult NoVerificadoFabricante(IFormCollection formFields)
        {
            // TODO: El framework permite usar modelos.
            var fabricante = formFields["fabricante"].ToString();
            if (cablemodemService.PoseeCablemodemsDelFabricante(fabricante))
            {
                CargarCablemodems(fabricante);
            }
            else
            {
                ViewBag.Error = "El fabricante no se encuentra";
            }

            return View("NoVerificado");
        }

        [HttpPost]
        [Route("agregar-modelo", Name = "AgregarModelo")]
        public ActionResult AgregarModelo(IFormCollection formFields)
        {
            try
            {
                var fabricante = formFields["fabricante"].ToString();
                var modelo = formFields["modelo"].ToString();
                var versionSoftware = formFields["versionSoftware"].ToString();
                this.modeloService.AgregarModelo(new Entidades.Modelo(fabricante, modelo, versionSoftware));
                this.CargarCablemodems(fabricante);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }

            return View();
        }

        private void CargarCablemodems(string fabricante = "")
        {
            var cablemodem = this.cablemodemService.GetNoVerificados(fabricante).Select(cablemodem => new Cablemodem(cablemodem));
            ViewBag.Fabricante = fabricante;
            ViewBag.Cablemodems = cablemodem;
            ViewBag.CablemodemsCount = cablemodem.Count();
        }
    }
}