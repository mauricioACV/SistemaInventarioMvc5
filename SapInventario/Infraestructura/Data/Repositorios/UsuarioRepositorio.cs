using SapInventario.Entidades;
using SapInventario.Entidades.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SapInventario.Infraestructura.Data.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly SqlConnection _conexion;

        public UsuarioRepositorio(SqlConnection conexion)
        {
            _conexion = conexion;
        }

        public Usuario ObtenerUsuario(string user, string pass)
        {
            SqlCommand cmd = null;
            SqlDataReader dr = null;
            Usuario objUsuario = new Usuario();

            var query = @"select * from usuario where NombreUsuario = @pNombreUsuario and password = @pPassword";
            cmd = new SqlCommand(query, _conexion);
            cmd.Parameters.AddWithValue("@pNombreUsuario", user);
            cmd.Parameters.AddWithValue("@pPassword", pass);
            _conexion.Open();

            try
            {
                using (dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        objUsuario.Nombres = dr["Nombres"].ToString();
                        objUsuario.Apellidos = dr["Apellidos"].ToString();
                        objUsuario.NombreUsuario = dr["NombreUsuario"].ToString();
                        objUsuario.Email = dr["Email"].ToString();
                        objUsuario.IdRol = Convert.ToInt32(dr["IdRol"]);
                        objUsuario.Rut = Convert.ToInt32(dr["Rut"]);
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

            return objUsuario;
        }

        public List<int> ObtenerOperacionesRolUsuario(int idRol, int idOperacion)
        {
            SqlCommand cmd = null;
            SqlDataReader dr = null;
            List<int> listadoOperacionesRol = new List<int>();

            var query = @"select idOperacion from rol_operacion where idRol = @pIdRol and idOperacion = @pIdOperacion";
            cmd = new SqlCommand(query, _conexion);
            cmd.Parameters.AddWithValue("@pIdRol", idRol);
            cmd.Parameters.AddWithValue("@pIdOperacion", idOperacion);
            _conexion.Open();

            try
            {
                using (dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        listadoOperacionesRol.Add(Convert.ToInt32(dr["idOperacion"]));
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

            return listadoOperacionesRol;
        }

        public string ObtenerRolUsuario(int idRolUser)
        {
            SqlCommand cmd = null;
            SqlDataReader dr = null;
            string rolUsuario = null;

            var query = @"select Nombre from Rol where Id = @IdRolUser";
            cmd = new SqlCommand(query, _conexion);
            cmd.Parameters.AddWithValue("@IdRolUser", idRolUser);
            _conexion.Open();

            try
            {
                using (dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        rolUsuario = dr["Nombre"].ToString();
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

            return rolUsuario;
        }
    }
}