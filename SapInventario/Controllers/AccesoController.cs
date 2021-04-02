using SapInventario.Entidades.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SapInventario.Controllers
{
    public class AccesoController : Controller
    {
        private readonly IUsuarioServicio _usuarioServicio;

        public AccesoController(IUsuarioServicio usuarioServicio)
        {
            _usuarioServicio = usuarioServicio;
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string User, string Pass)
        {
            try
            {
                var Usuario = _usuarioServicio.ObtenerUsuario(User, Pass);

                if(Usuario.NombreUsuario == null)
                {
                    ViewBag.MensajeError = "Usuario no existe o contraseña no válida";
                    return View();
                }

                Session["UserSession"] = Usuario;

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }
    }
}