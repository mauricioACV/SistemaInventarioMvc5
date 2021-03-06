using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SapInventario.Entidades.Interfaces
{
    public interface IDistribucionInventarioRepositorio
    {
        bool RegistrarProductoEnAlmacen(InventarioProducto inventarioProducto);
        bool ActualizarProductoDistribucion(InventarioProducto inventarioProducto);
        bool ActualizarStockAlmacenPorIdRegistro(string idRegistro, int stock);
        int ObtenerAlmacenProductoExistenteDistribucion(string codigoSap);
        int ObtenerStockPorCodigoSapCodigoAlmacen(string codigoSap, int codigoAlmacen);
        int ObtenerStockPorIdRegistro(string IdRegistro);
        List<DistribucionInventario> ObtenerStockAndAlmacenTotalProductoPorCodigoSap(string codigoSap);
    }
}
