using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace waSysColegio.Models
{
    public class Detalle_Libreta
    {
        public int ID_Libreta { get; set; }
        public int ID_Personal { get; set; }
        public byte[] Firma { get; set; }
        public byte[] Sello { get; set; }
        public string Estado_Registro { get; set; }

        public Detalle_Libreta()
        {
        }

        public Detalle_Libreta(int id_Libreta, int id_Personal, byte[] firma, byte[] sello, string estado_Registro)
        {
            ID_Libreta = id_Libreta;
            ID_Personal = id_Personal;
            Firma = firma;
            Sello = sello;
            Estado_Registro = estado_Registro;
        }
    }
}