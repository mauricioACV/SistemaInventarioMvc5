using SapInventario.Entidades.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SapInventario.Controllers
{
    public class SalidaProductoController : Controller
    {
        private readonly ISalidaProductoServicio _salidaProductoServicio;

        public SalidaProductoController(ISalidaProductoServicio salidaProductoServicio)
        {
            _salidaProductoServicio = salidaProductoServicio;
        }
        public ActionResult RegistrarSalidaProducto()
        {
            return View();
        }

        public ActionResult ReporteSalidaProducto()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ObtenerListadoProductosPorPalabraClave(string codigoClave, string palabraClave)
        {
            var response = _salidaProductoServicio.ObtenerListadoProductosPorPalabraClave(codigoClave, palabraClave);
            return Json(new { data = response });
        }

        [HttpPost]
        public ActionResult ObtenerStockTotalProductoPorCodigoSap(string codigoSap)
        {
            var response = _salidaProductoServicio.ObtenerStockTotalProductoPorCodigoSap(codigoSap);
            return Json(new { data = response });
        }

        [HttpPost]
        public ActionResult ObtenerAlmacenProductoExistenteDistribucion(string codigoSap)
        {
            var codigoAlmacen = _salidaProductoServicio.ObtenerAlmacenProductoExistenteDistribucion(codigoSap);
            return Json(new { data = codigoAlmacen });
        }
    }
}