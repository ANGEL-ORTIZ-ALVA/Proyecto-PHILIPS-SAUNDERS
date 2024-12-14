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
    public class CursoDAO
    {
        static String cadena = ConfigurationManager.ConnectionStrings["colegioBD"].ConnectionString;
        SqlConnection conn = new SqlConnection(cadena);

        //listar
        public DataTable listarCurso()
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from Curso;", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public List<Curso> ObtenerCursosActivos()
        {
            List<Curso> cursosActivos = new List<Curso>();
            string sql = "SELECT * FROM Curso WHERE Estado_Registro = 'Registrado'";

            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader dr = null;

            try
            {
                conn.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    // Asigna los valores de cada columna al objeto Curso
                    Curso curso = new Curso
                    {
                        ID_Curso = dr.GetInt32(dr.GetOrdinal("ID_Curso")),
                        Nombre_Curso = dr.GetString(dr.GetOrdinal("Nombre_Curso")),
                        Descripcion = dr.IsDBNull(dr.GetOrdinal("Descripcion")) ? null : dr.GetString(dr.GetOrdinal("Descripcion")),
                        Estado_Registro = dr.GetString(dr.GetOrdinal("Estado_Registro")),
                        ID_Personal = (int)(dr.IsDBNull(dr.GetOrdinal("ID_Personal")) ? (int?)null : dr.GetInt32(dr.GetOrdinal("ID_Personal")))
                    };
                    cursosActivos.Add(curso);
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al obtener cursos activos: " + ex.Message);
            }
            finally
            {
                conn.Close();
                cmd.Dispose();
                if (dr != null) dr.Close();
            }

            return cursosActivos;
        }
    }
}