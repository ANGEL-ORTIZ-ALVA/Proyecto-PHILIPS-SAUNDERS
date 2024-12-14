using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace waSysColegio.Models
{
    public class Seccion
    {
        [Required(ErrorMessage = "El ID de Estudiante es obligatorio.")]
        public int ID_Seccion { get; set; }
        [Required(ErrorMessage = "El ID de Estudiante es obligatorio.")]
        public string Nombre_Seccion { get; set; }
        [Required(ErrorMessage = "El ID de Estudiante es obligatorio.")]
        [Range(0, 20, ErrorMessage = "El aforo debe estar entre 15 y 30.")]
        public int Aforo { get; set; }
        [Required(ErrorMessage = "El ID de Estudiante es obligatorio.")]
        public string Estado_Registro { get; set; }

        public Seccion() { }

        public Seccion(int iD_Seccion, string nombre_Seccion, int aforo, string estado_Registro)
        {
            ID_Seccion = iD_Seccion;
            Nombre_Seccion = nombre_Seccion;
            Aforo = aforo;
            Estado_Registro = estado_Registro;
        }
    }
}