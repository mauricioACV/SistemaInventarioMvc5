using SapInventario.Entidades;
using SapInventario.Entidades.Interfaces;
using SapInventario.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SapInventario.Controllers
{
    public class InventarioProductoController : Controller
    {
        private readonly IInventarioProductoServicio _inventarioProductoServicio;

        public InventarioProductoController(IInventarioProductoServicio inventarioProductoServicio)
        {
            _inventarioProductoServicio = inventarioProductoServicio;
        }
        [VerificaAutorizacionUser(idOperacion:2)]
        public ActionResult AlmacenarProducto()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegistrarProducto(InventarioProducto inventarioProducto)
        {
            bool response = _inventarioProductoServicio.RegistrarDistribuirProducto(inventarioProducto);
            return Json(response);
        }

        [HttpPost]
        public ActionResult ObtenerListaAlmacenes()
        {
            var ListadoAlmacenes = _inventarioProductoServicio.ObtenerListaAlmacenes();
            return Json(new { data = ListadoAlmacenes});
        }

        [HttpPost]
        public ActionResult ObtenerListadoProductosPorPalabraClave(string codigoClave, string palabraClave)
        {
            var ListadoProductos = _inventarioProductoServicio.ObtenerListadoProductosPorPalabraClave(codigoClave, palabraClave);
            return Json(new { data = ListadoProductos });
        }

        [HttpPost]
        public ActionResult ObtenerUbicacionAlmacenPorCodigoAlmacen(int codigoAlmacen)
        {
            var ListUbicacionAlmacen = _inventarioProductoServicio.ObtenerUbicacionAlmacenPorCodigoAlmacen(codigoAlmacen);
            return Json(new { data = ListUbicacionAlmacen });
        }
    }
}