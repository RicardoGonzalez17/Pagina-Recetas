using Newtonsoft.Json;
using PaginaRecetas.App_Start;
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
        UsuarioController controladorUsuarios;
        //BD_PaginaRecetasEntities2 dbDeRecetas = new BD_PaginaRecetasEntities2();
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
        //private static string unidades = "[{\"PK_Unidad\":1, \"Unidad\":\"Gramos\"}, {\"PK_Unidad\":2, \"Unidad\":\"Kilogramos\"}, {\"PK_Unidad\":3, \"Unidad\":\"Cucharada\"}, {\"PK_Unidad\":4, \"Unidad\":\"Pieza\"}, {\"PK_Unidad\":5, \"Unidad\":\"Pisca\"} ]";
        private static string json = "[{\"ID_Receta\": 1, \"Nombre\": \"Enchiladas\", \"Categoria\":\"Mexicana\", \"Usuario\":\"Aarón Teposte\", \"Fecha_Alta\":\"2018-03-16 12:07:00\", \"Puntuacion\":3 },{\"ID_Receta\": 2, \"Nombre\": \"Pizza\", \"Categoria\":\"Italiana\", \"Usuario\":\"Ricardo Gonzalez\", \"Fecha_Alta\":\"2018-03-16 12:07:00\", \"Puntuacion\":5 },{\"ID_Receta\": 3, \"Nombre\": \"Hamburguesa\", \"Categoria\":\"Comida rápida\", \"Usuario\":\"Pedro López\", \"Fecha_Alta\":\"2018-03-16 12:07:00\", \"Puntuacion\":4 },{\"ID_Receta\": 4, \"Nombre\": \"Maruchan\", \"Categoria\":\"Comida rápida\", \"Usuario\":\"Lupita Huevona\", \"Fecha_Alta\":\"2018-03-16 12:07:00\", \"Puntuacion\":1 },{\"ID_Receta\": 5, \"Nombre\": \"Pollo en crema de chipotle\", \"Categoria\":\"Mexicana\", \"Usuario\":\"Francisco Márquez\", \"Fecha_Alta\":\"2018-03-16 12:07:00\", \"Puntuacion\":0 },{\"ID_Receta\": 6, \"Nombre\": \"Quesadillas\", \"Categoria\":\"Mexicana\", \"Usuario\":\"Ricardo Gonzalez\", \"Fecha_Alta\":\"2018-03-16 12:07:00\", \"Puntuacion\":5 } ]";
        public ActionResult Get() {
            using (BD_PaginaRecetasEntities2 entity = new BD_PaginaRecetasEntities2(ConnectionString.Value))
            {
                var obtenerInfo = entity
                    .RECETAS
                    .Select(x => new {x.Nombre,x.Fecha_Alta, x.Puntuacion, x.ID_Receta, x.ID_Usuario,x.Likes,x.Dislikes});
                return Content(JsonConvert.SerializeObject (obtenerInfo), "application/json");
            };

        }
        public ActionResult GetByIdUsuario(int idusuario)
        {
            using (BD_PaginaRecetasEntities2 entity = new BD_PaginaRecetasEntities2(ConnectionString.Value))
            {
                var recetas = entity
                    .RECETAS
                    .Where(x => x.ID_Usuario == idusuario)
                    .Select(x => new { x.Nombre, x.Fecha_Alta, x.Likes, x.Dislikes });
                return Content(JsonConvert.SerializeObject(recetas), "application/json");
            }
        }

        //public int GetIdRecetabyNombreReceta(string recetaNombre)
        //{
        //    using (BD_PaginaRecetasEntities2 entity = new BD_PaginaRecetasEntities2())
        //    {
        //        var obtenerInfo = entity
        //            .RECETAS
        //            .Where(x => x.Nombre == recetaNombre)
        //            .Select(x => x.ID_Receta)
        //            .FirstOrDefault();

        //        return (obtenerInfo);
        //    }
            //var objetos = JsonConvert.DeserializeObject<List<RECETAS>>(json).Where(x=>x.ID_Receta==id).FirstOrDefault();
            //return Content(JsonConvert.SerializeObject(objetos), "application/json");
        //}

        public ActionResult Categorias()
        {
            using (BD_PaginaRecetasEntities2 entity = new BD_PaginaRecetasEntities2(ConnectionString.Value))
            {
                var obtenerInfo = entity
                    .CATEGORIAS
                    .Select(x=> new { x.Nombre, x.ID_Categoria });
                //return obtenerInfo.ToString();
                return Content(JsonConvert.SerializeObject(obtenerInfo), "application/json");
            }
        }
        public ActionResult SumarORestar(string operacion, int idreceta)
        {
            using (BD_PaginaRecetasEntities2 entity = new BD_PaginaRecetasEntities2(ConnectionString.Value)) {
                if (operacion.Equals("Sumar"))
                {
                    var sumarORestar = entity
                        .RECETAS
                        .Where(x => x.ID_Receta == idreceta)
                        .Select(x => x)
                        .FirstOrDefault();
                    sumarORestar.Likes = sumarORestar.Likes + 1;
                    entity.SaveChanges();
                    return Content (JsonConvert.SerializeObject (sumarORestar.Likes + sumarORestar.Dislikes), "application/json");
                }
                else {
                    {
                        var sumarORestar = entity
                            .RECETAS
                            .Where(x => x.ID_Receta == idreceta)
                            .Select(x => x)
                            .FirstOrDefault();
                        sumarORestar.Dislikes = sumarORestar.Dislikes + 1;
                        entity.SaveChanges();
                        return Content(JsonConvert.SerializeObject(sumarORestar.Likes + sumarORestar.Dislikes), "application/json");
                    }
                }
            }
        }
        public ActionResult GetbyId(int id)
        {
            using (BD_PaginaRecetasEntities2 entity = new BD_PaginaRecetasEntities2(ConnectionString.Value))
            {
                var obtenerInfo = entity
                    .RECETAS
                    .Where(x => x.ID_Receta == id)
                    .Select (x => new { x.Nombre, x.Fecha_Alta, x.Puntuacion, x.ID_Receta, x.Descripcion, x.ID_Categoria, x.ID_Usuario, x.Likes, x.Dislikes})
                    .FirstOrDefault();
                return Content(JsonConvert.SerializeObject(obtenerInfo), "application/json");
            }
        }
        public ActionResult GetUsuariobyID(int idusuario)
        {
            using (BD_PaginaRecetasEntities2 entity = new BD_PaginaRecetasEntities2(ConnectionString.Value))
            {
                var obtenerInfo = entity
                    .USUARIOS
                    .Where(x => x.ID_Usuario == idusuario)
                    .Select(x => new { x.Nombre })
                    .FirstOrDefault();
                return Content(JsonConvert.SerializeObject(obtenerInfo), "application/json");
            };
        }
        public ActionResult CategoriasbyID(int idcategoria)
        {
            using (BD_PaginaRecetasEntities2 entity = new BD_PaginaRecetasEntities2(ConnectionString.Value))
            {
                var obtenerInfo = entity
                    .CATEGORIAS
                    .Where(x => x.ID_Categoria == idcategoria)
                    .Select(x => x.Nombre)
                    .FirstOrDefault();
                return Content(obtenerInfo.ToString());
            }
        }

        public ActionResult CategoriasGetID(string categoria)
        {
            using (BD_PaginaRecetasEntities2 entity = new BD_PaginaRecetasEntities2(ConnectionString.Value))
            {
                var obtenerInfo = entity
                    .CATEGORIAS
                    .Where(x => x.Nombre == categoria)
                    .FirstOrDefault();
                return Content (obtenerInfo.ToString());
            }
        }

        [SessionTimeOut]
        public bool AddIngrediente(int id_receta, string ingrediente)
        {
            using (BD_PaginaRecetasEntities2 entity = new BD_PaginaRecetasEntities2(ConnectionString.Value))
            {
                var obtenerInfor = entity
                    .INGREDIENTES
                    .Add(new INGREDIENTES
                    {
                        ID_Receta = id_receta,
                        Ingrediente = ingrediente
                    });
                entity.SaveChanges();
                return true;
            }
        }
        
        [WebMethod]
        [SessionTimeOut]
        public int Add(string nombre, string descripcion, int id_categoria, string ingredientes, string imagen, string video)
        {
            using (BD_PaginaRecetasEntities2 entity = new BD_PaginaRecetasEntities2(ConnectionString.Value))
            {
                var fecha = DateTime.Now;
                fecha.ToString("dd/MM/yyyy  hh:mm");
                var agregarReceta =
                    entity
                    .RECETAS
                    .Add(new RECETAS
                    {
                        Nombre = nombre,
                        Descripcion = descripcion,
                        Fecha_Alta = fecha,
                        Puntuacion = 0,
                        ID_Usuario = int.Parse(Session["ID_Usuario"].ToString()),
                        ID_Categoria = id_categoria,
                        Likes = 0,
                        Dislikes = 0
                    });
                entity.SaveChanges();
                var listaIngredientes = JsonConvert.DeserializeObject<List<Ingredientes>>(ingredientes);
                listaIngredientes.ForEach(ingre => { AddIngrediente(agregarReceta.ID_Receta, ingre.ingrediente); });
                if (video!=null || imagen!=null)
                {
                    InsertarMultimedia(imagen, video, agregarReceta.ID_Receta);
                }
                SumarCantidadReceta(int.Parse(Session["ID_Usuario"].ToString()));
               return agregarReceta.ID_Receta;
            }
        }
        public bool SumarCantidadReceta (int idusuario)
        {
            using (BD_PaginaRecetasEntities2 entity = new BD_PaginaRecetasEntities2(ConnectionString.Value))
            {
                var sumar = entity
                    .USUARIOS
                    .Where(x => x.ID_Usuario == idusuario)
                    .Select(x => x)
                    .FirstOrDefault();
                if (sumar.NumeroRecetas % 3 == 0 )
                {
                    sumar.Nivel = sumar.Nivel + 1;
                }
                sumar.NumeroRecetas = sumar.NumeroRecetas + 1;
                entity.SaveChanges();
                return true;
            }
        }
        public void InsertarMultimedia(string imagen, string video, int idreceta)
        {
            using (BD_PaginaRecetasEntities2 entity = new BD_PaginaRecetasEntities2(ConnectionString.Value))
            {
                if (imagen !=null && video != null)
                {
                    var Imagen = entity
                        .MULTIMEDIA
                        .Add(new MULTIMEDIA
                        {
                            ID_Receta = idreceta,
                            Liga = imagen,
                            Imagen = true
                        });
                    entity.SaveChanges();
                    var Video = entity
                      .MULTIMEDIA
                      .Add(new MULTIMEDIA
                      {
                          ID_Receta = idreceta,
                          Liga = video,
                          Video = true
                      });
                    entity.SaveChanges();
                }
                else { 
                if (imagen != null)
                {
                    var Imagen = entity
                        .MULTIMEDIA
                        .Add(new MULTIMEDIA
                        {
                            ID_Receta = idreceta,
                            Liga = imagen,
                            Imagen = true
                        });
                        entity.SaveChanges();
                    }
                else {
                    if (video != null)
                    {
                        var Video = entity
                            .MULTIMEDIA
                            .Add(new MULTIMEDIA
                            {
                                ID_Receta = idreceta,
                                Liga = video,
                                Video = true
                            });
                            entity.SaveChanges();
                        }
                    }
                }
            }
        }
        public ActionResult traerImagenes(int idreceta)
        {
            using (BD_PaginaRecetasEntities2 entity = new BD_PaginaRecetasEntities2(ConnectionString.Value))
            {
                var todaslasImagenes = entity
                    .MULTIMEDIA
                    .Where(x => x.ID_Receta == idreceta && x.Imagen == true)
                    .Select(x => x.Liga);

                return Content (JsonConvert.SerializeObject(todaslasImagenes),"application/json");
            }
        }
        public String traerVideo(int idreceta)
        {
            using (BD_PaginaRecetasEntities2 entity = new BD_PaginaRecetasEntities2(ConnectionString.Value))
            {
                try
                {
                    var video = entity
                        .MULTIMEDIA
                        .Where(x => x.ID_Receta == idreceta && x.Video == true)
                        .Select(x => new { x.Liga })
                        .FirstOrDefault();

                    return video.Liga.ToString();
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
        public ActionResult IngredientesGetbyIDReceta(int idreceta)
        {
            using (BD_PaginaRecetasEntities2 entity = new BD_PaginaRecetasEntities2(ConnectionString.Value))
            {
                var obtenerInfo = entity
                    .INGREDIENTES
                    .Where(x => x.ID_Receta == idreceta)
                    .Select(x => x.Ingrediente);
                return Content(JsonConvert.SerializeObject(obtenerInfo),"application/json");
            }
        }
        public ActionResult IngredientesGetByID(int id_ingrediente)
        {
            using (BD_PaginaRecetasEntities2 entity = new BD_PaginaRecetasEntities2(ConnectionString.Value))
            {
                var obtenerInfo = entity
                    .INGREDIENTES
                    .Where(x => x.ID_Ingrediente == id_ingrediente)
                    .Select(x => x.Ingrediente).FirstOrDefault();

                return Content(obtenerInfo.ToString()); 
            }
        }

        public bool Update(int PK_Receta,string Receta, string Categoria, string Usuario, string Fecha, int Puntuacion) //FALTA PONER VALORES DE LA BD NUEVA
        {
            return true;
        }

        public ActionResult GetComentariosByIdReceta(int idreceta)
        {
            using (BD_PaginaRecetasEntities2 entity = new BD_PaginaRecetasEntities2(ConnectionString.Value))
            {
                var comentarios = entity
                .COMENTARIOS
                .Where(x => x.ID_Receta == idreceta)
                .Select(x => new { x.ID_Usuario, x.Fecha, x.Comentario });
                return Content(JsonConvert.SerializeObject(comentarios), "application/json");
            };
        }
        public bool GuardarComentario(int idreceta, string idusuario, string comentario)
        {
            var hoy = DateTime.Now;
            using (BD_PaginaRecetasEntities2 entity = new BD_PaginaRecetasEntities2(ConnectionString.Value))
            {
                var comentarioNuevo = entity
                    .COMENTARIOS
                    .Add(new COMENTARIOS
                    {
                        ID_Receta = idreceta,
                        ID_Usuario = int.Parse(Session["ID_Usuario"].ToString()),
                        Comentario = comentario,
                        Fecha = hoy
                    });
                entity.SaveChanges();
                return true;
            }
        }


        public bool Delete(int PK_Receta)
        {
            return true;
        }


        public class Ingredientes {
            public string ingrediente { get; set; }
        }
        private class Recetas {
            public int ID_Receta { get; set; }
            public string Nombre { get; set; }
            public int ID_Categoria { get; set; }
            public string Usuario { get; set; }
            public DateTime Fecha_Alta { get; set; }
            public int Puntuacion { get; set; }
            public string Descripcion { get; set; }
            public int ID_Usuario { get; set; }

        }


    }

}