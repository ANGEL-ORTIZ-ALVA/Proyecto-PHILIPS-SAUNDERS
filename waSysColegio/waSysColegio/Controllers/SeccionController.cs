using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using waSysColegio;
using waSysColegio.Dao;
using waSysColegio.Models;

namespace waSysColegio.Controllers
{
    public class SeccionController : Controller
    {
        private ColegioBDv2Entities db = new ColegioBDv2Entities();

        // GET: Seccion
        public ActionResult Index()
        {
            return View(db.Seccion.ToList());
        }

        // GET: Seccion/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seccion seccion = db.Seccion.Find(id);
            if (seccion == null)
            {
                return HttpNotFound();
            }
            return View(seccion);
        }

        // GET: Seccion/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Seccion/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Seccion,Nombre_Seccion,Aforo")] Seccion seccion)
        {
            if (ModelState.IsValid)
            {

                // Asigna el valor predeterminado
                seccion.Estado_Registro = "Registrado";

                db.Seccion.Add(seccion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(seccion);
        }

        // GET: Seccion/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seccion seccion = db.Seccion.Find(id);
            if (seccion == null)
            {
                return HttpNotFound();
            }
            return View(seccion);
        }

        // POST: Seccion/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Seccion,Nombre_Seccion,Aforo,Estado_Registro")] Seccion seccion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(seccion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(seccion);
        }

        // GET: Seccion/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seccion seccion = db.Seccion.Find(id);
            if (seccion == null)
            {
                return HttpNotFound();
            }
            return View(seccion);
        }

        // POST: Seccion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Seccion seccion = db.Seccion.Find(id);
            db.Seccion.Remove(seccion);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //metodos propios
        public ActionResult VerSecciones()
        {
            SeccionDAO seccionDao = new SeccionDAO();
            var secciones = seccionDao.ObtenerTodasLasSecciones();

            // Ordenar por número y luego por letra
            var seccionesOrdenadas = secciones
                .OrderBy(s => s.Nombre_Seccion.Substring(0, s.Nombre_Seccion.Length - 1)) // Ordena por el número (e.g., "1" en "1A")
                .ThenBy(s => s.Nombre_Seccion.Substring(s.Nombre_Seccion.Length - 1)) // Ordena por la letra (e.g., "A" en "1A")
                .ToList();

            return View(seccionesOrdenadas);

            //return View(secciones);
        }

        [HttpGet]
        public ActionResult VerEstudiantes(int idSeccion, int? idPeriodo, int? idCurso)
        {
            var daoCurso = new CursoDAO();
            var daoPeriodo = new PeriodoDAO();
            var detalleCursoDao = new DetalleCursoDAO();

            ViewBag.Cursos = daoCurso.ObtenerCursosActivos();
            ViewBag.Periodos = daoPeriodo.ObtenerPeriodosActivos();
            ViewBag.SelectedPeriodoId = idPeriodo ?? 0;
            ViewBag.SelectedCursoId = idCurso ?? 0;
            ViewBag.SeccionId = idSeccion;

            // Obtiene los estudiantes de la sección especificada
            var estudiantes = detalleCursoDao.ObtenerEstudiantesPorSeccion(idSeccion);

            // Crea una lista de DetalleCurso en base a los estudiantes obtenidos
            var detallesCurso = estudiantes.Select(estudiante => new DetalleCurso
            {
                ID_Estudiante = estudiante.ID_Estudiante,
                NombreEstudiante = estudiante.Nombre,
                ApellidoEstudiante = estudiante.Apellido,
                ID_Curso = idCurso ?? 0, // Asigna el ID_Curso seleccionado
                ID_Periodo = idPeriodo ?? 0 // Asigna el ID_Periodo seleccionado
                                            // Agrega otros campos según sea necesario
            }).ToList();

            // Pasa la lista de DetalleCurso a la vista
            return View(detallesCurso);
        }

        [HttpPost]
        public ActionResult GuardarNotas(List<Models.DetalleCurso> detalles, int ID_Periodo, int ID_Curso)
        {
            if (detalles == null || !detalles.Any())
            {
                ModelState.AddModelError("", "No se recibieron datos para guardar.");
                return RedirectToAction("VerEstudiantes", new { idSeccion = 0 });
            }

            // Asigna el periodo y curso seleccionados a cada registro de DetalleCurso
            foreach (var detalle in detalles)
            {
                detalle.ID_Periodo = ID_Periodo;
                detalle.ID_Curso = ID_Curso;
            }

            DetalleCursoDAO detalleCursoDao = new DetalleCursoDAO();
            detalleCursoDao.GuardarNotas(detalles);

            int idEstudiante = detalles.First().ID_Estudiante;
            int idSeccion = new EstudianteDAO().ObtenerSeccionPorEstudiante(idEstudiante);

            TempData["SuccessMessage"] = "Las notas se han registrado correctamente.";

            return RedirectToAction("VerEstudiantes", new { idSeccion = idSeccion, idPeriodo = ID_Periodo, idCurso = ID_Curso });
        }

        public ActionResult CargarNotas(int idSeccion, int idPeriodo, int idCurso)
        {
            var daoCurso = new CursoDAO();
            var daoPeriodo = new PeriodoDAO();
            var detalleCursoDao = new DetalleCursoDAO();

            ViewBag.Cursos = daoCurso.ObtenerCursosActivos();
            ViewBag.Periodos = daoPeriodo.ObtenerPeriodosActivos();
            ViewBag.SelectedPeriodoId = idPeriodo;
            ViewBag.SelectedCursoId = idCurso;

            ViewBag.SeccionId = idSeccion; // Guardar idSeccion en ViewBag

            // Obtiene las notas o inicializa con valores en 0 si no existen
            var estudiantesConNotas = detalleCursoDao.ObtenerEstudiantesConNotas(idSeccion, idCurso, idPeriodo);

            // Obtenemos los nombres de los estudiantes
            //var estudianteDao = new EstudianteDao();
            var estudiantes = detalleCursoDao.ObtenerEstudiantesPorSeccion(idSeccion);

            // Combinar los datos de estudiante con sus notas
            var resultado = from e in estudiantes
                            join dc in estudiantesConNotas on e.ID_Estudiante equals dc.ID_Estudiante into gj
                            from dc in gj.DefaultIfEmpty(new DetalleCurso { Competencia1 = 0, Competencia2 = 0, Competencia3 = 0, Competencia4 = 0, Proyecto = 0, ExamenFinal = 0, Estado_Registro = "Registrado" })
                            select new DetalleCurso
                            {
                                ID_Estudiante = e.ID_Estudiante,
                                NombreEstudiante = e.Nombre, // Ya está en DetalleCurso
                                ApellidoEstudiante = e.Apellido, // Ya está en DetalleCurso
                                ID_Curso = idCurso,
                                ID_Periodo = idPeriodo,
                                Competencia1 = dc.Competencia1,
                                Competencia2 = dc.Competencia2,
                                Competencia3 = dc.Competencia3,
                                Competencia4 = dc.Competencia4,
                                Proyecto = dc.Proyecto,
                                ExamenFinal = dc.ExamenFinal,
                                Estado_Registro = dc.Estado_Registro
                            };


            // Mensaje de éxito para la carga de notas
            TempData["SuccessMessage"] = "Las notas se han cargado correctamente.";

            return View("VerEstudiantes", resultado);
        }
    }
}
