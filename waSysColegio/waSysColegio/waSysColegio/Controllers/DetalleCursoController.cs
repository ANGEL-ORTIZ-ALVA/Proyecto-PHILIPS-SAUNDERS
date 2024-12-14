using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using waSysColegio.Dao;
using waSysColegio.Models;

namespace waSysColegio.Controllers
{
    public class DetalleCursoController : Controller
    {
        // GET: DetalleCurso
        [HttpGet]
        public ActionResult RegistrarNota(int idEstudiante)
        {
            var daoCurso = new CursoDAO(); // Supón que tienes un método para obtener todos los cursos activos
            var daoEvaluacion = new EvaluacionDAO();
            var daoAsistencia = new AsistenciaDAO();
            var daoPeriodo = new PeriodoDAO();

            ViewBag.Cursos = daoCurso.ObtenerCursosActivos();
            ViewBag.Evaluaciones = daoEvaluacion.ObtenerEvaluacionesActivas();
            ViewBag.Asistencias = daoAsistencia.ObtenerAsistenciasActivas();
            ViewBag.Periodos = daoPeriodo.ObtenerPeriodosActivos();
            // Crear el modelo de vista que necesitas para el formulario de registro de nota.

            // Crear una instancia de EstudianteDao
            EstudianteDAO estudianteDao = new EstudianteDAO();
            // Llamar al método ObtenerEstudiantePorId usando la instancia
            Estudiante estudiante = estudianteDao.ObtenerEstudiantePorId(idEstudiante);

            DetalleCurso model = new DetalleCurso
            {
                ID_Estudiante = idEstudiante,
                //NombreEstudiante = $"{estudiante.Nombre} {estudiante.Apellido}" // Concatenar nombre completo
                NombreEstudiante = estudiante.Nombre + " " + estudiante.Apellido
                // Inicializa otros campos del modelo si es necesario.
            };

            return View(model);

        }

        [HttpPost]
        public ActionResult RegistrarNota(DetalleCurso model)
        {
            /* if (ModelState.IsValid)
             {
                 DetalleCursoDao daoDetalleCurso = new DetalleCursoDao();
                 daoDetalleCurso.RegistrarNota(model); // Método para insertar en la tabla Detalle_Curso

                 return RedirectToAction("Index", "Estudiante"); // Redirige de vuelta a la lista de estudiantes
             }
             return View(model);*/
            try
            {
                if (ModelState.IsValid)
                {
                    DetalleCursoDAO daoDetalleCurso = new DetalleCursoDAO();
                    daoDetalleCurso.RegistrarNota(model); // Método para insertar en la tabla Detalle_Curso

                    // Establecer mensaje de éxito
                    TempData["SuccessMessage"] = "Nota registrada exitosamente.";

                    // Redirige de vuelta a la vista donde se muestra la lista de estudiantes o notas
                    return RedirectToAction("VerNotas", new { idEstudiante = model.ID_Estudiante });
                }

                // Si el modelo no es válido, devuelve la vista con el modelo actual
                return View(model);
            }
            catch (Exception ex)
            {
                // Establecer el mensaje de error
                TempData["ErrorMessage"] = ex.Message;

                // Redirige de vuelta a la misma vista en caso de error
                return RedirectToAction("RegistrarNota", new { idEstudiante = model.ID_Estudiante });
            }


        }

        //ver notas
        [HttpGet]
        // Obtener opciones para los filtros
        public ActionResult VerNotas(int idEstudiante, int? idCurso = null, int? idEvaluacion = null, int? idAsistencia = null, int? idPeriodo = null)
        {
            EstudianteDAO estudianteDao = new EstudianteDAO();

            DetalleCursoDAO detalleCursoDao = new DetalleCursoDAO();

            CursoDAO cursos = new CursoDAO();
            AsistenciaDAO asistencias = new AsistenciaDAO();
            EvaluacionDAO evaluacion = new EvaluacionDAO();
            PeriodoDAO periodo = new PeriodoDAO();

            // Obtener el estudiante y los datos de sus notas
            var estudiante = estudianteDao.ObtenerEstudiantePorId(idEstudiante);
            var notas = detalleCursoDao.ObtenerNotasPorEstudiante(idEstudiante, idCurso, idEvaluacion, idAsistencia, idPeriodo);

            // Obtener listas para los ComboBoxes
            ViewBag.Cursos = cursos.ObtenerCursosActivos();
            ViewBag.Evaluaciones = evaluacion.ObtenerEvaluacionesActivas();
            ViewBag.Asistencias = asistencias.ObtenerAsistenciasActivas();
            ViewBag.Periodos = periodo.ObtenerPeriodosActivos();

            // ViewBag.NombreEstudiante = $"{estudiante.Nombre} {estudiante.Apellido}";
            ViewBag.EstudianteNombre = $"{estudiante.Nombre} {estudiante.Apellido}";
            ViewBag.IdEstudiante = estudiante.ID_Estudiante;

            return View(notas);
        }

        // GET: Muestra el formulario de edición
        [HttpGet]
        public ActionResult EditarNota(int idEstudiante, int idCurso, int idEvaluacion, int idAsistencia, int idPeriodo)
        {
            DetalleCursoDAO dao = new DetalleCursoDAO();
            var detalleCurso = dao.ObtenerNotaPorIds(idEstudiante, idCurso, idEvaluacion, idAsistencia, idPeriodo);
            return View(detalleCurso);
        }

        // POST: Procesa la edición
        [HttpPost]
        public ActionResult EditarNota(DetalleCurso model)
        {
            if (ModelState.IsValid)
            {
                DetalleCursoDAO dao = new DetalleCursoDAO();
                try
                {
                    dao.EditarNota(model);
                    return RedirectToAction("VerNotas", new { idEstudiante = model.ID_Estudiante });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error al guardar los cambios: " + ex.Message);
                }
            }
            return View(model);
        }

        //cambiar estado

        [HttpPost]
        public ActionResult EliminarNota(int idEstudiante, int idCurso, int idEvaluacion, int idAsistencia, int idPeriodo)
        {
            DetalleCursoDAO dao = new DetalleCursoDAO();
            try
            {
                dao.EliminarNota(idEstudiante, idCurso, idEvaluacion, idAsistencia, idPeriodo);
                return RedirectToAction("VerNotas", new { idEstudiante = idEstudiante });
            }
            catch (Exception ex)
            {
                // Manejar el error como prefieras
                TempData["Error"] = "Error al eliminar la nota: " + ex.Message;
                return RedirectToAction("VerNotas", new { idEstudiante = idEstudiante });
            }
        }
    }
}