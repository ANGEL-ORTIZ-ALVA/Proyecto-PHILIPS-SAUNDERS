using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace waSysColegio.Models
{
    public class Grado
    {
        public int ID_Grado { get; set; }

        public string Numero_Grado { get; set; }

        public string Estado_Registro { get; set; }

        public Grado()
        {
        }

        public Grado(int iD_Grado, string numero_Grado, string estado_Registro)
        {
            ID_Grado = iD_Grado;
            Numero_Grado = numero_Grado;
            Estado_Registro = estado_Registro;
        }
    }
}