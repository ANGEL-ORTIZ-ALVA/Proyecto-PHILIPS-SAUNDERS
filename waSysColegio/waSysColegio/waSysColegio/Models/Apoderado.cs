using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace waSysColegio.Models
{
    public class Apoderado
    {
        public int ID_Apoderado { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string DNI { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Estado_Registro { get; set; }
        public int ID_Genero { get; set; }
        public int ID_Usuario { get; set; }

        public Apoderado() { }

        public Apoderado(int iD_Apoderado, string nombre, string apellido, string dNI, string correo, string telefono, string direccion, string estado_Registro, int iD_Genero, int iD_Usuario)
        {
            ID_Apoderado = iD_Apoderado;
            Nombre = nombre;
            Apellido = apellido;
            DNI = dNI;
            Correo = correo;
            Telefono = telefono;
            Direccion = direccion;
            Estado_Registro = estado_Registro;
            ID_Genero = iD_Genero;
            ID_Usuario = iD_Usuario;
        }
    }
}