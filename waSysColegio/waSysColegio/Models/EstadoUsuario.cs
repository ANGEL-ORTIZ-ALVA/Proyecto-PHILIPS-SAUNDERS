using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace waSysColegio.Models
{
    public class EstadoUsuario
    {
        public int ID_Estado_Usuario { get; set; }
        public string Nombre_Estado_Usuario { get; set; }
        public string Estado_Registro { get; set; }
        public EstadoUsuario() { }
        public EstadoUsuario(int iD_Estado_Usuario, string nombre_Estado_Usuario, string estado_Registro)
        {
            ID_Estado_Usuario = iD_Estado_Usuario;
            Nombre_Estado_Usuario = nombre_Estado_Usuario;
            Estado_Registro = estado_Registro;
        }
    }
}