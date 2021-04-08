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
        int ObtenerStockTotalProductoPorCodigoSap(string codigoSap);
        int ObtenerAlmacenProductoExistenteDistribucion(string codigoSap);
    }
}
