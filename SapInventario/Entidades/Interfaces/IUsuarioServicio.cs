using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SapInventario.Entidades.Interfaces
{
    public interface IUsuarioServicio
    {
        Usuario ObtenerUsuario(string user, string pass);
    }
}
