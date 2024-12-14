using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace waSysColegio.Dao
{
    public class SeccionDAO
    {
        static String cadena = ConfigurationManager.ConnectionStrings["ColegioBD"].ConnectionString;
        SqlConnection cn = new SqlConnection(cadena);

        //Listar secciones
        public DataTable listarSeccion()
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from Seccion", cn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public DataTable ListarSecciones()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT ID_Seccion, Nombre_Seccion FROM Seccion WHERE Estado_Registro = 'Registrado'", cn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
    }
}