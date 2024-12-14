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
    public class CursoController : Controller
    {
        private ColegioBDv2Entities db = new ColegioBDv2Entities();

        // GET: Curso
        public ActionResult Index()
        {
            var curso = db.Curso.Include(c => c.Personal);
            return View(curso.ToList());
        }

        // GET: Curso/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Curso curso = db.Curso.Find(id);
            if (curso == null)
            {
                return HttpNotFound();
            }
            return View(curso);
        }

        // GET: Curso/Create
        public ActionResult Create()
        {
            var personalList = db.Personal.Select(p => new 
            {
                ID_Personal = p.ID_Personal, 
                NombreCompleto = p.Nombre + " " + p.Apellido 
            }).ToList(); 
            ViewBag.ID_Personal = new SelectList(personalList, "ID_Personal", "NombreCompleto"); 
            return View();
        }

        // POST: Curso/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Curso,Nombre_Curso,Descripcion,ID_Personal")] Curso curso)
        {
            if (ModelState.IsValid) {

                // Asigna el valor predeterminado
                curso.Estado_Registro = "Registrado";

                db.Curso.Add(curso);
                db.SaveChanges(); 
                return RedirectToAction("Index"); 
            } 
            var personalList = db.Personal.Select(p => new 
            { 
                ID_Personal = p.ID_Personal, 
                NombreCompleto = p.Nombre + " " + p.Apellido 
            }).ToList(); 
            ViewBag.ID_Personal = new SelectList(personalList, "ID_Personal", "NombreCompleto", curso.ID_Personal);
            return View(curso);
        }

        // GET: Curso/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Curso curso = db.Curso.Find(id);
            if (curso == null)
            {
                return HttpNotFound();
            }

            // Concatenar Nombre y Apellido en el SelectList
            ViewBag.ID_Personal = new SelectList(
                db.Personal.Select(p => new
                {
                    ID_Personal = p.ID_Personal,
                    NombreCompleto = p.Nombre + " " + p.Apellido
                }),
                "ID_Personal",
                "NombreCompleto",
                curso.ID_Personal
            );

            return View(curso);
        }

        // POST: Curso/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Curso,Nombre_Curso,Descripcion,Estado_Registro,ID_Personal")] Curso curso)
        {
            if (ModelState.IsValid)
            {
                db.Entry(curso).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            // Concatenar Nombre y Apellido en el SelectList (para evitar errores si el formulario falla la validación)
            ViewBag.ID_Personal = new SelectList(
                db.Personal.Select(p => new
                {
                    ID_Personal = p.ID_Personal,
                    NombreCompleto = p.Nombre + " " + p.Apellido
                }),
                "ID_Personal",
                "NombreCompleto",
                curso.ID_Personal
            );

            return View(curso);
        }

        // GET: Curso/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Curso curso = db.Curso.Find(id);
            if (curso == null)
            {
                return HttpNotFound();
            }
            return View(curso);
        }

        // POST: Curso/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Curso curso = db.Curso.Find(id);
            db.Curso.Remove(curso);
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
