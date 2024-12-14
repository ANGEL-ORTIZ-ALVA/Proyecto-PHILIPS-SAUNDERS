using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace waSysColegio.Models
{
    public class Evaluacion
    {
        public int ID_Evaluacion { get; set; }
        public string Nombre_Evaluacion { get; set; }
        public string Descripcion { get; set; }
        public string Estado_Registro { get; set; }

        public Evaluacion() { }

        public Evaluacion(int iD_Evaluacion, string nombre_Evaluacion, string descripcion, string estado_Registro)
        {
            ID_Evaluacion = iD_Evaluacion;
            Nombre_Evaluacion = nombre_Evaluacion;
            Descripcion = descripcion;
            Estado_Registro = estado_Registro;
        }
    }
}