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

        [HttpPost]
        public ActionResult ObtenerListadoProductosPorPalabraClave(string codigoClave, string palabraClave)
        {
            var response = _salidaProductoServicio.ObtenerListadoProductosPorPalabraClave(codigoClave, palabraClave);
            return Json(new { data = response });
        }
    }
}