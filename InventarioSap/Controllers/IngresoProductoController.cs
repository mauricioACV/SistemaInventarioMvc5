using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventarioSap.Controllers
{
    public class IngresoProductoController : Controller
    {
        // GET: IngresoProducto
        public ActionResult RegistrarProducto()
        {
            return View();
        }
    }
}