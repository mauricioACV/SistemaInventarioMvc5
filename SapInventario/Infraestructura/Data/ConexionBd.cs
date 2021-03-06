using System.Configuration;
using System.Data.SqlClient;

namespace SapInventario.Infraestructura.Data
{
    public class ConexionBd
    {
        #region "Singleton"
        private static ConexionBd conexion = null;
        private ConexionBd() { }
        public static ConexionBd ObtenerInstancia()
        {
            if(conexion == null)
            {
                conexion = new ConexionBd();
            }
            return conexion;
        }
        #endregion


        public SqlConnection ObtenerStringConexion()
        {
            SqlConnection conexion = new SqlConnection
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["ConexionDb"].ToString()
            };
            return conexion;
        }
    }
}