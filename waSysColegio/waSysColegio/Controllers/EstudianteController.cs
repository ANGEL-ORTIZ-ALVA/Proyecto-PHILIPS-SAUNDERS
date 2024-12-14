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
using waSysColegio.Validations;

namespace waSysColegio.Controllers
{
    public class EstudianteController : Controller
    {
        private ColegioBDv2Entities db = new ColegioBDv2Entities();

        // GET: Estudiante
        public ActionResult Index()
        {
            var estudiante = db.Estudiante.Include(e => e.Genero).Include(e => e.Grado).Include(e => e.Seccion).Include(e => e.Usuario);
            return View(estudiante.ToList());
        }

        // GET: Estudiante/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estudiante estudiante = db.Estudiante.Find(id);
            if (estudiante == null)
            {
                return HttpNotFound();
            }
            return View(estudiante);
        }

        // GET: Estudiante/Create
        public ActionResult Create()
        {
            ViewBag.ID_Genero = new SelectList(db.Genero, "ID_Genero", "Nombre_Genero");
            ViewBag.ID_Grado = new SelectList(db.Grado, "ID_Grado", "Numero_Grado");
            ViewBag.ID_Seccion = new SelectList(db.Seccion, "ID_Seccion", "Nombre_Seccion");
            ViewBag.ID_Usuario = new SelectList(db.Usuario, "ID_Usuario", "Nombre_Usuario");
            return View();
        }

        // POST: Estudiante/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Estudiante,Nombre,Apellido,Fecha_Nacimiento,DNI,Direccion,ID_Genero,ID_Grado,ID_Seccion,ID_Usuario")] Estudiante estudiante)
        {
            if (ModelState.IsValid)
            {

                // Asigna el valor predeterminado
                estudiante.Estado_Registro = "Registrado";

                db.Estudiante.Add(estudiante);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_Genero = new SelectList(db.Genero, "ID_Genero", "Nombre_Genero", estudiante.ID_Genero);
            ViewBag.ID_Grado = new SelectList(db.Grado, "ID_Grado", "Numero_Grado", estudiante.ID_Grado);
            ViewBag.ID_Seccion = new SelectList(db.Seccion, "ID_Seccion", "Nombre_Seccion", estudiante.ID_Seccion);
            ViewBag.ID_Usuario = new SelectList(db.Usuario, "ID_Usuario", "Nombre_Usuario", estudiante.ID_Usuario);
            return View(estudiante);
        }

        //endpoint

        /*  public JsonResult VerificarDniUnico(string dni)
          {
                  bool existe = db.Estudiante.Any(s => s.DNI == dni);
                  return Json(!existe, JsonRequestBehavior.AllowGet); // Retorna true si es único
          }*/

        [HttpGet]
        public JsonResult EsDniUnico(string DNI)
        {
            bool esUnico = !db.Estudiante.Any(e => e.DNI == DNI);
            return Json(esUnico, JsonRequestBehavior.AllowGet);
        }

        // GET: Estudiante/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estudiante estudiante = db.Estudiante.Find(id);
            if (estudiante == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Genero = new SelectList(db.Genero, "ID_Genero", "Nombre_Genero", estudiante.ID_Genero);
            ViewBag.ID_Grado = new SelectList(db.Grado, "ID_Grado", "Numero_Grado", estudiante.ID_Grado);
            ViewBag.ID_Seccion = new SelectList(db.Seccion, "ID_Seccion", "Nombre_Seccion", estudiante.ID_Seccion);
            ViewBag.ID_Usuario = new SelectList(db.Usuario, "ID_Usuario", "Nombre_Usuario", estudiante.ID_Usuario);
            return View(estudiante);
        }

        // POST: Estudiante/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Estudiante,Nombre,Apellido,Fecha_Nacimiento,DNI,Direccion,Estado_Registro,ID_Genero,ID_Grado,ID_Seccion,ID_Usuario")] Estudiante estudiante)
        {
            if (ModelState.IsValid)
            {
                db.Entry(estudiante).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_Genero = new SelectList(db.Genero, "ID_Genero", "Nombre_Genero", estudiante.ID_Genero);
            ViewBag.ID_Grado = new SelectList(db.Grado, "ID_Grado", "Numero_Grado", estudiante.ID_Grado);
            ViewBag.ID_Seccion = new SelectList(db.Seccion, "ID_Seccion", "Nombre_Seccion", estudiante.ID_Seccion);
            ViewBag.ID_Usuario = new SelectList(db.Usuario, "ID_Usuario", "Nombre_Usuario", estudiante.ID_Usuario);
            return View(estudiante);
        }

        // GET: Estudiante/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estudiante estudiante = db.Estudiante.Find(id);
            if (estudiante == null)
            {
                return HttpNotFound();
            }
            return View(estudiante);
        }

        // POST: Estudiante/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Estudiante estudiante = db.Estudiante.Find(id);
            db.Estudiante.Remove(estudiante);
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
