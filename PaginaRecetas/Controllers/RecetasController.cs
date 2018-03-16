using PaginaRecetas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PaginaRecetas.Controllers
{
    public class RecetasController : Controller
    {
        DB_RecetasEntities dbDeRecetas = new DB_RecetasEntities();
        // GET: Recetas
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AgregarReceta(int id=0)
        {
            Receta lareceta = new Receta();
            return View(lareceta);
        }
        [HttpPost]
        public ActionResult AgregarReceta([Bind(Include ="Nombre, Instrucciones, Fecha_Alta, Imagen, Video, ID_Usuario")]Receta recetaNueva)
        {
            dbDeRecetas.Recetas.Add(recetaNueva);
            dbDeRecetas.SaveChanges();
            return RedirectToAction("AgregarReceta");
        }

        public ActionResult MostrarRecetas()
        {
            return View(ObtenerRecetas());
        }

        IEnumerable<Receta> ObtenerRecetas()
        {
            using (DB_RecetasEntities baseDeDatos = new DB_RecetasEntities())
            {
                return baseDeDatos.Recetas.ToList();
            }
        }
    }
}