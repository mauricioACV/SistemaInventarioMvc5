using SapInventario.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SapInventario.Controllers
{
    public class HomeController : Controller
    {
        private Usuario objUsuario;

        public ActionResult Index()
        {
            objUsuario = (Usuario)Session["UserSession"];
            Session["NombreUser"] = objUsuario.Nombres;
            Session["ApellidoUser"] = objUsuario.Apellidos;
            Session["RolUser"] = objUsuario.NombreRol;
            return View();
        }
    }
}