using SapInventario.Entidades.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SapInventario.Infraestructura.Data.Repositorios
{
    public class SalidaProductoRepositorio : ISalidaProductoRepositorio
    {
        private readonly SqlConnection _conexion;

        public SalidaProductoRepositorio(SqlConnection conexion)
        {
            _conexion = conexion;
        }

        public int ObtenerTotalUnidadesEntregadasProducto(string codigoSap)
        {
            throw new NotImplementedException();
        }
    }
}