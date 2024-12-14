using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace waSysColegio.Models
{
    public class EstudianteViewModel
    {
        public int ID_Estudiante { get; set; }
        public string NombreCompleto { get; set; }
        public string DNI { get; set; }
        public string Grado { get; set; }
        public string Seccion { get; set; }
    }
}