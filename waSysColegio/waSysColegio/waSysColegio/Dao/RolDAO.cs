using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace waSysColegio.Dao
{
    public class RolDAO
    {
    static string cadena = ConfigurationManager.ConnectionStrings["colegioBD"].ConnectionString;
    SqlConnection cn = new SqlConnection(cadena);

        public DataTable listarRoles()
        {
            SqlDataAdapter da = new SqlDataAdapter("select*from Rol", cn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
    }
}