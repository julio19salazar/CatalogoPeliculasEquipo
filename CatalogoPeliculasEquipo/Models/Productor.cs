using System;
using System.Collections.Generic;

#nullable disable

namespace CatalogoPeliculasEquipo.Models
{
    public partial class Productor
    {
        public Productor()
        {
            Peliculas = new HashSet<Pelicula>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Pelicula> Peliculas { get; set; }
    }
}
