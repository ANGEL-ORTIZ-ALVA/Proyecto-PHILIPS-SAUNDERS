using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace waSysColegio.Models
{
    public class Usuario
    {
        public int ID_Usuario { get; set; }
        public string Nombre_Usuario { get; set; }
        public string Password { get; set; }
        public DateTime Fecha_Creacion { get; set; }
        public DateTime? Ultimo_Acceso { get; set; }
        public string Estado_Registro { get; set; }
        public int ID_Estado_Usuario { get; set; }
        public int ID_Rol { get; set; }
        public Usuario() { }
        public Usuario(int iD_Usuario, string nombre_Usuario, string password, DateTime fecha_Creacion, DateTime? ultimo_Acceso, string estado_Registro, int iD_Estado_Usuario, int iD_Rol)
        {
            ID_Usuario = iD_Usuario;
            Nombre_Usuario = nombre_Usuario;
            Password = password;
            Fecha_Creacion = fecha_Creacion;
            Ultimo_Acceso = ultimo_Acceso;
            Estado_Registro = estado_Registro;
            ID_Estado_Usuario = iD_Estado_Usuario;
            ID_Rol = iD_Rol;
        }
    }
}