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
    public class GeneroController : Controller
    {
        private ColegioBDv2Entities db = new ColegioBDv2Entities();

        // GET: Genero
        public ActionResult Index()
        {
            return View(db.Genero.ToList());
        }

        // GET: Genero/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Genero genero = db.Genero.Find(id);
            if (genero == null)
            {
                return HttpNotFound();
            }
            return View(genero);
        }

        // GET: Genero/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Genero/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Genero,Nombre_Genero")] Genero genero)
        {
            if (ModelState.IsValid)
            {
                // Asigna el valor predeterminado
                genero.Estado_Registro = "Registrado";

                db.Genero.Add(genero);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(genero);
        }

        // GET: Genero/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Genero genero = db.Genero.Find(id);
            if (genero == null)
            {
                return HttpNotFound();
            }
            return View(genero);
        }

        // POST: Genero/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Genero,Nombre_Genero,Estado_Registro")] Genero genero)
        {
            if (ModelState.IsValid)
            {
                db.Entry(genero).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(genero);
        }

        // GET: Genero/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Genero genero = db.Genero.Find(id);
            if (genero == null)
            {
                return HttpNotFound();
            }
            return View(genero);
        }

        // POST: Genero/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Genero genero = db.Genero.Find(id);
            db.Genero.Remove(genero);
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
