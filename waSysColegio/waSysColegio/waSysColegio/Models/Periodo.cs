using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace waSysColegio.Models
{
    public class Periodo
    {
        public int ID_Periodo { get; set; }
        public string Nombre_Periodo { get; set; }
        public string Descripcion { get; set; }
        public string Estado_Registro { get; set; }

        public Periodo() { }
        public Periodo(int iD_Periodo, string nombre_Periodo, string descripcion, string estado_Registro)
        {
            ID_Periodo = iD_Periodo;
            Nombre_Periodo = nombre_Periodo;
            Descripcion = descripcion;
            Estado_Registro = estado_Registro;
        }
    }
}