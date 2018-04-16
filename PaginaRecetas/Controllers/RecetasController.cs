using Newtonsoft.Json;
using PaginaRecetas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;

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

        //public ActionResult AgregarReceta(int id=0)
        //{
        //    Receta lareceta = new Receta();
        //    return View(lareceta);
        //}
        //[HttpPost]
        //public ActionResult AgregarReceta([Bind(Include ="Nombre, Instrucciones, Fecha_Alta, Imagen, Video, ID_Usuario")]Receta recetaNueva)
        //{
        //    dbDeRecetas.Recetas.Add(recetaNueva);
        //    dbDeRecetas.SaveChanges();
        //    return RedirectToAction("AgregarReceta");
        //}

        //public ActionResult MostrarRecetas()
        //{
        //    return View(ObtenerRecetas());
        //}

        //IEnumerable<Receta> ObtenerRecetas()
        //{
        //    using (DB_RecetasEntities baseDeDatos = new DB_RecetasEntities())
        //    {
        //        return baseDeDatos.Recetas.ToList();
        //    }
        //}
        private static string categorias = "[{\"PK_Categoria\":1, \"Categoria\":\"Mexicana\"},{\"PK_Categoria\":2, \"Categoria\":\"Comida rápida\"},{\"PK_Categoria\":3, \"Categoria\":\"Italiana\"}]";
        private static string unidades = "[{\"PK_Unidad\":1, \"Unidad\":\"Gramos\"}, {\"PK_Unidad\":2, \"Unidad\":\"Kilogramos\"}, {\"PK_Unidad\":3, \"Unidad\":\"Cucharada\"}, {\"PK_Unidad\":4, \"Unidad\":\"Pieza\"}, {\"PK_Unidad\":5, \"Unidad\":\"Pisca\"} ]";
        private static string json = "[{\"PK_Receta\": 1, \"Receta\": \"Enchiladas\", \"Categoria\":\"Mexicana\", \"Usuario\":\"Aarón Teposte\", \"Fecha\":\"2018-03-16 12:07:00\", \"Puntuacion\":3 },{\"PK_Receta\": 2, \"Receta\": \"Pizza\", \"Categoria\":\"Italiana\", \"Usuario\":\"Ricardo Gonzalez\", \"Fecha\":\"2018-03-16 12:07:00\", \"Puntuacion\":5 },{\"PK_Receta\": 3, \"Receta\": \"Hamburguesa\", \"Categoria\":\"Comida rápida\", \"Usuario\":\"Pedro López\", \"Fecha\":\"2018-03-16 12:07:00\", \"Puntuacion\":4 },{\"PK_Receta\": 4, \"Receta\": \"Maruchan\", \"Categoria\":\"Comida rápida\", \"Usuario\":\"Lupita Huevona\", \"Fecha\":\"2018-03-16 12:07:00\", \"Puntuacion\":1 },{\"PK_Receta\": 5, \"Receta\": \"Pollo en crema de chipotle\", \"Categoria\":\"Mexicana\", \"Usuario\":\"Francisco Márquez\", \"Fecha\":\"2018-03-16 12:07:00\", \"Puntuacion\":0 },{\"PK_Receta\": 6, \"Receta\": \"Quesadillas\", \"Categoria\":\"Mexicana\", \"Usuario\":\"Ricardo Gonzalez\", \"Fecha\":\"2018-03-16 12:07:00\", \"Puntuacion\":5 } ]";
        public ActionResult Get() {
            using (DB_RecetasEntities entity = new DB_RecetasEntities())
            {
                var obtenerInfo = entity
                    .tablaPrincipals
                    .Select(x => new {x.Expr3,x.Fecha_Alta, x.Nombre, x.Tipo_Receta,x.Expr2 });
                return Content(JsonConvert.SerializeObject (obtenerInfo), "application/json");
            };
        }

        public ActionResult GetbyId(int id) {
            var objetos = JsonConvert.DeserializeObject<List<Recetas>>(json).Where(x=>x.PK_Receta==id).FirstOrDefault();
            return Content(JsonConvert.SerializeObject(objetos), "application/json");
        }

        public ActionResult Categorias()
        {
            return Content(categorias, "application/json");
        }

        public ActionResult Unidades()
        {
            return Content(unidades, "application/json");
        }
        [WebMethod]
        public bool Add(string nombre, string instrucciones, int id_tiporeceta)
        {
            using (DB_RecetasEntities entity = new DB_RecetasEntities())
            {
                var agregarReceta =
                    entity
                    .Recetas
                    .Add(new Receta
                    {
                        Nombre = nombre,
                        Instrucciones = instrucciones,
                        Nivel = 1,
                        Activo = true,
                        Fecha_Alta = DateTime.Now,
                        Imagen = null,
                        Video = null,
                        ID_Usuario = 1,
                        ID_TipoReceta = id_tiporeceta
                    });
                entity.SaveChanges();
            }
            return true;
        }


        public bool Update(int PK_Receta,string Receta, string Categoria, string Usuario, string Fecha, int Puntuacion)
        {
            return true;
        }


        public bool Delete(int PK_Receta)
        {
            return true;
        }


        private class Recetas {
            public int PK_Receta { get; set; }
            public string Receta { get; set; }
            public string Categoria { get; set; }
            public string Usuario { get; set; }
            public string Fecha { get; set; }
            public int Puntuacion { get; set; }
        }


    }
}