using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SapInventario.Entidades.Interfaces
{
    public interface ISalidaProductoServicio
    {
        List<Producto> ObtenerListadoProductosPorPalabraClave(string codigoClave, string palabraClave);
        List<DistribucionInventario> ObtenerStockTotaAndAlmacenlProductoPorCodigoSap(string codigoSap);
        int ObtenerAlmacenProductoExistenteDistribucion(string codigoSap);
        int ObtenerUltimoNumeroActa();
        int ObtenerStockPorIdRegistro(string IdRegistro);
        bool ActualizarStockAlmacenPorIdRegistro(string idRegistro, int stock);
        bool RegistrarActa(SalidaProducto objSalidaProducto);
    }
}
