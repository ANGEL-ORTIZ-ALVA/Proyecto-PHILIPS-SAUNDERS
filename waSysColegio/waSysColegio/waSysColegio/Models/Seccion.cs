using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace waSysColegio.Models
{
    public class Seccion
    {
        public int ID_Seccion { get; set; }

        public string Nombre_Seccion { get; set; }

        public int Aforo { get; set; }

        public string Estado_Registro { get; set; }

        public Seccion()
        {
        }

        public Seccion(int iD_Seccion, string nombre_Seccion, int aforo, string estado_Registro)
        {
            ID_Seccion = iD_Seccion;
            Nombre_Seccion = nombre_Seccion;
            Aforo = aforo;
            Estado_Registro = estado_Registro;
        }
    }
}