using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SapInventario.Entidades
{
    public class Usuario
    {
        public int Rut { get; set; }
        public string NombreUsuario { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime Fecha { get; set; }
        public int IdRol { get; set; }
        public string NombreRol { get; set; }
    }
}