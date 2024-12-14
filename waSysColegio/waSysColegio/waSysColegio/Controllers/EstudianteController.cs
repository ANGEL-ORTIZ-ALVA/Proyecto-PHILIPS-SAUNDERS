using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using waSysColegio.Dao;
using waSysColegio.Models;

namespace waSysColegio.Controllers
{
    public class EstudianteController : Controller
    {
        // GET: Estudiante
        public ActionResult VerEstudiante()
        {
            EstudianteDAO daoEstudiante = new EstudianteDAO();
            List<Estudiante> listaEstudiantes = daoEstudiante.listarEstudiante();

            return View(listaEstudiantes);
        }
    }
}