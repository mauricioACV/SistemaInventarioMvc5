using SapInventario.Entidades;
using SapInventario.Entidades.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SapInventario.Infraestructura.Data.Repositorios
{
    public class AlmacenUbicacionRepositorio : IAlmacenUbicacionRepositorio
    {
        private readonly SqlConnection _conexion;

        public AlmacenUbicacionRepositorio(SqlConnection conexion)
        {
            _conexion = conexion;
        }

        public List<AlmacenUbicacion> ObtenerUbicacionAlmacenPorCodigoAlmacen(int codigoAlmacen)
        {
            SqlCommand cmd = null;
            SqlDataReader dr = null;
            List<AlmacenUbicacion> ListAlmacenUbicacion = new List<AlmacenUbicacion>();

            var query = @"select CodigoUbicacion, Descripcion from AlmacenUbicacion where CodigoAlmacen = @CodigoAlmacen";
            cmd = new SqlCommand(query, _conexion);
            cmd.Parameters.AddWithValue("@CodigoAlmacen", codigoAlmacen);

            _conexion.Open();

            try
            {
                using (dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        AlmacenUbicacion objAlmacenUbicacion = new AlmacenUbicacion
                        {
                            CodigoUbicacion = dr["CodigoUbicacion"].ToString(),
                            Descripcion = dr["Descripcion"].ToString()
                        };

                        ListAlmacenUbicacion.Add(objAlmacenUbicacion);
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

            return ListAlmacenUbicacion;
        }
    }
}