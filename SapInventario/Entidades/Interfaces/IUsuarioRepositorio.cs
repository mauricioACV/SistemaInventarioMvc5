using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SapInventario.Entidades.Interfaces
{
    public interface IUsuarioRepositorio
    {
        Usuario ObtenerUsuario(string user, string pass);
        List<int> ObtenerOperacionesRolUsuario(int idRol, int idOperacion);
        string ObtenerRolUsuario(int idRolUser);
    }
}
