using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using waSysColegio.Models;

namespace waSysColegio.Dao
{
    public class PeriodoDAO
    {
        static string cadena = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
        private SqlConnection conn = new SqlConnection(cadena);

        //listar
        public DataTable listarPeriodo()
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from Periodo;", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public List<Periodo> ObtenerPeriodosActivos()
        {
            List<Periodo> periodosActivos = new List<Periodo>();
            string sql = "SELECT * FROM Periodo WHERE Estado_Registro = 'Registrado'";

            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader dr = null;

            try
            {
                conn.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Periodo periodo = new Periodo
                    {
                        ID_Periodo = dr.GetInt32(dr.GetOrdinal("ID_Periodo")),
                        Nombre_Periodo = dr.GetString(dr.GetOrdinal("Nombre_Periodo")),
                        Descripcion = dr.IsDBNull(dr.GetOrdinal("Descripcion")) ? null : dr.GetString(dr.GetOrdinal("Descripcion")),
                        Estado_Registro = dr.GetString(dr.GetOrdinal("Estado_Registro"))
                    };
                    periodosActivos.Add(periodo);
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al obtener periodos activos: " + ex.Message);
            }
            finally
            {
                conn.Close();
                cmd.Dispose();
                if (dr != null) dr.Close();
            }

            return periodosActivos;
        }
    }
}