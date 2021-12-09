using System;
using System.Collections.Generic;

#nullable disable

namespace CatalogoPeliculasEquipo.Models
{
    public partial class Pelicula
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Año { get; set; }
        public string Categoria { get; set; }
        public string Clasificacion { get; set; }
        public string Pais { get; set; }
        public string Sinopsis { get; set; }
        public int Idproductor { get; set; }

        public virtual Productor IdproductorNavigation { get; set; }
    }
}
