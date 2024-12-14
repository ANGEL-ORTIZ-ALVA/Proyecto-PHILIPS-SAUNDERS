using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using waSysColegio;

namespace waSysColegio.Controllers
{
    public class UsuarioController : Controller
    {
        private ColegioBDv2Entities db = new ColegioBDv2Entities();

        // GET: Usuario
        public ActionResult Index()
        {
            var usuario = db.Usuario.Include(u => u.Estado_Usuario).Include(u => u.Rol);
            return View(usuario.ToList());
        }

        // GET: Usuario/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // GET: Usuario/Create
        public ActionResult Create()
        {
            ViewBag.ID_Estado_Usuario = new SelectList(db.Estado_Usuario, "ID_Estado_Usuario", "Nombre_Estado_Usuario");
            ViewBag.ID_Rol = new SelectList(db.Rol, "ID_Rol", "Nombre_Rol");
            return View();
        }


        private void PrepararViewBags()
        {
            ViewBag.ID_Estado_Usuario = new SelectList(db.Estado_Usuario, "ID_Estado_Usuario", "Nombre_Estado_Usuario");
            ViewBag.ID_Rol = new SelectList(db.Rol, "ID_Rol", "Nombre_Rol");
        }
        // POST: Usuario/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Usuario,Nombre_Usuario,Password,Fecha_Creacion,Ultimo_Acceso,Estado_Registro,ID_Estado_Usuario,ID_Rol")] Usuario usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Validación adicional: verifica si el usuario ya existe
                    if (db.Usuario.Any(u => u.Nombre_Usuario == usuario.Nombre_Usuario))
                    {
                        ModelState.AddModelError("Nombre_Usuario", "El nombre de usuario ya existe.");
                        PrepararViewBags();
                        return View(usuario);
                    }

                    // Asignar valores por defecto
                    usuario.Fecha_Creacion = DateTime.Now;
                    usuario.Ultimo_Acceso = DateTime.Now;
                    usuario.Estado_Registro = "Registrado";

                    // Registrar usuario
                    db.Usuario.Add(usuario);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al registrar el usuario: " + ex.Message);
            }

            PrepararViewBags();
            return View(usuario);
        }

        // GET: Usuario/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }

            // Cargar las listas
            var estados = db.Estado_Usuario.ToList();
            var roles = db.Rol.ToList();

            ViewBag.ID_Estado_Usuario = new SelectList(estados, "ID_Estado_Usuario", "Nombre_Estado_Usuario", usuario.ID_Estado_Usuario);
            ViewBag.ID_Rol = new SelectList(roles, "ID_Rol", "Nombre_Rol", usuario.ID_Rol);

            return View(usuario);
        }

        // POST: Usuario/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Usuario,Nombre_Usuario,Password,Estado_Registro,ID_Estado_Usuario,ID_Rol,Fecha_Creacion,Ultimo_Acceso")] Usuario usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(usuario).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DbEntityValidationException ex)
            {
                // Obtener los detalles de la validación fallida
                var errorMessages = string.Join("; ", ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.ErrorMessage));
                ModelState.AddModelError("", $"Error al actualizar el usuario: {errorMessages}");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error al actualizar el usuario: {ex.Message}");
            }

            // Recargar las listas en caso de error
            ViewBag.ID_Estado_Usuario = new SelectList(
                db.Estado_Usuario,
                "ID_Estado_Usuario",
                "Nombre_Estado_Usuario",
                usuario.ID_Estado_Usuario);
            ViewBag.ID_Rol = new SelectList(
                db.Rol,
                "ID_Rol",
                "Nombre_Rol",
                usuario.ID_Rol);

            return View(usuario);
        }

        // GET: Usuario/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuario
                              .Include(u => u.Estado_Usuario)
                              .Include(u => u.Rol)
                              .FirstOrDefault(u => u.ID_Usuario == id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                // Usando LINQ para actualizar el estado
                var usuario = db.Usuario.FirstOrDefault(u => u.ID_Usuario == id);
                if (usuario != null)
                {
                    usuario.Estado_Registro = "Eliminado";
                    usuario.Ultimo_Acceso = DateTime.Now; //actualizar última modificación

                    // cambiar el estado del usuario si es necesario
                    usuario.ID_Estado_Usuario = 2;

                    db.Entry(usuario).State = EntityState.Modified;
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Log del error
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Error al eliminar el usuario");
            }
        }

        // Método para restaurar usuarios
        public ActionResult Restore(int id)
        {
            try
            {
                var usuario = db.Usuario
                    .FirstOrDefault(u => u.ID_Usuario == id && u.Estado_Registro == "Eliminado");

                if (usuario != null)
                {
                    usuario.Estado_Registro = "Registrado";
                    usuario.Ultimo_Acceso = DateTime.Now;
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }

        // Ver usuarios eliminados 
        public ActionResult DeletedUsers()
        {
            var usuariosEliminados = db.Usuario
                                      .Include(u => u.Estado_Usuario)
                                      .Include(u => u.Rol)
                                      .Where(u => u.Estado_Registro == "Eliminado");
            return View("Index", usuariosEliminados.ToList());
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
