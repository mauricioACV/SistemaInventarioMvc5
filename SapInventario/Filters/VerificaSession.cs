using SapInventario.Controllers;
using SapInventario.Entidades;
using System.Web;
using System.Web.Mvc;

namespace SapInventario.Filters
{
    public class VerificaSession : ActionFilterAttribute
    {
        private Usuario objUsuario;

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            try
            {
                base.OnActionExecuted(filterContext);

                objUsuario = (Usuario)HttpContext.Current.Session["UserSession"];

                if(objUsuario == null)
                {
                    if(filterContext.Controller is AccesoController == false)
                    {
                        filterContext.HttpContext.Response.Redirect("/Acceso/Login");
                    }
                }
            }
            catch (System.Exception)
            {
                filterContext.HttpContext.Response.Redirect("/Acceso/Login");
            }
        }
    }
}