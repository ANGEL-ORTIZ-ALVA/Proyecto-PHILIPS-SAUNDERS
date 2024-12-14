using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace waSysColegio.Models
{
    public class Genero
    {
        public int ID_Genero { get; set; }

        public string Nombre_Genero { get; set; }

        public string Estado_Registro { get; set; }

        public Genero()
        {
        }

        public Genero(int iD_Genero, string nombre_Genero, string estado_Registro)
        {
            ID_Genero = iD_Genero;
            Nombre_Genero = nombre_Genero;
            Estado_Registro = estado_Registro;
        }
    }
}