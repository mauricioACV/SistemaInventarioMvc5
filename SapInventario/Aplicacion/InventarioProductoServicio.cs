using SapInventario.Entidades;
using SapInventario.Entidades.Interfaces;
using SapInventario.Entidades.Interfaces.IUnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SapInventario.Aplicacion
{
    public class InventarioProductoServicio : IInventarioProductoServicio
    {
        private readonly IUnitOfWork _unitOfWork;
        public InventarioProductoServicio(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<Almacen> ObtenerListaAlmacenes()
        {
            return _unitOfWork.AlmacenRepositorio.ObtenerListaAlmacenes();
        }

        public List<Producto> ObtenerListadoProductosPorPalabraClave(string codigoClave, string palabraClave)
        {
            return _unitOfWork.ProductoRepositorio.ObtenerListadoProductosPorPalabraClave(codigoClave, palabraClave);
        }

        public List<AlmacenUbicacion> ObtenerUbicacionAlmacenPorCodigoAlmacen(int codigoAlmacen)
        {
            return _unitOfWork.AlmacenUbicacionRepositorio.ObtenerUbicacionAlmacenPorCodigoAlmacen(codigoAlmacen);
        }

        public bool RegistrarDistribuirProducto(InventarioProducto productoRegistrar)
        {
            bool response = false;
            bool registrarDistribucion = false;
            bool registrarInventario = false;

            List<InventarioProducto> objProducto = _unitOfWork.DistribucionInventarioRepositorio.ObtenerProductoExistenteDistribucion(productoRegistrar.CodigoSap);
            var ObjProductoExiste = objProducto.Where(x => x.CodigoAlmacen == productoRegistrar.CodigoAlmacen).ToList();

            if (ObjProductoExiste.Count != 0)
            {
                registrarInventario = _unitOfWork.InventarioProductoRepositorio.RegistrarProductoEnInventario(productoRegistrar);

                productoRegistrar.Stock += _unitOfWork.DistribucionInventarioRepositorio.ObtenerStockPorCodigoSapCodigoAlmacen(productoRegistrar.CodigoSap, productoRegistrar.CodigoAlmacen);
                registrarDistribucion = _unitOfWork.DistribucionInventarioRepositorio.ActualizarProductoDistribucion(productoRegistrar);
            }
            else
            {
                registrarInventario = _unitOfWork.InventarioProductoRepositorio.RegistrarProductoEnInventario(productoRegistrar);
                registrarDistribucion = _unitOfWork.DistribucionInventarioRepositorio.RegistrarProductoEnAlmacen(productoRegistrar);
            }


            if (registrarInventario && registrarDistribucion)
            {
                response = true;
            }


            return response;
        }
    }
}