using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SapInventario.Entidades.Interfaces
{
    public interface IInventarioProductoServicio
    {
        bool RegistrarDistribuirProducto(InventarioProducto productoRegistrar);
        List<Almacen> ObtenerListaAlmacenes();
        List<Producto> ObtenerListadoProductosPorPalabraClave(string codigoClave, string palabraClave);
        List<AlmacenUbicacion> ObtenerUbicacionAlmacenPorCodigoAlmacen(int codigoAlmacen);
        int ObtenerAlmacenProductoExistenteDistribucion(string codigoSap);
    }
}
