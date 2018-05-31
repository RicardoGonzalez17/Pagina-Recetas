using Newtonsoft.Json;
using PaginaRecetas.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using static System.Net.WebRequestMethods;

namespace PaginaRecetas.Controllers
{
    public class UsuarioController : Controller
    {
        
        private BD_PaginaRecetasEntities2 dbDeRecetas = new BD_PaginaRecetasEntities2(ConnectionString.Value);
        // GET: Usuario
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetAll()
        {
            using (BD_PaginaRecetasEntities2 entity = new BD_PaginaRecetasEntities2(ConnectionString.Value))
            {
                var todos = entity
                    .USUARIOS
                    .Select(x => new {x.ID_Usuario,x.imagen,x.Nivel,x.Nombre,x.Correo });
                return Content(JsonConvert.SerializeObject(todos), "application/json");
            }
        }
        public ActionResult Get()
        {
            var idusuario = Convert.ToUInt32(Session["ID_Usuario"]);
            using (var entity = new BD_PaginaRecetasEntities2(ConnectionString.Value))
            {
                var usuariofull = entity
                    .USUARIOS
                    .Where(x => x.ID_Usuario == idusuario)
                    .Select(x => new { x.Nombre,x.Nivel, x.Contrasenia, x.Correo, x.imagen})
                    .FirstOrDefault();
                return Content(JsonConvert.SerializeObject(usuariofull), "application/json");
            }
        }
        public ActionResult GeyIdUsuario(int idusuario)
        {
            using (var entity = new BD_PaginaRecetasEntities2(ConnectionString.Value))
            {
                var usuario = entity
                    .USUARIOS
                    .Where(x => x.ID_Usuario == idusuario)
                    .Select(x => new { x.ID_Usuario, x.imagen, x.Nivel, x.Nombre, x.Correo })
                    .FirstOrDefault();
                return Content(JsonConvert.SerializeObject(usuario), "application / json");
                //var prueba = JsonConvert.DeserializeObject<Usuario>(json);
            }
        }

        public bool ActualizarUsuario(string nombre, string correo, string contrasenia, string imagen )
        {
            using (BD_PaginaRecetasEntities2 entity = new BD_PaginaRecetasEntities2(ConnectionString.Value))
            {
                var idusuario = Convert.ToUInt32(Session["ID_Usuario"]);
                if (!contrasenia.Equals("") && !correo.Equals("") && !nombre.Equals(""))
                {
                    var nuevosDatos = entity
                        .USUARIOS
                        .Where(x => x.ID_Usuario == idusuario)
                        .Select(x => x)
                        .FirstOrDefault();
                    nuevosDatos.Nombre = nombre;
                    nuevosDatos.Correo = correo;
                    nuevosDatos.Contrasenia = contrasenia;
                    // QUEDA PENDIENTE LA IMAGEN 
                    entity.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }

        public bool Login(string correo, string contrasenia) {
            using (var entity = new BD_PaginaRecetasEntities2(ConnectionString.Value))
            {
                var usuario =
                entity
                .USUARIOS
                .FirstOrDefault(x => x.Correo == correo && x.Contrasenia == contrasenia);
                if(usuario != null)
                {
                    Session["ID_Usuario"] = usuario.ID_Usuario;
                    Session["Nombre"] = usuario.Nombre;
                    Session["Correo"] = usuario.Correo;
                }
                return usuario != null;
            }
        }

        public bool Logout() {
            Session.Clear();
            return true;
        }
        public static byte[] ImageToBinary(string _path)
        {
            if (!string.IsNullOrEmpty(_path))
            {
                FileStream fS = new FileStream(_path, FileMode.Open, FileAccess.Read);
                byte[] b = new byte[fS.Length];
                fS.Read(b, 0, (int)fS.Length);
                fS.Close();
                return b;
            } return null;
        }

        public bool Add(string nombre, string correo, string contrasenia, string imagen)
        {
            using (BD_PaginaRecetasEntities2 entity = new BD_PaginaRecetasEntities2(ConnectionString.Value))
            {
                entity
                .USUARIOS
                .Add(new USUARIOS
                {
                    Nombre = nombre,
                    Correo = correo,
                    Contrasenia = contrasenia,
                    Nivel = 1,
                    imagen = ImageToBinary(string.IsNullOrEmpty(imagen)?string.Empty:"C:\\Users\\ricar\\Pictures\\Imagenes Usuarios\\" + imagen)
                    });
                entity.SaveChanges();
            }
            return true;
        }
        public ActionResult AgregaroEditar(int d=0)
        {
            USUARIOS elUsuario = new USUARIOS();
            return View(elUsuario);
        }
        [HttpPost]
        public ActionResult AgregaroEditar([Bind(Include = "Nombre,Correo,Contrasenia")]USUARIOS usuarioNuevo)
        {
            if (ModelState.IsValid)
            {
                dbDeRecetas.USUARIOS.Add(usuarioNuevo);
                dbDeRecetas.SaveChanges();
            }
            return RedirectToAction("AgregaroEditar");
        }

        public ActionResult MostrarRecetas()
        {
            return View(ObtenerRecetas());
        }

        IEnumerable<RECETAS> ObtenerRecetas()
        {
            using (BD_PaginaRecetasEntities2 baseDeDatos = new BD_PaginaRecetasEntities2(ConnectionString.Value))
            {
                return baseDeDatos.RECETAS.ToList();
            }
        }
        public class Usuario
        {
            public String Nombre { get; set; }

        }

    }
}