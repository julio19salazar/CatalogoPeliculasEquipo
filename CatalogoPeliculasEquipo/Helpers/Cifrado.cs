using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CatalogoPeliculasEquipo.Helpers
{
    public class Cifrado
    {
        public static string GetHash(string cadena)
        {
            //SALT - sal

            var algoritmo = SHA512.Create();
            //convertir la cadena a un byte[]
            var arreglo = Encoding.UTF8.GetBytes(cadena);

            var hash = algoritmo.ComputeHash(arreglo).Select(x => x.ToString("x2"));
    
            return string.Join("", hash);
        }
    }
}
