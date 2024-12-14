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
    public class PeriodoController : Controller
    {
        private ColegioBDv2Entities db = new ColegioBDv2Entities();

        // GET: Periodo
        public ActionResult Index()
        {
            return View(db.Periodo.ToList());
        }

        // GET: Periodo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Periodo periodo = db.Periodo.Find(id);
            if (periodo == null)
            {
                return HttpNotFound();
            }
            return View(periodo);
        }

        // GET: Periodo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Periodo/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Periodo,Nombre_Periodo,Descripcion")] Periodo periodo)
        {
            if (ModelState.IsValid)
            {
                // Asigna el valor predeterminado
                periodo.Estado_Registro = "Registrado";

                db.Periodo.Add(periodo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(periodo);
        }

        // GET: Periodo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Periodo periodo = db.Periodo.Find(id);
            if (periodo == null)
            {
                return HttpNotFound();
            }
            return View(periodo);
        }

        // POST: Periodo/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Periodo,Nombre_Periodo,Descripcion,Estado_Registro")] Periodo periodo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(periodo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(periodo);
        }

        // GET: Periodo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Periodo periodo = db.Periodo.Find(id);
            if (periodo == null)
            {
                return HttpNotFound();
            }
            return View(periodo);
        }

        // POST: Periodo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Periodo periodo = db.Periodo.Find(id);
            db.Periodo.Remove(periodo);
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
