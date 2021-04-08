using SapInventario.Entidades;
using SapInventario.Entidades.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SapInventario.Infraestructura.Data.Repositorios
{
    public class InventarioProductoRepositorio : IInventarioProductoRepositorio
    {
        private readonly SqlConnection _conexion;

        public InventarioProductoRepositorio(SqlConnection pConexion)
        {
            _conexion = pConexion;
        }

        public bool RegistrarProductoEnInventario(InventarioProducto inventarioProducto)
        {
            bool response = false;
            SqlCommand cmd = null;

            try
            {
                var query = @"INSERT INTO InventarioProducto (CodigoSap, NumOrdenCompra, Stock, ValorUnitario, FechaCompra)
                                VALUES (@CodigoSap, @NumOrdenCompra, @Stock, @ValorUnitario, @FechaCompra)";

                using (cmd = new SqlCommand(query, _conexion))
                {
                    cmd.Parameters.AddWithValue("@CodigoSap", inventarioProducto.CodigoSap);
                    cmd.Parameters.AddWithValue("@NumOrdenCompra", inventarioProducto.NumOrdCompra);
                    cmd.Parameters.AddWithValue("@Stock", inventarioProducto.Stock);
                    cmd.Parameters.AddWithValue("@ValorUnitario", inventarioProducto.ValorUnitario);
                    cmd.Parameters.AddWithValue("@FechaCompra", inventarioProducto.FechaCompra);
                    _conexion.Open();
                    int filas = cmd.ExecuteNonQuery();
                    if(filas > 0) { response = true; }
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