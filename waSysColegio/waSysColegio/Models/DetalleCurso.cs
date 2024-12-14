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

        [Required(ErrorMessage = "El Periodo es obligatorio.")]
        public int ID_Periodo { get; set; }

        [Required(ErrorMessage = "La nota es obligatoria.")]
        [Range(0, 20, ErrorMessage = "La nota debe estar entre 0 y 20.")]
        public decimal Competencia1 { get; set; }

        [Required(ErrorMessage = "La nota es obligatoria.")]
        [Range(0, 20, ErrorMessage = "La nota debe estar entre 0 y 20.")]
        public decimal Competencia2 { get; set; }

        [Required(ErrorMessage = "La nota es obligatoria.")]
        [Range(0, 20, ErrorMessage = "La nota debe estar entre 0 y 20.")]
        public decimal Competencia3 { get; set; }

        [Required(ErrorMessage = "La nota es obligatoria.")]
        [Range(0, 20, ErrorMessage = "La nota debe estar entre 0 y 20.")]
        public decimal Competencia4 { get; set; }

        [Required(ErrorMessage = "La nota es obligatoria.")]
        [Range(0, 20, ErrorMessage = "La nota debe estar entre 0 y 20.")]
        public decimal Proyecto { get; set; }

        [Required(ErrorMessage = "La nota es obligatoria.")]
        [Range(0, 20, ErrorMessage = "La nota debe estar entre 0 y 20.")]
        public decimal ExamenFinal { get; set; }
        public string Estado_Registro { get; set; } = "Registrado";

        // Propiedades adicionales para mostrar en la vista
        public string NombreEstudiante { get; set; }  // Propiedad adicional para el nombre del estudiante
        public string ApellidoEstudiante { get; set; }  // Propiedad adicional para el apellido del estudiante

        public DetalleCurso() { }

        public DetalleCurso(int iD_Estudiante, int iD_Curso, int iD_Periodo, decimal competencia1, decimal competencia2, decimal competencia3, decimal competencia4, decimal proyecto, decimal examenFinal, string estado_Registro)
        {
            ID_Estudiante = iD_Estudiante;
            ID_Curso = iD_Curso;
            ID_Periodo = iD_Periodo;
            Competencia1 = competencia1;
            Competencia2 = competencia2;
            Competencia3 = competencia3;
            Competencia4 = competencia4;
            Proyecto = proyecto;
            ExamenFinal = examenFinal;
            Estado_Registro = estado_Registro;
        }
    }
}