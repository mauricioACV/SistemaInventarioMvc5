using SapInventario.Entidades;
using SapInventario.Entidades.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SapInventario.Infraestructura.Data.Repositorios
{
    public class ProductoRepositorio : IProductoRepositorio
    {
        private readonly SqlConnection _conexion;

        public ProductoRepositorio(SqlConnection conexion)
        {
            _conexion = conexion;
        }

        public List<Producto> ObtenerListadoProductosPorPalabraClave(string codigoClave, string palabraClave)
        {
            SqlCommand cmd = null;
            SqlDataReader dr = null;
            List<Producto> ListadoProductos = new List<Producto>();

            var query = @"select CodigoSap, NombreProducto from Producto where UPPER(CodigoSap) = @codigoClave OR UPPER(NombreProducto) LIKE UPPER(@palabraClave) order by CodigoSap";

            cmd = new SqlCommand(query, _conexion);
            cmd.Parameters.AddWithValue("@codigoClave", codigoClave);
            cmd.Parameters.AddWithValue("@palabraClave", palabraClave);
            _conexion.Open();

            try
            {
                using (dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Producto objProducto = new Producto
                        {
                            CodigoSap = dr["CodigoSap"].ToString(),
                            NombreProducto = dr["NombreProducto"].ToString()
                        };

                        ListadoProductos.Add(objProducto);
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _conexion.Close();
            }

            return ListadoProductos;
        }
    }
}