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

        public bool ActualizarStockAlmacenPorIdRegistro(string idRegistro, int stock)
        {
            return _unitOfWork.DistribucionInventarioRepositorio.ActualizarStockAlmacenPorIdRegistro(idRegistro, stock);
        }

        public int ObtenerAlmacenProductoExistenteDistribucion(string codigoSap)
        {
            return _unitOfWork.DistribucionInventarioRepositorio.ObtenerAlmacenProductoExistenteDistribucion(codigoSap);
        }

        public List<Producto> ObtenerListadoProductosPorPalabraClave(string codigoClave, string palabraClave)
        {
            return _unitOfWork.ProductoRepositorio.ObtenerListadoProductosPorPalabraClave(codigoClave, palabraClave);
        }

        public int ObtenerStockPorIdRegistro(string IdRegistro)
        {
            return _unitOfWork.DistribucionInventarioRepositorio.ObtenerStockPorIdRegistro(IdRegistro);
        }

        public List<DistribucionInventario> ObtenerStockTotaAndAlmacenlProductoPorCodigoSap(string codigoSap)
        {
            return _unitOfWork.DistribucionInventarioRepositorio.ObtenerStockAndAlmacenTotalProductoPorCodigoSap(codigoSap);
        }

        public int ObtenerUltimoNumeroActa()
        {
            return _unitOfWork.SalidaProductoRepositorio.ObtenerUltimoNumeroActa();
        }

        public bool RegistrarActa(SalidaProducto objSalidaProducto)
        {
            return _unitOfWork.SalidaProductoRepositorio.RegistrarActa(objSalidaProducto);
        }
    }
}