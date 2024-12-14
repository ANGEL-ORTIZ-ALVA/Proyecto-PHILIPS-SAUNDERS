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
    public class AsistenciaDAO
    {
        static String cadena = ConfigurationManager.ConnectionStrings["colegioBD"].ConnectionString;
        SqlConnection conn = new SqlConnection(cadena);

        //listar
        public DataTable listarAsistencia()
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from Asistencia;", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public List<Asistencia> ObtenerAsistenciasActivas()
        {
            List<Asistencia> asistenciasActivas = new List<Asistencia>();
            string sql = "SELECT * FROM Asistencia WHERE Estado_Registro = 'Registrado'";

            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader dr = null;

            try
            {
                conn.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Asistencia asistencia = new Asistencia
                    {
                        ID_Asistencia = dr.GetInt32(dr.GetOrdinal("ID_Asistencia")),
                        Nombre_Tipo_Asistencia = dr.GetString(dr.GetOrdinal("Nombre_Tipo_Asistencia")),
                        Descripcion = dr.IsDBNull(dr.GetOrdinal("Descripcion")) ? null : dr.GetString(dr.GetOrdinal("Descripcion")),
                        Estado_Registro = dr.GetString(dr.GetOrdinal("Estado_Registro"))
                    };
                    asistenciasActivas.Add(asistencia);
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al obtener asistencias activas: " + ex.Message);
            }
            finally
            {
                conn.Close();
                cmd.Dispose();
                if (dr != null) dr.Close();
            }

            return asistenciasActivas;
        }
    }
}