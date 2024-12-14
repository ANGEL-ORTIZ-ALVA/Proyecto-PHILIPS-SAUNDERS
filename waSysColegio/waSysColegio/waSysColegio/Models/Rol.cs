using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace waSysColegio.Models
{
    public class Rol
    {
        public int ID_Rol { get; set; }
        public string Nombre_Rol { get; set; }
        public string Permiso { get; set; }
        public string Estado_Registro { get; set; }
        public Rol() { }
        public Rol(int iD_Rol, string nombre_Rol, string permiso, string estado_Registro)
        {
            ID_Rol = iD_Rol;
            Nombre_Rol = nombre_Rol;
            Permiso = permiso;
            Estado_Registro = estado_Registro;
        }
    }
}