using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace waSysColegio.Models
{
    public class Libreta
    {
        public int ID_Libreta { get; set; }

        public DateTime Anio_Escolar { get; set; }

        public string Estado_Registro { get; set; }

        public int ID_Estudiante { get; set; }

        public Libreta()
        {
        }

        public Libreta(int iD_Libreta, DateTime anio_Escolar, string estado_Registro, int iD_Estudiante)
        {
            ID_Libreta = iD_Libreta;
            Anio_Escolar = anio_Escolar;
            Estado_Registro = estado_Registro;
            ID_Estudiante = iD_Estudiante;
        }
    }
}