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
    public class ApoderadoController : Controller
    {
        private ColegioBDv2Entities db = new ColegioBDv2Entities();

        // GET: Apoderado
        public ActionResult Index()
        {
            var apoderado = db.Apoderado.Include(a => a.Genero).Include(a => a.Usuario);
            return View(apoderado.ToList());
        }

        // GET: Apoderado/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Apoderado apoderado = db.Apoderado.Find(id);
            if (apoderado == null)
            {
                return HttpNotFound();
            }
            return View(apoderado);
        }

        // GET: Apoderado/Create
        public ActionResult Create()
        {
            ViewBag.ID_Genero = new SelectList(db.Genero, "ID_Genero", "Nombre_Genero");
            ViewBag.ID_Usuario = new SelectList(db.Usuario, "ID_Usuario", "Nombre_Usuario");
            return View();
        }

        // POST: Apoderado/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Apoderado,Nombre,Apellido,DNI,Correo,Telefono,Direccion,ID_Genero,ID_Usuario")] Apoderado apoderado)
        {
            if (ModelState.IsValid)
            {

                // Asigna el valor predeterminado
                apoderado.Estado_Registro = "Registrado";

                db.Apoderado.Add(apoderado);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_Genero = new SelectList(db.Genero, "ID_Genero", "Nombre_Genero", apoderado.ID_Genero);
            ViewBag.ID_Usuario = new SelectList(db.Usuario, "ID_Usuario", "Nombre_Usuario", apoderado.ID_Usuario);
            return View(apoderado);
        }

        //endpoint

        [HttpGet]
        public JsonResult EsDniUnico(string DNI)
        {
            bool esUnico = !db.Apoderado.Any(e => e.DNI == DNI);
            return Json(esUnico, JsonRequestBehavior.AllowGet);
        }

        // GET: Apoderado/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Apoderado apoderado = db.Apoderado.Find(id);
            if (apoderado == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Genero = new SelectList(db.Genero, "ID_Genero", "Nombre_Genero", apoderado.ID_Genero);
            ViewBag.ID_Usuario = new SelectList(db.Usuario, "ID_Usuario", "Nombre_Usuario", apoderado.ID_Usuario);
            return View(apoderado);
        }

        // POST: Apoderado/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Apoderado,Nombre,Apellido,DNI,Correo,Telefono,Direccion,Estado_Registro,ID_Genero,ID_Usuario")] Apoderado apoderado)
        {
            if (ModelState.IsValid)
            {
                db.Entry(apoderado).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_Genero = new SelectList(db.Genero, "ID_Genero", "Nombre_Genero", apoderado.ID_Genero);
            ViewBag.ID_Usuario = new SelectList(db.Usuario, "ID_Usuario", "Nombre_Usuario", apoderado.ID_Usuario);
            return View(apoderado);
        }

        // GET: Apoderado/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Apoderado apoderado = db.Apoderado.Find(id);
            if (apoderado == null)
            {
                return HttpNotFound();
            }
            return View(apoderado);
        }

        // POST: Apoderado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Apoderado apoderado = db.Apoderado.Find(id);
            db.Apoderado.Remove(apoderado);
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
