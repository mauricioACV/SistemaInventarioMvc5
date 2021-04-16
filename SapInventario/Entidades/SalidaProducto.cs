using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SapInventario.Entidades
{
    public class SalidaProducto
    {
        public int Id { get; set; }
        public string CodigoSap { get; set; }
        public int Unidades { get; set; }
        public int IdUsuarioEntrega { get; set; }
        public string UnidadDestino { get; set; }
        public string FechaEntrega { get; set; }
        public string Constancia { get; set; }
        public string RecepcionadoPor { get; set; }
        public string Observaciones { get; set; }
        public int CodigoAlmacen { get; set; }
        public int NumActa { get; set; }
    }
}