using SapInventario.Aplicacion.Common.Comportamientos.Interfaces;
using SapInventario.Entidades;
using SapInventario.Entidades.Interfaces;
using SapInventario.Entidades.Interfaces.IUnitOfWork;

namespace SapInventario.Aplicacion
{
    public class UsuarioServicio : IUsuarioServicio
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISeguridadServicioRepositorio _servicioSeguridad;

        public UsuarioServicio(IUnitOfWork unitOfWork, ISeguridadServicioRepositorio seguridadServicioRepositorio)
        {
            _unitOfWork = unitOfWork;
            _servicioSeguridad = seguridadServicioRepositorio;
        }

        public Usuario ObtenerUsuario(string user, string pass)
        {
            string passCrypto = _servicioSeguridad.EncriptarPassword(pass);

            var objUser = _unitOfWork.UsuarioRepositorio.ObtenerUsuario(user, passCrypto);
            if (objUser.IdRol != 0)
            {
                objUser.NombreRol = _unitOfWork.UsuarioRepositorio.ObtenerRolUsuario(objUser.IdRol);
            }

            return objUser;
        }
    }
}