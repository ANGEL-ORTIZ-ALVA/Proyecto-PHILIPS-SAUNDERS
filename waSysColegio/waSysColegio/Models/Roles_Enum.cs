using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace waSysColegio.Models
{
    public enum Roles_Enum
    {
        [Description("Administrador")]
        Administrador = 1,
        [Description("Docente")]
        Docente = 2,
        [Description("Estudiante")]
        Estudiante = 3,
        [Description("Apoderado")]
        Apoderado = 4,
        [Description("Directiva")]
        Directiva = 5
    }
}