using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace waSysColegio.Models
{
    public class Apoderado_Estudiante
    {
        public int ID_Estudiante { get; set; }
        public int ID_Curso { get; set; }
        public int ID_Evaluacion { get; set; }
        public int ID_Asistencia { get; set; }
        public int ID_Periodo { get; set; }
        public decimal Nota { get; set; }
        public string Estado_Registro { get; set; }

        public Apoderado_Estudiante() { }

        public Apoderado_Estudiante(int iD_Estudiante, int iD_Curso, int iD_Evaluacion, int iD_Asistencia, int iD_Periodo, decimal nota, string estado_Registro)
        {
            ID_Estudiante = iD_Estudiante;
            ID_Curso = iD_Curso;
            ID_Evaluacion = iD_Evaluacion;
            ID_Asistencia = iD_Asistencia;
            ID_Periodo = iD_Periodo;
            Nota = nota;
            Estado_Registro = estado_Registro;
        }
    }

}