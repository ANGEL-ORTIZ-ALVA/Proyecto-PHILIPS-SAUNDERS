using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace waSysColegio.Models
{
    public class Estudiante
    {
        public int ID_Estudiante { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime Fecha_Nacimiento { get; set; }
        public string DNI { get; set; }
        public string Direccion { get; set; }
        public string Estado_Registro { get; set; }
        public int ID_Genero { get; set; }
        public int ID_Grado { get; set; }
        public int ID_Seccion { get; set; }
        public int ID_Usuario { get; set; }

        public Estudiante()
        {
        }

        public Estudiante(int iD_Estudiante, string nombre, string apellido, DateTime fecha_Nacimiento, string dNI, string direccion, string estado_Registro, int iD_Genero, int iD_Grado, int iD_Seccion, int iD_Usuario)
        {
            ID_Estudiante = iD_Estudiante;
            Nombre = nombre;
            Apellido = apellido;
            Fecha_Nacimiento = fecha_Nacimiento;
            DNI = dNI;
            Direccion = direccion;
            Estado_Registro = estado_Registro;
            ID_Genero = iD_Genero;
            ID_Grado = iD_Grado;
            ID_Seccion = iD_Seccion;
            ID_Usuario = iD_Usuario;
        }
    }
}