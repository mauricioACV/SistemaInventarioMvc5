using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SapInventario.Controllers
{
    public class ErrorController : Controller
    {
        [HttpGet]
        public ActionResult OperacionNoAutorizada(string nombreUsuario)
        {
            ViewBag.nombreUsuario = nombreUsuario;
            return View();
        }
    }
}