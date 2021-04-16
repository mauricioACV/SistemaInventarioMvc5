using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SapInventario.Entidades
{
    public class DistribucionInventario
    {
        public int Id { get; set; }
        public int CodigoAlmacen { get; set; }
        public int Stock { get; set; }
    }
}