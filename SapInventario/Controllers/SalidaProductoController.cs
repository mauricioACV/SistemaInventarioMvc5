using SapInventario.Entidades;
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
        public ActionResult ObtenerStockTotalProductoPorCodigoSap(string CodigoSap)
        {
            var response = _salidaProductoServicio.ObtenerStockTotaAndAlmacenlProductoPorCodigoSap(CodigoSap);
            return Json(new { data = response });
        }

        [HttpPost]
        public ActionResult ObtenerStockTotalProductoPorId(string idRegistro)
        {
            var response = _salidaProductoServicio.ObtenerStockPorIdRegistro(idRegistro);
            return Json(new { data = response });
        }

        [HttpPost]
        public ActionResult ObtenerAlmacenProductoExistenteDistribucion(string codigoSap)
        {
            var codigoAlmacen = _salidaProductoServicio.ObtenerAlmacenProductoExistenteDistribucion(codigoSap);
            return Json(new { data = codigoAlmacen });
        }

        [HttpPost]
        public ActionResult ObtenerNumeroActa()
        {
            var numActa = _salidaProductoServicio.ObtenerUltimoNumeroActa();
            return Json(new { data = numActa });
        }

        [HttpPost]
        public ActionResult ActualizarStockAlmacenPorIdRegistro(string idRegistro, int stock)
        {
            var reponse = _salidaProductoServicio.ActualizarStockAlmacenPorIdRegistro(idRegistro, stock);
            return Json(new { data = reponse });
        }


        [HttpPost]
        public ActionResult RegistrarActa(SalidaProducto objSalidaProducto)
        {
            var reponse = _salidaProductoServicio.RegistrarActa(objSalidaProducto);
            return Json(new { data = reponse });
        }

        [HttpPost]
        public ActionResult RegistrarProductosSalida(List<ListaProductoSalida> listaSalidaProducto)
        {
            var reponse = _salidaProductoServicio.RegistrarProductosSalida(listaSalidaProducto);
            return Json(new { data = reponse });
        }
    }
}