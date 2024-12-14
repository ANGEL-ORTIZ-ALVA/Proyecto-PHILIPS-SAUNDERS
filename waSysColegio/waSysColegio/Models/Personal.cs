using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace waSysColegio.Models
{
    public class Personal
    {
        public int ID_Personal { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public DateTime Fecha_Nacimiento { get; set; }

        public string DNI { get; set; }

        public string Correo { get; set; }

        public string Telefono { get; set; }

        public string Direccion { get; set; }

        public string Estado_Registro { get; set; }

        public int ID_Tipo_Personal { get; set; }

        public int ID_Genero { get; set; }

        public int ID_Grado { get; set; }

        public int ID_Seccion { get; set; }

        public int ID_Direccion { get; set; }

        public int ID_Telefono { get; set; }

        public int ID_Usuario { get; set; }

        public Personal()
        {
        }

        public Personal(int iD_Personal, string nombre, string apellido, DateTime fecha_Nacimiento, string dNI, string correo, string telefono, string direccion, string estado_Registro, int iD_Tipo_Personal, int iD_Genero, int iD_Grado, int iD_Seccion, int iD_Direccion, int iD_Telefono, int iD_Usuario)
        {
            ID_Personal = iD_Personal;
            Nombre = nombre;
            Apellido = apellido;
            Fecha_Nacimiento = fecha_Nacimiento;
            DNI = dNI;
            Correo = correo;
            Telefono = telefono;
            Direccion = direccion;
            Estado_Registro = estado_Registro;
            ID_Tipo_Personal = iD_Tipo_Personal;
            ID_Genero = iD_Genero;
            ID_Grado = iD_Grado;
            ID_Seccion = iD_Seccion;
            ID_Direccion = iD_Direccion;
            ID_Telefono = iD_Telefono;
            ID_Usuario = iD_Usuario;
        }
    }
}