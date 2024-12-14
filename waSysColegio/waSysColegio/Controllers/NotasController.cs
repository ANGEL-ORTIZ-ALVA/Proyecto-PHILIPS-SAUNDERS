using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using waSysColegio;
using waSysColegio.Models;

namespace waSysColegio.Controllers
{
    public class NotasController : Controller
    {
        private ColegioBDv2Entities db = new ColegioBDv2Entities();

        // GET: Notas
        public ActionResult Index(int? idEstudiante, int? idCurso, int? idSeccion, int? idPeriodo)
        {
            var userId = Session["ID_Usuario"] as int?; // Obtener ID del usuario
            var userRole = Session["Rol"] != null ? (Roles_Enum)(int)Session["Rol"] : (Roles_Enum?)null; // Convertir a Roles_Enum

            // Verificar si los datos de sesión están disponibles
            if (!userId.HasValue || !userRole.HasValue)
            {
                Console.WriteLine("Error: No se pudo obtener el usuario o el rol de la sesión.");
                return Redirect("~/Login.aspx"); // Redirigir al login
            }

            Console.WriteLine($"Usuario ID: {userId}, Rol: {userRole}");

            var detalle_Curso = db.Detalle_Curso
                .Include(d => d.Curso)
                .Include(d => d.Estudiante)
                .Include(d => d.Estudiante.Seccion)
                .AsQueryable();

            // Filtrar por rol y preparar el listado de estudiantes
            List<SelectListItem> listaEstudiantes = new List<SelectListItem>();
            bool deshabilitarEstudiante = false;

            if (userRole == Roles_Enum.Estudiante)
            {
                var estudiante = db.Estudiante.FirstOrDefault(e => e.ID_Usuario == (int)userId);
                if (estudiante != null)
                {
                    detalle_Curso = detalle_Curso.Where(d => d.ID_Estudiante == estudiante.ID_Estudiante);

                    // Mostrar sólo al estudiante actual
                    listaEstudiantes.Add(new SelectListItem
                    {
                        Value = estudiante.ID_Estudiante.ToString(),
                        Text = $"{estudiante.Nombre} {estudiante.Apellido}"
                    });
                    deshabilitarEstudiante = true;
                }
            }
            else if (userRole == Roles_Enum.Apoderado)
            {
                var apoderado = db.Apoderado.FirstOrDefault(a => a.ID_Usuario == (int)userId);
                if (apoderado != null)
                {
                    var estudiantesIds = db.Apoderado_Estudiante
                                            .Where(ae => ae.ID_Apoderado == apoderado.ID_Apoderado)
                                            .Select(ae => ae.ID_Estudiante)
                                            .ToList();
                    detalle_Curso = detalle_Curso.Where(d => estudiantesIds.Contains(d.ID_Estudiante));

                    // Mostrar sólo estudiantes asociados al apoderado
                    listaEstudiantes = db.Estudiante
                        .Where(e => estudiantesIds.Contains(e.ID_Estudiante))
                        .AsEnumerable() // Aquí se pasa la consulta a memoria
                        .Select(e => new SelectListItem
                        {
                            Value = e.ID_Estudiante.ToString(),
                            Text = e.Nombre + " " + e.Apellido // Concatenación de cadenas en memoria
                        })
                        .ToList();

                    deshabilitarEstudiante = estudiantesIds.Count == 1; // Deshabilitar si hay un único estudiante
                }
            }
            else
            {
                // Rol administrador: Mostrar todos los estudiantes
                listaEstudiantes = db.Estudiante.Select(e => new SelectListItem
                {
                    Value = e.ID_Estudiante.ToString(),
                    Text = e.Nombre + " " + e.Apellido
                }).ToList();
            }

            // Aplicar filtros adicionales
            if (idEstudiante.HasValue)
            {
                detalle_Curso = detalle_Curso.Where(d => d.ID_Estudiante == idEstudiante.Value);
                Console.WriteLine($"Filtro Estudiante: {detalle_Curso.Count()} resultados");
            }

            if (idCurso.HasValue)
            {
                detalle_Curso = detalle_Curso.Where(d => d.ID_Curso == idCurso.Value);
                Console.WriteLine($"Filtro Curso: {detalle_Curso.Count()} resultados");
            }

            if (idSeccion.HasValue)
            {
                detalle_Curso = detalle_Curso.Where(d => d.Estudiante.ID_Seccion == idSeccion.Value);
                Console.WriteLine($"Filtro Sección: {detalle_Curso.Count()} resultados");
            }

            if (idPeriodo.HasValue)
            {
                detalle_Curso = detalle_Curso.Where(d => d.ID_Periodo == idPeriodo.Value);
                Console.WriteLine($"Filtro Periodo: {detalle_Curso.Count()} resultados");
            }

            // ViewBag para filtros en la vista
            ViewBag.idEstudiante = new SelectList(listaEstudiantes, "Value", "Text");
            ViewBag.DesactivarEstudiante = deshabilitarEstudiante;
            ViewBag.idCurso = new SelectList(db.Curso, "ID_Curso", "Nombre_Curso");
            ViewBag.idSeccion = new SelectList(db.Seccion, "ID_Seccion", "Nombre_Seccion");
            ViewBag.idPeriodo = new SelectList(db.Periodo, "ID_Periodo", "Nombre_Periodo");

            return View(detalle_Curso.ToList());
        }

        // GET: Notas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Detalle_Curso detalle_Curso = db.Detalle_Curso.Find(id);
            if (detalle_Curso == null)
            {
                return HttpNotFound();
            }
            return View(detalle_Curso);
        }

        // GET: Notas/Create
        public ActionResult Create()
        {
            ViewBag.ID_Curso = new SelectList(db.Curso, "ID_Curso", "Nombre_Curso");
            ViewBag.ID_Estudiante = new SelectList(db.Estudiante, "ID_Estudiante", "Nombre");
            ViewBag.ID_Periodo = new SelectList(db.Periodo, "ID_Periodo", "Nombre_Periodo");
            return View();
        }

        // POST: Notas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Estudiante,ID_Curso,ID_Periodo,Competencia1,Competencia2,Competencia3,Competencia4,Proyecto,ExamenFinal,Estado_Registro")] Detalle_Curso detalle_Curso)
        {
            if (ModelState.IsValid)
            {
                db.Detalle_Curso.Add(detalle_Curso);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_Curso = new SelectList(db.Curso, "ID_Curso", "Nombre_Curso", detalle_Curso.ID_Curso);
            ViewBag.ID_Estudiante = new SelectList(db.Estudiante, "ID_Estudiante", "Nombre", detalle_Curso.ID_Estudiante);
            ViewBag.ID_Periodo = new SelectList(db.Periodo, "ID_Periodo", "Nombre_Periodo", detalle_Curso.ID_Periodo);
            return View(detalle_Curso);
        }

        // GET: Notas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Detalle_Curso detalle_Curso = db.Detalle_Curso.Find(id);
            if (detalle_Curso == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Curso = new SelectList(db.Curso, "ID_Curso", "Nombre_Curso", detalle_Curso.ID_Curso);
            ViewBag.ID_Estudiante = new SelectList(db.Estudiante, "ID_Estudiante", "Nombre", detalle_Curso.ID_Estudiante);
            ViewBag.ID_Periodo = new SelectList(db.Periodo, "ID_Periodo", "Nombre_Periodo", detalle_Curso.ID_Periodo);
            return View(detalle_Curso);
        }

        // POST: Notas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Estudiante,ID_Curso,ID_Periodo,Competencia1,Competencia2,Competencia3,Competencia4,Proyecto,ExamenFinal,Estado_Registro")] Detalle_Curso detalle_Curso)
        {
            if (ModelState.IsValid)
            {
                db.Entry(detalle_Curso).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_Curso = new SelectList(db.Curso, "ID_Curso", "Nombre_Curso", detalle_Curso.ID_Curso);
            ViewBag.ID_Estudiante = new SelectList(db.Estudiante, "ID_Estudiante", "Nombre", detalle_Curso.ID_Estudiante);
            ViewBag.ID_Periodo = new SelectList(db.Periodo, "ID_Periodo", "Nombre_Periodo", detalle_Curso.ID_Periodo);
            return View(detalle_Curso);
        }

        // GET: Notas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Detalle_Curso detalle_Curso = db.Detalle_Curso.Find(id);
            if (detalle_Curso == null)
            {
                return HttpNotFound();
            }
            return View(detalle_Curso);
        }

        // POST: Notas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Detalle_Curso detalle_Curso = db.Detalle_Curso.Find(id);
            db.Detalle_Curso.Remove(detalle_Curso);
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
    }
}
