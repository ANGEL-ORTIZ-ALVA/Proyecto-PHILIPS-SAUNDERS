using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace waSysColegio.Models
{
    public class Asistencia
    {
        public int ID_Asistencia { get; set; }
        public string Nombre_Tipo_Asistencia { get; set; }

        public string Descripcion { get; set; }
        public string Estado_Registro { get; set; }

        public Asistencia() { }
        public Asistencia(int iD_Asistencia, string nombre_Tipo_Asistencia, string descripcion, string estado_Registro)
        {
            ID_Asistencia = iD_Asistencia;
            Nombre_Tipo_Asistencia = nombre_Tipo_Asistencia;
            Descripcion = descripcion;
            Estado_Registro = estado_Registro;
        }
    }
}