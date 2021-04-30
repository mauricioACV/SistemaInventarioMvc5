using SapInventario.Entidades.Interfaces;
using SapInventario.Entidades.Interfaces.IUnitOfWork;
using SapInventario.Infraestructura.Data.Repositorios;
using System.Data.SqlClient;

namespace SapInventario.Infraestructura.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SqlConnection _conexion;

#pragma warning disable 0649
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IInventarioProductoRepositorio _inventarioProductoRepositorio;
        private readonly IAlmacenRepositorio _almacenRepositorio;
        private readonly IProductoRepositorio _productoRepositorio;
        private readonly IAlmacenUbicacionRepositorio _almacenUbicacionRepositorio;
        private readonly IDistribucionInventarioRepositorio _distribucionInventarioRepositorio;
        private readonly ISalidaProductoRepositorio _salidaProductoRepositorio;
        private readonly IListaProductoSalidaRepositorio _listaProductoSalidaRepositorio;
#pragma warning restore 0649

        public UnitOfWork()
        {
            _conexion = ConexionBd.ObtenerInstancia().ObtenerStringConexion();
        }

        public IUsuarioRepositorio UsuarioRepositorio => _usuarioRepositorio ?? new UsuarioRepositorio(_conexion);

        public IInventarioProductoRepositorio InventarioProductoRepositorio => _inventarioProductoRepositorio ?? new InventarioProductoRepositorio(_conexion);

        public IAlmacenRepositorio AlmacenRepositorio => _almacenRepositorio ?? new AlmacenRepositorio(_conexion);

        public IProductoRepositorio ProductoRepositorio => _productoRepositorio ?? new ProductoRepositorio(_conexion);

        public IAlmacenUbicacionRepositorio AlmacenUbicacionRepositorio => _almacenUbicacionRepositorio ?? new AlmacenUbicacionRepositorio(_conexion);

        public IDistribucionInventarioRepositorio DistribucionInventarioRepositorio => _distribucionInventarioRepositorio ?? new DistribucionInventarioRepositorio(_conexion);

        public ISalidaProductoRepositorio SalidaProductoRepositorio => _salidaProductoRepositorio ?? new SalidaProductoRepositorio(_conexion);

        public IListaProductoSalidaRepositorio ListaProductoSalidaRepositorio => _listaProductoSalidaRepositorio ?? new ListaProductoSalidaRepositorio(_conexion);

        public void Dispose()
        {
            _conexion?.Dispose();
        }
    }
}