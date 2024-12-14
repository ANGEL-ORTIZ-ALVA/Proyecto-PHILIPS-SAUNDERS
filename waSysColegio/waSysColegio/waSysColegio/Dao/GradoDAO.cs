using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace waSysColegio.Dao
{
    public class GradoDAO
    {
        static String cadena = ConfigurationManager.ConnectionStrings["ColegioBD"].ConnectionString;
        SqlConnection cn = new SqlConnection(cadena);

        public DataTable listarGrado()
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from Grado", cn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        public DataTable ListarGrados()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT ID_Grado, Numero_Grado FROM Grado WHERE Estado_Registro = 'Registrado'", cn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
    }
}