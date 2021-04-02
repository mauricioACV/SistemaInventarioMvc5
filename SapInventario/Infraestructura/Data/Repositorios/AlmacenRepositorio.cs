using SapInventario.Entidades;
using SapInventario.Entidades.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SapInventario.Infraestructura.Data.Repositorios
{
    public class AlmacenRepositorio : IAlmacenRepositorio
    {
        private readonly SqlConnection _conexion;

        public AlmacenRepositorio(SqlConnection conexion)
        {
            _conexion = conexion;
        }

        public List<Almacen> ObtenerListaAlmacenes()
        {
            SqlCommand cmd = null;
            SqlDataReader dr = null;
            List<Almacen> ListadoAlmacenes = new List<Almacen>();

            var query = @"select CodigoAlmacen, NombreAlmacen from Almacen";
            cmd = new SqlCommand(query, _conexion);
            _conexion.Open();

            try
            {
                using (dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Almacen objAlmacen = new Almacen
                        {
                            CodigoAlmacen = Convert.ToInt32(dr["CodigoAlmacen"]),
                            NombreAlmacen = dr["NombreAlmacen"].ToString()
                        };

                        ListadoAlmacenes.Add(objAlmacen);
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

            return ListadoAlmacenes;
        }
    }
}