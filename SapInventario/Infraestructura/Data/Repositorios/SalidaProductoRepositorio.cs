using SapInventario.Entidades;
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

        public int ObtenerUltimoNumeroActa()
        {
            SqlCommand cmd = null;
            SqlDataReader dr = null;
            int numeroActa = 0;

            var query = @"select NumActa from SalidaProducto";
            cmd = new SqlCommand(query, _conexion);
            _conexion.Open();

            try
            {
                using (dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        numeroActa = Convert.ToInt32(dr["NumActa"]);
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

            return numeroActa+1;
        }

        public bool RegistrarActa(SalidaProducto objSalidaProducto)
        {
            bool response = false;
            SqlCommand cmd = null;

            try
            {
                var query = @"INSERT INTO SalidaProducto (IdUsuarioEntrega, UnidadDestino, FechaEntrega, RecepcionadoPor, Observaciones, NumActa)
                                VALUES (@IdUsuarioEntrega, @UnidadDestino, @FechaEntrega, @RecepcionadoPor, @Observaciones, @NumActa)";

                using (cmd = new SqlCommand(query, _conexion))
                {
                    cmd.Parameters.AddWithValue("@IdUsuarioEntrega", objSalidaProducto.IdUsuarioEntrega);
                    cmd.Parameters.AddWithValue("@UnidadDestino", objSalidaProducto.UnidadDestino);
                    cmd.Parameters.AddWithValue("@FechaEntrega", objSalidaProducto.FechaEntrega);
                    cmd.Parameters.AddWithValue("@RecepcionadoPor", objSalidaProducto.RecepcionadoPor);
                    cmd.Parameters.AddWithValue("@Observaciones", objSalidaProducto.Observaciones);
                    cmd.Parameters.AddWithValue("@NumActa", objSalidaProducto.NumActa);
                    _conexion.Open();
                    int filas = cmd.ExecuteNonQuery();
                    if (filas > 0) { response = true; }
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

            return response;
        }
    }
}