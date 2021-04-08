using System;

namespace SapInventario.Entidades.Interfaces.IUnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IUsuarioRepositorio UsuarioRepositorio { get; }
        IInventarioProductoRepositorio InventarioProductoRepositorio { get; }
        IAlmacenRepositorio AlmacenRepositorio { get; }
        IProductoRepositorio ProductoRepositorio { get; }
        IAlmacenUbicacionRepositorio AlmacenUbicacionRepositorio { get; }
        IDistribucionInventarioRepositorio DistribucionInventarioRepositorio { get; }
        ISalidaProductoRepositorio SalidaProductoRepositorio { get; }
    }
}
