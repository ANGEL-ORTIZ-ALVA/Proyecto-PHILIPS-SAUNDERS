using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace waSysColegio.Models
{
    public class GradoSeccionMainViewModel
    {
        public List<GradoSeccionViewModel> Grados { get; set; }
        public List<SeccionViewModel> Secciones { get; set; }
        public int SelectedGradoID { get; set; }
        public int SelectedSeccionID { get; set; }
    }

    public class GradoSeccionViewModel
    {
        public int ID_Grado { get; set; }
        public string Numero_Grado { get; set; }
        public List<SeccionViewModel> Secciones { get; set; } // Añadir la lista de secciones aquí
    }


    public class SeccionViewModel
    {
        public int ID_Seccion { get; set; }
        public string Nombre_Seccion { get; set; }
    }
}