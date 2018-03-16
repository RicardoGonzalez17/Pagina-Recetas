using PaginaRecetas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PaginaRecetas.Controllers
{
    public class UsuarioController : Controller
    {
        private DB_RecetasEntities dbDeRecetas = new DB_RecetasEntities();
        // GET: Usuario
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AgregaroEditar(int d=0)
        {
            Usuario elUsuario = new Usuario();
            return View(elUsuario);
        }
        [HttpPost]
        public ActionResult AgregaroEditar([Bind(Include = "Nombre,Apellidos,Correo,Contrasenia")]Usuario usuarioNuevo)
        {
            if (ModelState.IsValid)
            {
                dbDeRecetas.Usuarios.Add(usuarioNuevo);
                dbDeRecetas.SaveChanges();
            }
            return RedirectToAction("AgregaroEditar");
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