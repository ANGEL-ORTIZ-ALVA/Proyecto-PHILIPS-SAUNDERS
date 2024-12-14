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
    public class EvaluacionDAO
    {
        static String cadena = ConfigurationManager.ConnectionStrings["colegioBD"].ConnectionString;
        SqlConnection conn = new SqlConnection(cadena);

        //listar
        public DataTable listarEvaluacion()
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from Evaluacion;", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public List<Evaluacion> ObtenerEvaluacionesActivas()
        {
            List<Evaluacion> evaluacionesActivas = new List<Evaluacion>();
            string sql = "SELECT * FROM Evaluacion WHERE Estado_Registro = 'Registrado'";

            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader dr = null;

            try
            {
                conn.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Evaluacion evaluacion = new Evaluacion
                    {
                        ID_Evaluacion = dr.GetInt32(dr.GetOrdinal("ID_Evaluacion")),
                        Nombre_Evaluacion = dr.GetString(dr.GetOrdinal("Nombre_Evaluacion")),
                        Descripcion = dr.IsDBNull(dr.GetOrdinal("Descripcion")) ? null : dr.GetString(dr.GetOrdinal("Descripcion")),
                        Estado_Registro = dr.GetString(dr.GetOrdinal("Estado_Registro"))
                    };
                    evaluacionesActivas.Add(evaluacion);
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al obtener evaluaciones activas: " + ex.Message);
            }
            finally
            {
                conn.Close();
                cmd.Dispose();
                if (dr != null) dr.Close();
            }

            return evaluacionesActivas;
        }
    }
}