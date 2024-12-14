using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace waSysColegio.Models
{
    public class TipoPersonal
    {
        public int ID_Tipo_Personal { get; set; }

        public string Nombre_Tipo_Personal { get; set; }

        public string Descripcion { get; set; }

        public string Estado_Registro { get; set; }

        public TipoPersonal()
        {
        }

        public TipoPersonal(int iD_Tipo_Personal, string nombre_Tipo_Personal, string descripcion, string estado_Registro)
        {
            ID_Tipo_Personal = iD_Tipo_Personal;
            Nombre_Tipo_Personal = nombre_Tipo_Personal;
            Descripcion = descripcion;
            Estado_Registro = estado_Registro;
        }
    }
}