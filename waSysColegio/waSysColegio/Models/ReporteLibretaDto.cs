using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace waSysColegio.Models
{
    public class ReporteLibretaDto
    {
        public string Grado { get; set; }
        public string Seccion { get; set; }
        public string ApellidosNombresEstudiante { get; set; }
        public string DNI { get; set; }
        public string Curso { get; set; }
        public decimal PromedioPeriodo1 { get; set; }
        public decimal PromedioPeriodo2 { get; set; }
        public decimal PromedioPeriodo3 { get; set; }
        public decimal PromedioPeriodo4 { get; set; }
        public decimal Proyecto { get; set; }
        public decimal ExamenFinal { get; set; }
        public double NotaFinal { get; set; }
    }
}