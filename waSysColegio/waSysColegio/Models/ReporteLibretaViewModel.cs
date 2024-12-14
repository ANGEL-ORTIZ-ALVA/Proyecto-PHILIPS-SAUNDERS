using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace waSysColegio.Models
{
    public class ReporteLibretaViewModel
    {
        public string Grado { get; set; }
        public string Seccion { get; set; }
        public string NombreEstudiante { get; set; }
        public string DNI { get; set; }
        public string Curso { get; set; }
        public string Competencia1 { get; set; }
        public string Competencia2 { get; set; }
        public string Competencia3 { get; set; }
        public string Competencia4 { get; set; }
        public string Proyecto { get; set; }
        public string ExamenFinal { get; set; }
        public double NotaFinal { get; set; }
        public string NotaFinalLetra { get; set; }
    }
}