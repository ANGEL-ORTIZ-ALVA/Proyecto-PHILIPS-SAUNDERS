using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace waSysColegio.Models
{
    public class Curso
    {
        public int ID_Curso { get; set; }
        public string Nombre_Curso { get; set; }
        public string Descripcion { get; set; }
        public string Estado_Registro { get; set; }
        public int ID_Personal { get; set; }

        public Curso() { }

        public Curso(int iD_Curso, string nombre_Curso, string descripcion, string estado_Registro, int iD_Personal)
        {
            ID_Curso = iD_Curso;
            Nombre_Curso = nombre_Curso;
            Descripcion = descripcion;
            Estado_Registro = estado_Registro;
            ID_Personal = iD_Personal;
        }
    }
}