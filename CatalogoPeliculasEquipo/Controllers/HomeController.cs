using CatalogoPeliculasEquipo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogoPeliculasEquipo.Controllers
{
    public class HomeController : Controller
    {
        public peliculasContext Context { get; }
        public HomeController(peliculasContext context)
        {
            Context = context;
        }
    
        public IActionResult Index()
        {
            var p = Context.Peliculas.OrderBy(x => x.Nombre);
            return View(p);
        }
        [Route("pelicula/{nombre}")]
        public IActionResult InformacionPelicula(string nombre)
        {
            string nombre2 = nombre == null ? "" : nombre.Replace("-", " ");


            var pelicula = Context.Peliculas
                .Include(x => x.IdproductorNavigation)
                   
                .FirstOrDefault(x => x.Nombre == nombre2 || x.Nombre == nombre);

            if (pelicula == null)
            {
                return RedirectToAction("Index");
            }

            return View(pelicula);

        }
        [Route("/acercade")]
        public IActionResult AcercaDe()
        {
            var p = Context.Peliculas.OrderBy(x => x.Nombre);
            return View(p);
        }
    }
}
