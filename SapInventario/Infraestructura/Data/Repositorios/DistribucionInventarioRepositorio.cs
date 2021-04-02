using SapInventario.Entidades;
using SapInventario.Entidades.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SapInventario.Infraestructura.Data.Repositorios
{
    public class DistribucionInventarioRepositorio : IDistribucionInventarioRepositorio
    {
        private readonly SqlConnection _conexion;

        public DistribucionInventarioRepositorio(SqlConnection pConexion)
        {
            _conexion = pConexion;
        }

        public bool RegistrarProductoEnAlmacen(InventarioProducto inventarioProducto)
        {
            bool response = false;
            SqlCommand cmd = null;

            try
            {
                var query = @"INSERT INTO DistribucionInventario (CodigoSap, Stock, CodigoAlmacen, CodigoUbicacion, FechaActualizacion)
                                VALUES (@CodigoSap, @Stock, @CodigoAlmacen, @CodigoUbicacion, @FechaActualizacion)";

                using (cmd = new SqlCommand(query, _conexion))
                {
                    cmd.Parameters.AddWithValue("@CodigoSap", inventarioProducto.CodigoSap);
                    cmd.Parameters.AddWithValue("@Stock", inventarioProducto.Stock);
                    cmd.Parameters.AddWithValue("@CodigoAlmacen", inventarioProducto.CodigoAlmacen);
                    cmd.Parameters.AddWithValue("@CodigoUbicacion", inventarioProducto.CodigoUbicacion);
                    cmd.Parameters.AddWithValue("@FechaActualizacion", inventarioProducto.FechaCompra);
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

        public bool ActualizarProductoDistribucion(InventarioProducto inventarioProducto)
        {
            bool response = false;
            SqlCommand cmd = null;

            try
            {
                var query = @"UPDATE DistribucionInventario SET Stock = @Stock, CodigoAlmacen = @CodigoAlmacen, FechaActualizacion = @FechaActualizacion WHERE CodigoSap = @CodigoSap and CodigoAlmacen = @CodigoAlmacen";

                using (cmd = new SqlCommand(query, _conexion))
                {
                    cmd.Parameters.AddWithValue("@Stock", inventarioProducto.Stock);
                    cmd.Parameters.AddWithValue("@CodigoAlmacen", inventarioProducto.CodigoAlmacen);
                    cmd.Parameters.AddWithValue("@FechaActualizacion", inventarioProducto.FechaCompra);
                    cmd.Parameters.AddWithValue("@CodigoSap", inventarioProducto.CodigoSap);
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

        public List<InventarioProducto> ObtenerProductoExistenteDistribucion(string codigoSap)
        {
            SqlDataReader dr = null;
            SqlCommand cmd = null;
            List<InventarioProducto> objProducto = new List<InventarioProducto>();

            try
            {
                var query = @"select Id, Stock, CodigoAlmacen from DistribucionInventario where CodigoSap = @CodigoSap";

                using (cmd = new SqlCommand(query, _conexion))
                {
                    cmd.Parameters.AddWithValue("@CodigoSap", codigoSap);
                    _conexion.Open();

                    using (dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            InventarioProducto objInvProducto = new InventarioProducto
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                Stock = Convert.ToInt32(dr["Stock"]),
                                CodigoAlmacen = Convert.ToInt32(dr["CodigoAlmacen"])
                            };

                            objProducto.Add(objInvProducto);                            
                        }
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

            return objProducto;
        }

        public int ObtenerStockPorCodigoSapCodigoAlmacen(string codigoSap, int codigoAlmacen)
        {
            SqlDataReader dr = null;
            SqlCommand cmd = null;
            int stockProducto = 0;

            try
            {
                var query = @"select Stock from DistribucionInventario where CodigoSap = @CodigoSap and CodigoAlmacen = @CodigoAlmacen";

                using (cmd = new SqlCommand(query, _conexion))
                {
                    cmd.Parameters.AddWithValue("@CodigoSap", codigoSap);
                    cmd.Parameters.AddWithValue("@CodigoAlmacen", codigoAlmacen);
                    _conexion.Open();

                    using (dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            stockProducto = Convert.ToInt32(dr["Stock"]);
                        }
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

            return stockProducto;
        }
    }
}