using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SapInventario.Entidades
{
    public class InventarioProducto
    {
        public int Id { get; set; }
        public string CodigoSap { get; set; }
        public int Stock { get; set; }
        public int ValorUnitario { get; set; }
        public int CodigoAlmacen { get; set; }
        public string CodigoUbicacion { get; set; }
        public string FechaCompra { get; set; }
    }
}