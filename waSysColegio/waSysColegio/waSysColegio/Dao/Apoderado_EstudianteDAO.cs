using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace waSysColegio.Dao
{
    public class Apoderado_EstudianteDAO
    {
        static String connectionString = ConfigurationManager.ConnectionStrings["colegioBD"].ConnectionString;
        SqlConnection conn = new SqlConnection(connectionString);

        //listar tabla apoderados_estudiantes
        public DataTable ListarApoEstudiantes()
        {
            DataTable dt = new DataTable();
            string query = @"SELECT ae.ID_Apoderado, ae.ID_Estudiante, ae.Parentesco, ae.Estado_Registro, 
                    a.Nombre AS Nombre_Apoderado, e.Nombre AS Nombre_Estudiante
                    FROM Apoderado_Estudiante ae
                    JOIN Apoderado a ON ae.ID_Apoderado = a.ID_Apoderado
                    JOIN Estudiante e ON ae.ID_Estudiante = e.ID_Estudiante";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.Fill(dt);
            }

            return dt;
        }

        public void AgregarApoEstudiante(int idApoderado, int idEstudiante, string parentesco, string estadoRegistro)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
            INSERT INTO Apoderado_Estudiante (ID_Apoderado, ID_Estudiante, Parentesco, Estado_Registro)
            VALUES (@ID_Apoderado, @ID_Estudiante, @Parentesco, @Estado_Registro)";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ID_Apoderado", idApoderado);
                    cmd.Parameters.AddWithValue("@ID_Estudiante", idEstudiante);
                    cmd.Parameters.AddWithValue("@Parentesco", parentesco);
                    cmd.Parameters.AddWithValue("@Estado_Registro", estadoRegistro);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException sqlEx)
            {
                throw new Exception("Error de base de datos: " + sqlEx.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar el registro: " + ex.Message);
            }
        }

        public DataRow ObtenerApoEstudiantePorID(int idApoderado, int idEstudiante)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"SELECT * FROM Apoderado_Estudiante 
                                 WHERE ID_Apoderado = @ID_Apoderado AND ID_Estudiante = @ID_Estudiante";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@ID_Apoderado", idApoderado);
                da.SelectCommand.Parameters.AddWithValue("@ID_Estudiante", idEstudiante);

                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    return dt.Rows[0];
                }
                else
                {
                    return null;
                }
            }
        }

        public void ActualizarApoEstudiante(int idApoderado, int idEstudiante, string parentesco, string estadoRegistro)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                UPDATE Apoderado_Estudiante 
                SET Parentesco = @Parentesco, Estado_Registro = @Estado_Registro
                WHERE ID_Apoderado = @ID_Apoderado AND ID_Estudiante = @ID_Estudiante";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ID_Apoderado", idApoderado);
                cmd.Parameters.AddWithValue("@ID_Estudiante", idEstudiante);
                cmd.Parameters.AddWithValue("@Parentesco", parentesco);
                cmd.Parameters.AddWithValue("@Estado_Registro", estadoRegistro);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void EliminarApoEstudiante(int idApoderado, int idEstudiante)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                DELETE FROM Apoderado_Estudiante 
                WHERE ID_Apoderado = @ID_Apoderado AND ID_Estudiante = @ID_Estudiante";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ID_Apoderado", idApoderado);
                cmd.Parameters.AddWithValue("@ID_Estudiante", idEstudiante);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}