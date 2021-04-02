using SapInventario.Entidades;
using SapInventario.Entidades.Interfaces.IUnitOfWork;
using SapInventario.Infraestructura.Data;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SapInventario.Filters
{
    public class VerificaAutorizacionUser : AuthorizeAttribute
    {
        private Usuario objUsuario;

        private readonly IUnitOfWork _unitOfWork = new UnitOfWork();

        private int _idOperacion;

        public VerificaAutorizacionUser(int idOperacion = 0)
        {
            _idOperacion = idOperacion;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            try
            {
                objUsuario = (Usuario)HttpContext.Current.Session["UserSession"];
                var listadoOperacionesRol = _unitOfWork.UsuarioRepositorio.ObtenerOperacionesRolUsuario(objUsuario.IdRol, _idOperacion);

                if(listadoOperacionesRol.ToList().Count() == 0)
                {
                    filterContext.Result = new RedirectResult("/Error/OperacionNoAutorizada?nombreUsuario=");
                }
            }
            catch (Exception)
            {
                filterContext.Result = new RedirectResult("/Error/OperacionNoAutorizada?nombreUsuario=");
            }
        }
    }
}