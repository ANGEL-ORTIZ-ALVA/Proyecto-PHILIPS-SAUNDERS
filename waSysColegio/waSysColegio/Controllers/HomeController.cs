using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using waSysColegio.Models;

namespace waSysColegio.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            int? userRole = Session["Rol"] as int?;
            if (userRole == null || !Enum.IsDefined(typeof(Roles_Enum), userRole.Value))
            {
                // Redirigir a login si el rol no es válido
                return RedirectToAction("Login", "Account");
            }

            ViewBag.Rol = userRole.Value; // Asegúrate de que nunca sea null en la vista
            return View();
        }

        // Acción de Login (Redirigir si se requiere)
        public ActionResult Login()
        {
            return View();
        }
    }
}