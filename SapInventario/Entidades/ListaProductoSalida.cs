using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SapInventario.Entidades
{
    public class ListaProductoSalida
    {
        public int Id { get; set; }
        public int NumActa { get; set; }
        public string CodigoSap { get; set; }
        public int cantidad { get; set; }
        public int almacen { get; set; }
    }
}