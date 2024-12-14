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
    public class PersonalController : Controller
    {
        private ColegioBDv2Entities db = new ColegioBDv2Entities();

        // GET: Personal
        public ActionResult Index()
        {
            var personal = db.Personal.Include(p => p.Genero).Include(p => p.Grado).Include(p => p.Seccion).Include(p => p.Tipo_Personal).Include(p => p.Usuario);
            return View(personal.ToList());
        }

        // GET: Personal/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personal personal = db.Personal.Find(id);
            if (personal == null)
            {
                return HttpNotFound();
            }
            return View(personal);
        }

        // GET: Personal/Create
        public ActionResult Create()
        {
            ViewBag.ID_Genero = new SelectList(db.Genero, "ID_Genero", "Nombre_Genero");
            ViewBag.ID_Grado = new SelectList(db.Grado, "ID_Grado", "Numero_Grado");
            ViewBag.ID_Seccion = new SelectList(db.Seccion, "ID_Seccion", "Nombre_Seccion");
            ViewBag.ID_Tipo_Personal = new SelectList(db.Tipo_Personal, "ID_Tipo_Personal", "Nombre_Tipo_Personal");
            ViewBag.ID_Usuario = new SelectList(db.Usuario, "ID_Usuario", "Nombre_Usuario");
            return View();
        }

        // POST: Personal/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Personal,Nombre,Apellido,Fecha_Nacimiento,DNI,Correo,Telefono,Direccion,Firma,Sello,ID_Tipo_Personal,ID_Genero,ID_Grado,ID_Seccion,ID_Usuario")] Personal personal)
        {
            if (ModelState.IsValid)
            {

                // Asigna el valor predeterminado
                personal.Estado_Registro = "Registrado";

                db.Personal.Add(personal);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_Genero = new SelectList(db.Genero, "ID_Genero", "Nombre_Genero", personal.ID_Genero);
            ViewBag.ID_Grado = new SelectList(db.Grado, "ID_Grado", "Numero_Grado", personal.ID_Grado);
            ViewBag.ID_Seccion = new SelectList(db.Seccion, "ID_Seccion", "Nombre_Seccion", personal.ID_Seccion);
            ViewBag.ID_Tipo_Personal = new SelectList(db.Tipo_Personal, "ID_Tipo_Personal", "Nombre_Tipo_Personal", personal.ID_Tipo_Personal);
            ViewBag.ID_Usuario = new SelectList(db.Usuario, "ID_Usuario", "Nombre_Usuario", personal.ID_Usuario);
            return View(personal);
        }

        //ednpoint

        [HttpGet]
        public JsonResult EsDniUnico(string DNI)
        {
            bool esUnico = !db.Personal.Any(e => e.DNI == DNI);
            return Json(esUnico, JsonRequestBehavior.AllowGet);
        }

        // GET: Personal/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personal personal = db.Personal.Find(id);
            if (personal == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Genero = new SelectList(db.Genero, "ID_Genero", "Nombre_Genero", personal.ID_Genero);
            ViewBag.ID_Grado = new SelectList(db.Grado, "ID_Grado", "Numero_Grado", personal.ID_Grado);
            ViewBag.ID_Seccion = new SelectList(db.Seccion, "ID_Seccion", "Nombre_Seccion", personal.ID_Seccion);
            ViewBag.ID_Tipo_Personal = new SelectList(db.Tipo_Personal, "ID_Tipo_Personal", "Nombre_Tipo_Personal", personal.ID_Tipo_Personal);
            ViewBag.ID_Usuario = new SelectList(db.Usuario, "ID_Usuario", "Nombre_Usuario", personal.ID_Usuario);
            return View(personal);
        }

        // POST: Personal/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Personal,Nombre,Apellido,Fecha_Nacimiento,DNI,Correo,Telefono,Direccion,Firma,Sello,Estado_Registro,ID_Tipo_Personal,ID_Genero,ID_Grado,ID_Seccion,ID_Usuario")] Personal personal)
        {
            if (ModelState.IsValid)
            {
                db.Entry(personal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_Genero = new SelectList(db.Genero, "ID_Genero", "Nombre_Genero", personal.ID_Genero);
            ViewBag.ID_Grado = new SelectList(db.Grado, "ID_Grado", "Numero_Grado", personal.ID_Grado);
            ViewBag.ID_Seccion = new SelectList(db.Seccion, "ID_Seccion", "Nombre_Seccion", personal.ID_Seccion);
            ViewBag.ID_Tipo_Personal = new SelectList(db.Tipo_Personal, "ID_Tipo_Personal", "Nombre_Tipo_Personal", personal.ID_Tipo_Personal);
            ViewBag.ID_Usuario = new SelectList(db.Usuario, "ID_Usuario", "Nombre_Usuario", personal.ID_Usuario);
            return View(personal);
        }

        // GET: Personal/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personal personal = db.Personal.Find(id);
            if (personal == null)
            {
                return HttpNotFound();
            }
            return View(personal);
        }

        // POST: Personal/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Personal personal = db.Personal.Find(id);
            db.Personal.Remove(personal);
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
