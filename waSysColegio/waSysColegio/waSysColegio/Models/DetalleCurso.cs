using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace waSysColegio.Models
{
    public class DetalleCurso
    {
        [Required(ErrorMessage = "El ID de Estudiante es obligatorio.")]
        public int ID_Estudiante { get; set; }

        [Required(ErrorMessage = "El Curso es obligatorio.")]
        public int ID_Curso { get; set; }

        [Required(ErrorMessage = "La Evaluacion es obligatoria.")]
        public int ID_Evaluacion { get; set; }

        [Required(ErrorMessage = "La Asistencia es obligatoria.")]
        public int ID_Asistencia { get; set; }

        [Required(ErrorMessage = "El Periodo es obligatorio.")]
        public int ID_Periodo { get; set; }

        [Required(ErrorMessage = "La nota es obligatoria.")]
        [Range(0, 20, ErrorMessage = "La nota debe estar entre 0 y 20.")]
        public decimal Nota { get; set; }
        public string Estado_Registro { get; set; } = "Registrado";

        // Propiedades adicionales para mostrar en la vista
        public string NombreEstudiante { get; set; } // Nombre completo del estudiante
        public string NombreCurso { get; set; }
        public string NombreEvaluacion { get; set; }
        public string NombreAsistencia { get; set; }
        public string NombrePeriodo { get; set; }
        public string ApellidoEstudiante { get; set; }

        public DetalleCurso() { }

        public DetalleCurso(int iD_Estudiante, int iD_Curso, int iD_Evaluacion, int iD_Asistencia, int iD_Periodo, decimal nota, string estado_Registro, string nombreEstudiante, string nombreCurso, string nombreEvaluacion, string nombreAsistencia, string nombrePeriodo, string apellidoEstudiante)
        {
            ID_Estudiante = iD_Estudiante;
            ID_Curso = iD_Curso;
            ID_Evaluacion = iD_Evaluacion;
            ID_Asistencia = iD_Asistencia;
            ID_Periodo = iD_Periodo;
            Nota = nota;
            Estado_Registro = estado_Registro;
            NombreEstudiante = nombreEstudiante;
            NombreCurso = nombreCurso;
            NombreEvaluacion = nombreEvaluacion;
            NombreAsistencia = nombreAsistencia;
            NombrePeriodo = nombrePeriodo;
            ApellidoEstudiante = apellidoEstudiante;
        }
    }
}