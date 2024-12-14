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
    public class Estado_UsuarioController : Controller
    {
        private ColegioBDv2Entities db = new ColegioBDv2Entities();

        // GET: Estado_Usuario
        public ActionResult Index()
        {
            return View(db.Estado_Usuario.ToList());
        }

        // GET: Estado_Usuario/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estado_Usuario estado_Usuario = db.Estado_Usuario.Find(id);
            if (estado_Usuario == null)
            {
                return HttpNotFound();
            }
            return View(estado_Usuario);
        }

        // GET: Estado_Usuario/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Estado_Usuario/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Estado_Usuario,Nombre_Estado_Usuario,Estado_Registro")] Estado_Usuario estado_Usuario)
        {
            if (ModelState.IsValid)
            {
                db.Estado_Usuario.Add(estado_Usuario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(estado_Usuario);
        }

        // GET: Estado_Usuario/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estado_Usuario estado_Usuario = db.Estado_Usuario.Find(id);
            if (estado_Usuario == null)
            {
                return HttpNotFound();
            }
            return View(estado_Usuario);
        }

        // POST: Estado_Usuario/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Estado_Usuario,Nombre_Estado_Usuario,Estado_Registro")] Estado_Usuario estado_Usuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(estado_Usuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(estado_Usuario);
        }

        // GET: Estado_Usuario/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estado_Usuario estado_Usuario = db.Estado_Usuario.Find(id);
            if (estado_Usuario == null)
            {
                return HttpNotFound();
            }
            return View(estado_Usuario);
        }

        // POST: Estado_Usuario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Estado_Usuario estado_Usuario = db.Estado_Usuario.Find(id);
            db.Estado_Usuario.Remove(estado_Usuario);
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
