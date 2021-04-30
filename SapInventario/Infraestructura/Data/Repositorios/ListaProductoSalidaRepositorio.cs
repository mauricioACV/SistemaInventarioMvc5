using SapInventario.Entidades;
using SapInventario.Entidades.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SapInventario.Infraestructura.Data.Repositorios
{
    public class ListaProductoSalidaRepositorio : IListaProductoSalidaRepositorio
    {
        private readonly SqlConnection _conexion;

        public ListaProductoSalidaRepositorio(SqlConnection conexion)
        {
            _conexion = conexion;
        }

        public bool RegistrarProductosSalida(List<ListaProductoSalida> listaSalidaProducto)
        {
            bool response = false;
            SqlCommand cmd = null;

            try
            {
                var query = @"INSERT INTO ListaProductoSalida (NumActa, CodigoSap, Unidades, CodigoAlmacen)
                                VALUES (@NumActa, @CodigoSap, @Unidades, @CodigoAlmacen)";

                int filas = 0;
                _conexion.Open();

                foreach (var producto in listaSalidaProducto)
                {
                    using (cmd = new SqlCommand(query, _conexion))
                    {
                        cmd.Parameters.AddWithValue("@NumActa", producto.NumActa);
                        cmd.Parameters.AddWithValue("@CodigoSap", producto.CodigoSap);
                        cmd.Parameters.AddWithValue("@Unidades", producto.cantidad);
                        cmd.Parameters.AddWithValue("@CodigoAlmacen", producto.almacen);
                        filas = cmd.ExecuteNonQuery();
                    }
                }

                if (filas > 0) { response = true; }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _conexion.Close();
            }

            return response;
        }
    }
}