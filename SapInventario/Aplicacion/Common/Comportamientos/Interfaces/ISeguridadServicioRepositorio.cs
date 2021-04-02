using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SapInventario.Aplicacion.Common.Comportamientos.Interfaces
{
    public interface ISeguridadServicioRepositorio
    {
        string EncriptarPassword(string passUser);
    }
}
