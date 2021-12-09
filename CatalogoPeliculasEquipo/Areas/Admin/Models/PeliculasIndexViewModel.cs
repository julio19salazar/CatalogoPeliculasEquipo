using CatalogoPeliculasEquipo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogoPeliculasEquipo.Areas.Admin.Models
{
    public class PeliculasIndexViewModel
    {
        public Pelicula Peliculas { get; set; }
        public IEnumerable<Pelicula> ListaDePeliculas { get; set; }
        public IEnumerable<Productor> Productores { get; set; }
      
    }
}
