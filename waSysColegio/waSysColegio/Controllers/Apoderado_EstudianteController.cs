using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using waSysColegio;

namespace waSysColegio.Controllers
{
    public class Apoderado_EstudianteController : Controller
    {
        private ColegioBDv2Entities db = new ColegioBDv2Entities();

        // GET: Apoderado_Estudiante
        public ActionResult Index()
        {
            var apoderado_Estudiante = db.Apoderado_Estudiante.Include(a => a.Apoderado).Include(a => a.Estudiante);
            return View(apoderado_Estudiante.ToList());
        }

        // GET: Apoderado_Estudiante/Details/5
        public ActionResult Details(int? ID_Apoderado, int? ID_Estudiante)
        {
            if (ID_Apoderado == null || ID_Estudiante == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Apoderado_Estudiante apoderado_Estudiante = db.Apoderado_Estudiante.Find(ID_Apoderado, ID_Estudiante);
            if (apoderado_Estudiante == null)
            {
                return HttpNotFound();
            }
            return View(apoderado_Estudiante);
        }

        // GET: Apoderado_Estudiante/Create
        public ActionResult Create()
        {
            ViewBag.ID_Apoderado = new SelectList(db.Apoderado.Select(a => new
            {
                ID_Apoderado = a.ID_Apoderado,
                NombreCompleto = a.Nombre + " " + a.Apellido
            }), "ID_Apoderado", "NombreCompleto");

            ViewBag.ID_Estudiante = new SelectList(db.Estudiante.Select(e => new
            {
                ID_Estudiante = e.ID_Estudiante,
                NombreCompleto = e.Nombre + " " + e.Apellido
            }), "ID_Estudiante", "NombreCompleto");

            return View();
        }

        // POST: Apoderado_Estudiante/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Apoderado,ID_Estudiante,Parentesco")] Apoderado_Estudiante apoderado_Estudiante)
        {
            if (ModelState.IsValid)
            {

                // Asigna el valor predeterminado
                apoderado_Estudiante.Estado_Registro = "Registrado";

                db.Apoderado_Estudiante.Add(apoderado_Estudiante);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            // Repopulate the SelectLists in case of an error
            ViewBag.ID_Apoderado = new SelectList(db.Apoderado.Select(a => new
            {
                ID_Apoderado = a.ID_Apoderado,
                NombreCompleto = a.Nombre + " " + a.Apellido
            }), "ID_Apoderado", "NombreCompleto", apoderado_Estudiante.ID_Apoderado);

            ViewBag.ID_Estudiante = new SelectList(db.Estudiante.Select(e => new
            {
                ID_Estudiante = e.ID_Estudiante,
                NombreCompleto = e.Nombre + " " + e.Apellido
            }), "ID_Estudiante", "NombreCompleto", apoderado_Estudiante.ID_Estudiante);

            return View(apoderado_Estudiante);
        }

        // GET: Apoderado_Estudiante/Edit/5
        public ActionResult Edit(int? ID_Apoderado, int? ID_Estudiante)
        {
            if (ID_Apoderado == null || ID_Estudiante == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Apoderado_Estudiante apoderado_Estudiante = db.Apoderado_Estudiante.Find(ID_Apoderado, ID_Estudiante);
            if (apoderado_Estudiante == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Apoderado = new SelectList(db.Apoderado, "ID_Apoderado", "Nombre", apoderado_Estudiante.ID_Apoderado);
            ViewBag.ID_Estudiante = new SelectList(db.Estudiante, "ID_Estudiante", "Nombre", apoderado_Estudiante.ID_Estudiante);
            return View(apoderado_Estudiante);
        }

        // POST: Apoderado_Estudiante/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Apoderado,ID_Estudiante,Parentesco,Estado_Registro")] Apoderado_Estudiante apoderado_Estudiante)
        {
            if (ModelState.IsValid)
            {
                db.Entry(apoderado_Estudiante).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_Apoderado = new SelectList(db.Apoderado, "ID_Apoderado", "Nombre", apoderado_Estudiante.ID_Apoderado);
            ViewBag.ID_Estudiante = new SelectList(db.Estudiante, "ID_Estudiante", "Nombre", apoderado_Estudiante.ID_Estudiante);
            return View(apoderado_Estudiante);
        }

        // GET: Apoderado_Estudiante/Delete/5
        public ActionResult Delete(int? ID_Apoderado, int? ID_Estudiante)
        {
            if (ID_Apoderado == null || ID_Estudiante == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Apoderado_Estudiante apoderado_Estudiante = db.Apoderado_Estudiante.Find(ID_Apoderado, ID_Estudiante);
            if (apoderado_Estudiante == null)
            {
                return HttpNotFound();
            }
            return View(apoderado_Estudiante);
        }

        // POST: Apoderado_Estudiante/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int ID_Apoderado, int ID_Estudiante)
        {
            Apoderado_Estudiante apoderado_Estudiante = db.Apoderado_Estudiante.Find(ID_Apoderado, ID_Estudiante);
            db.Apoderado_Estudiante.Remove(apoderado_Estudiante);
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
