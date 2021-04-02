using SapInventario.Aplicacion.Common.Comportamientos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace SapInventario.Aplicacion.Common.Comportamientos
{
    public class SeguridadServicioRepositorio : ISeguridadServicioRepositorio
    {
        public string EncriptarPassword(string passUser)
        {
            var sSourcePass = passUser;
            var tmpSource = ASCIIEncoding.ASCII.GetBytes(sSourcePass);
            var tmpHash = new MD5CryptoServiceProvider().ComputeHash(tmpSource);
            StringBuilder sOutput = new StringBuilder(tmpHash.Length);
            for (int i = 0; i < tmpHash.Length; i++)
            {
                sOutput.Append(tmpHash[i].ToString("X2"));
            }

            return sOutput.ToString();
        }
    }
}