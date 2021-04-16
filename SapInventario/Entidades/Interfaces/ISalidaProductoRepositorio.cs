using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SapInventario.Entidades.Interfaces
{
    public interface ISalidaProductoRepositorio
    {
        int ObtenerTotalUnidadesEntregadasProducto(string codigoSap);
        int ObtenerUltimoNumeroActa();
        bool RegistrarActa(SalidaProducto objSalidaProducto);
    }
}
