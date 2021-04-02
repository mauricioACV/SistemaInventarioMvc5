using SapInventario.Entidades;
using SapInventario.Entidades.Interfaces;
using SapInventario.Entidades.Interfaces.IUnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SapInventario.Aplicacion
{
    public class SalidaProductoServicio : ISalidaProductoServicio
    {
        private readonly IUnitOfWork _unitOfWork;
        public SalidaProductoServicio(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<Producto> ObtenerListadoProductosPorPalabraClave(string codigoClave, string palabraClave)
        {
            return _unitOfWork.ProductoRepositorio.ObtenerListadoProductosPorPalabraClave(codigoClave, palabraClave);
        }
    }
}