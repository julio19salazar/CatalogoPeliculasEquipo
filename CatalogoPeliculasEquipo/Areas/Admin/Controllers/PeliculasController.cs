using CatalogoPeliculasEquipo.Areas.Admin.Models;
using CatalogoPeliculasEquipo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogoPeliculasEquipo.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    //[Authorize(Roles = "Administrador")]
    public class PeliculasController : Controller
    {
        public peliculasContext Context { get; }
        public IWebHostEnvironment Host { get; }

        public PeliculasController(peliculasContext context, IWebHostEnvironment host)
        {
            Context = context;
            Host = host;
        }
        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<Pelicula> p = Context.Peliculas.OrderBy(x => x.Nombre);
            return View(p);
        }
        [Authorize(Roles = "Administrador")]
        public IActionResult Agregar()
        {
            return View(new PeliculasIndexViewModel
            {
                Productores = Context.Productors.OrderBy(x => x.Nombre)
            });
        }
        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public IActionResult Agregar(PeliculasIndexViewModel vm, IFormFile foto)
        {   if (Context.Peliculas.Any(x => x.Nombre == vm.Peliculas.Nombre))
            {
                ModelState.AddModelError("", "Ya existe una película con el mismo nombre");
                vm.Productores = Context.Productors.OrderBy(x => x.Nombre);
                return View(vm);
            }
            
            if (string.IsNullOrWhiteSpace(vm.Peliculas.Nombre))
            {
                ModelState.AddModelError("", "El nombre de la película no puede ir vacío");
                vm.Productores = Context.Productors.OrderBy(x => x.Nombre);
                return View(vm);
            }
            if (string.IsNullOrWhiteSpace(vm.Peliculas.Año))
            {
                ModelState.AddModelError("", "El película de la pelicula está vacío");
                vm.Productores = Context.Productors.OrderBy(x => x.Nombre);
                return View(vm);
            }
            if (string.IsNullOrWhiteSpace(vm.Peliculas.Categoria))
            {
                ModelState.AddModelError("", "La categoría de la película está vacía");
                vm.Productores = Context.Productors.OrderBy(x => x.Nombre);
                return View(vm);
            }
            if (string.IsNullOrWhiteSpace(vm.Peliculas.Clasificacion))
            {
                ModelState.AddModelError("", "La clasificación de la película está vacía");
                vm.Productores = Context.Productors.OrderBy(x => x.Nombre);
                return View(vm);
            }
            if (string.IsNullOrWhiteSpace(vm.Peliculas.Pais))
            {
                ModelState.AddModelError("", "El país de la película está vacío");
                vm.Productores = Context.Productors.OrderBy(x => x.Nombre);
                return View(vm);
            }
            if (string.IsNullOrWhiteSpace(vm.Peliculas.Sinopsis))
            {
                ModelState.AddModelError("", "La sinopsis de la película está vacía");
                vm.Productores = Context.Productors.OrderBy(x => x.Nombre);
                return View(vm);
            }
            if (vm.Peliculas.Idproductor<=0)
            {
                ModelState.AddModelError("", "Seleccione una productora para poder continuar");
                vm.Productores = Context.Productors.OrderBy(x => x.Nombre);
                return View(vm);
            }
           
            if (foto != null)
            {
                if (foto.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("", "Solo se permite la carga de archivos JPG");
                    vm.Productores = Context.Productors.OrderBy(x => x.Nombre);
                    return View(vm);
                }
                if (foto.Length > 1024 * 1024 * 5)
                {
                    ModelState.AddModelError("", "No se permite la carga de archivos mayores a 5MB");
                    vm.Productores = Context.Productors.OrderBy(x => x.Nombre);
                    return View(vm);
                }
            }
            Context.Add(vm.Peliculas);
            Context.SaveChanges();
            if (foto != null)
            {
                var path = Host.WebRootPath+"/imgs_peliculas/"+vm.Peliculas.Id + "_peli.jpg";
                FileStream fs = new FileStream(path, FileMode.Create);
                foto.CopyTo(fs);
                fs.Close();
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Modificar(int id)
        {
            Pelicula p = Context.Peliculas.FirstOrDefault(x => x.Id == id);
         
            if (p == null)
            {
                return RedirectToAction("Index");
            }
            PeliculasIndexViewModel vm = new PeliculasIndexViewModel
            {
                Peliculas = p,
                Productores = Context.Productors.OrderBy(x => x.Nombre)
            };
            return View(vm);
        }
        [HttpPost]
        public IActionResult Modificar(PeliculasIndexViewModel vm, IFormFile foto)
        {


            var p = Context.Peliculas.FirstOrDefault(x => x.Id == vm.Peliculas.Id);
            if (p == null)
            {
                return RedirectToAction("Index");
            }
           
            if (Context.Peliculas.Any(x => x.Nombre == vm.Peliculas.Nombre && x.Id!=vm.Peliculas.Id))
            {
                ModelState.AddModelError("", "Ya existe una película con el mismo nombre");
                vm.Productores = Context.Productors.OrderBy(x => x.Nombre);
                return View(vm);
            }

            if (string.IsNullOrWhiteSpace(vm.Peliculas.Nombre))
            {
                ModelState.AddModelError("", "El nombre de la película no puede ir vacío");
                vm.Productores = Context.Productors.OrderBy(x => x.Nombre);
                return View(vm);
            }
            if (string.IsNullOrWhiteSpace(vm.Peliculas.Año))
            {
                ModelState.AddModelError("", "El película de la pelicula está vacío");
                vm.Productores = Context.Productors.OrderBy(x => x.Nombre);
                return View(vm);
            }
            if (string.IsNullOrWhiteSpace(vm.Peliculas.Categoria))
            {
                ModelState.AddModelError("", "La categoría de la película está vacía");
                vm.Productores = Context.Productors.OrderBy(x => x.Nombre);
                return View(vm);
            }
            if (string.IsNullOrWhiteSpace(vm.Peliculas.Clasificacion))
            {
                ModelState.AddModelError("", "La clasificación de la película está vacía");
                vm.Productores = Context.Productors.OrderBy(x => x.Nombre);
                return View(vm);
            }
            if (string.IsNullOrWhiteSpace(vm.Peliculas.Pais))
            {
                ModelState.AddModelError("", "El país de la película está vacío");
                vm.Productores = Context.Productors.OrderBy(x => x.Nombre);
                return View(vm);
            }
            if (string.IsNullOrWhiteSpace(vm.Peliculas.Sinopsis))
            {
                ModelState.AddModelError("", "La sinopsis de la película está vacía");
                vm.Productores = Context.Productors.OrderBy(x => x.Nombre);
                return View(vm);
            }
            if (vm.Peliculas.Idproductor <= 0)
            {
                ModelState.AddModelError("", "Seleccione una productora para poder continuar");
                vm.Productores = Context.Productors.OrderBy(x => x.Nombre);
                return View(vm);
            }

            if (foto != null)
            {
                if (foto.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("", "Solo se permite la carga de archivos JPG");
                    vm.Productores = Context.Productors.OrderBy(x => x.Nombre);
                    return View(vm);
                }
                if (foto.Length > 1024 * 1024 * 5)
                {
                    ModelState.AddModelError("", "No se permite la carga de archivos mayores a 5MB");
                    vm.Productores = Context.Productors.OrderBy(x => x.Nombre);
                    return View(vm);
                }
            }
            p.Nombre = vm.Peliculas.Nombre;
            p.Año = vm.Peliculas.Año;
            p.Categoria = vm.Peliculas.Categoria;
            p.Clasificacion = vm.Peliculas.Clasificacion;
            p.Pais = vm.Peliculas.Pais;
            p.Sinopsis = vm.Peliculas.Sinopsis;
            p.Idproductor = vm.Peliculas.Idproductor;
            
           
     Context.Update(p);
            Context.SaveChanges();
            if (foto != null)
            {
                var path = Host.WebRootPath + "/imgs_peliculas/" + vm.Peliculas.Id + "_peli.jpg";
                FileStream fs = new FileStream(path, FileMode.Create);
                foto.CopyTo(fs);
                fs.Close();
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Eliminar(int id)
        {
            Pelicula raza = Context.Peliculas.FirstOrDefault(x => x.Id == id);
            if (raza == null)
            {
                return RedirectToAction("Index");
            }
            return View(raza);
        }

        [HttpPost]
        public IActionResult Eliminar(Pelicula vm)
        {
            Pelicula p = Context.Peliculas.FirstOrDefault(x => x.Id == vm.Id);
          
            if (p == null)
            {
                ModelState.AddModelError("", "No se encontro la pelicula, puede que no exista o ya haya sido eliminado");
                return View(vm);
            }

            Context.Remove(p);
          
            Context.SaveChanges();

            string path = Host.WebRootPath + "/imgs_pelciulas/" +p.Id + "_peli.jpg";
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }

            return RedirectToAction("Index");
        }



    }
}
