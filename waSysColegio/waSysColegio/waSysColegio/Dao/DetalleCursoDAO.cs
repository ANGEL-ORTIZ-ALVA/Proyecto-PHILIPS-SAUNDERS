using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using waSysColegio.Models;

namespace waSysColegio.Dao
{
    public class DetalleCursoDAO
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["colegioBD"].ConnectionString;
        static string cadena = ConfigurationManager.ConnectionStrings["colegioBD"].ConnectionString;
        private SqlConnection conn = new SqlConnection(cadena);


        // Método para insertar un nuevo detalle del curso
        public bool AgregarDetalleCurso(DetalleCurso detalle)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Detalle_Curso (ID_Estudiante, ID_Curso, ID_Evaluacion, ID_Asistencia, ID_Periodo, Nota, Estado_Registro) " +
                               "VALUES (@ID_Estudiante, @ID_Curso, @ID_Evaluacion, @ID_Asistencia, @ID_Periodo, @Nota, @Estado_Registro)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ID_Estudiante", detalle.ID_Estudiante);
                cmd.Parameters.AddWithValue("@ID_Curso", detalle.ID_Curso);
                cmd.Parameters.AddWithValue("@ID_Evaluacion", detalle.ID_Evaluacion);
                cmd.Parameters.AddWithValue("@ID_Asistencia", detalle.ID_Asistencia);
                cmd.Parameters.AddWithValue("@ID_Periodo", detalle.ID_Periodo);
                cmd.Parameters.AddWithValue("@Nota", detalle.Nota);
                cmd.Parameters.AddWithValue("@Estado_Registro", detalle.Estado_Registro);

                con.Open();
                int rows = cmd.ExecuteNonQuery();
                con.Close();
                return rows > 0;
            }
        }

        // Método para obtener un detalle del curso por ID
        public DetalleCurso ObtenerDetalleCurso_0(int idCurso)
        {
            DetalleCurso detalle = null;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Detalle_Curso WHERE ID_Curso = @ID_Curso";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ID_Curso", idCurso);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    detalle = new DetalleCurso
                    {
                        ID_Estudiante = (int)reader["ID_Estudiante"],
                        ID_Curso = (int)reader["ID_Curso"],
                        ID_Evaluacion = (int)reader["ID_Evaluacion"],
                        ID_Asistencia = (int)reader["ID_Asistencia"],
                        ID_Periodo = (int)reader["ID_Periodo"],
                        Nota = (decimal)reader["Nota"],
                        Estado_Registro = reader["Estado_Registro"].ToString()
                    };
                }
            }

            return detalle;
        }

        //Metodo para obtener regsirto con todos los foreigns keys
        public DetalleCurso ObtenerDetalleCurso(int idEstudiante, int idCurso, int idEvaluacion, int idAsistencia, int idPeriodo)
        {
            DetalleCurso detalle = null;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Detalle_Curso WHERE ID_Estudiante = @ID_Estudiante AND ID_Curso = @ID_Curso AND ID_Evaluacion = @ID_Evaluacion AND ID_Asistencia = @ID_Asistencia AND ID_Periodo = @ID_Periodo";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ID_Estudiante", idEstudiante);
                cmd.Parameters.AddWithValue("@ID_Curso", idCurso);
                cmd.Parameters.AddWithValue("@ID_Evaluacion", idEvaluacion);
                cmd.Parameters.AddWithValue("@ID_Asistencia", idAsistencia);
                cmd.Parameters.AddWithValue("@ID_Periodo", idPeriodo);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    detalle = new DetalleCurso
                    {
                        ID_Estudiante = (int)reader["ID_Estudiante"],
                        ID_Curso = (int)reader["ID_Curso"],
                        ID_Evaluacion = (int)reader["ID_Evaluacion"],
                        ID_Asistencia = (int)reader["ID_Asistencia"],
                        ID_Periodo = (int)reader["ID_Periodo"],
                        Nota = (decimal)reader["Nota"],
                        Estado_Registro = reader["Estado_Registro"].ToString()
                    };
                }
            }

            return detalle;
        }

        // Método para actualizar un detalle del curso


        public bool ActualizarDetalleCurso(DetalleCurso detalle, DetalleCurso originalDetalle)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "UPDATE Detalle_Curso SET ID_Estudiante = @ID_Estudiante, ID_Curso = @ID_Curso, ID_Evaluacion = @ID_Evaluacion, " +
                               "ID_Asistencia = @ID_Asistencia, ID_Periodo = @ID_Periodo, Nota = @Nota, Estado_Registro = @Estado_Registro " +
                               "WHERE ID_Estudiante = @OriginalID_Estudiante AND ID_Curso = @OriginalID_Curso AND ID_Evaluacion = @OriginalID_Evaluacion AND " +
                               "ID_Asistencia = @OriginalID_Asistencia AND ID_Periodo = @OriginalID_Periodo";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ID_Estudiante", detalle.ID_Estudiante);
                cmd.Parameters.AddWithValue("@ID_Curso", detalle.ID_Curso);
                cmd.Parameters.AddWithValue("@ID_Evaluacion", detalle.ID_Evaluacion);
                cmd.Parameters.AddWithValue("@ID_Asistencia", detalle.ID_Asistencia);
                cmd.Parameters.AddWithValue("@ID_Periodo", detalle.ID_Periodo);
                cmd.Parameters.AddWithValue("@Nota", detalle.Nota);
                cmd.Parameters.AddWithValue("@Estado_Registro", detalle.Estado_Registro);

                // Parámetros de las llaves originales
                cmd.Parameters.AddWithValue("@OriginalID_Estudiante", originalDetalle.ID_Estudiante);
                cmd.Parameters.AddWithValue("@OriginalID_Curso", originalDetalle.ID_Curso);
                cmd.Parameters.AddWithValue("@OriginalID_Evaluacion", originalDetalle.ID_Evaluacion);
                cmd.Parameters.AddWithValue("@OriginalID_Asistencia", originalDetalle.ID_Asistencia);
                cmd.Parameters.AddWithValue("@OriginalID_Periodo", originalDetalle.ID_Periodo);

                con.Open();
                int rows = cmd.ExecuteNonQuery();
                return rows > 0;
            }
        }




        // Método para eliminar un detalle del curso
        public bool EliminarDetalleCurso(int idEstudiante, int idCurso, int idEvaluacion, int idAsistencia, int idPeriodo)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // Usar todas las claves en la consulta DELETE
                string query = @"DELETE FROM Detalle_Curso 
                         WHERE ID_Estudiante = @ID_Estudiante
                         AND ID_Curso = @ID_Curso 
                         AND ID_Evaluacion = @ID_Evaluacion 
                         AND ID_Asistencia = @ID_Asistencia 
                         AND ID_Periodo = @ID_Periodo";

                SqlCommand cmd = new SqlCommand(query, con);
                // Añadir los parámetros para todas las claves
                cmd.Parameters.AddWithValue("@ID_Estudiante", idEstudiante);
                cmd.Parameters.AddWithValue("@ID_Curso", idCurso);
                cmd.Parameters.AddWithValue("@ID_Evaluacion", idEvaluacion);
                cmd.Parameters.AddWithValue("@ID_Asistencia", idAsistencia);
                cmd.Parameters.AddWithValue("@ID_Periodo", idPeriodo);

                con.Open();
                int result = cmd.ExecuteNonQuery();
                return result > 0;  // Retornar true si se eliminó al menos un registro
            }
        }

        //registrar nota
        public void RegistrarNota(DetalleCurso model)
        {
            string sql = "INSERT INTO Detalle_Curso (ID_Estudiante, ID_Curso, ID_Evaluacion, ID_Asistencia, ID_Periodo, Nota, Estado_Registro) " +
                         "VALUES (@IDEstudiante, @IDCurso, @IDEvaluacion, @IDAsistencia, @IDPeriodo, @Nota, @EstadoRegistro)";

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@IDEstudiante", model.ID_Estudiante);
            cmd.Parameters.AddWithValue("@IDCurso", model.ID_Curso);
            cmd.Parameters.AddWithValue("@IDEvaluacion", model.ID_Evaluacion);
            cmd.Parameters.AddWithValue("@IDAsistencia", model.ID_Asistencia);
            cmd.Parameters.AddWithValue("@IDPeriodo", model.ID_Periodo);
            cmd.Parameters.AddWithValue("@Nota", model.Nota);
            cmd.Parameters.AddWithValue("@EstadoRegistro", model.Estado_Registro);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627 || ex.Number == 2601) // Códigos de error para duplicados
                {
                    throw new Exception("Error: Insertó un registro duplicado, vuelva a intentar.");
                }
                else
                {
                    throw new Exception("Error al registrar la nota: " + ex.Message);
                }
            }
            finally
            {
                conn.Close();
                cmd.Dispose();
            }
        }

        //obtener nota de estudiante
        public List<DetalleCurso> ObtenerNotasPorEstudiante(int idEstudiante, int? idCurso = null, int? idEvaluacion = null, int? idAsistencia = null, int? idPeriodo = null)
        {
            List<DetalleCurso> lista = new List<DetalleCurso>();
            string sql = @"
            SELECT dc.ID_Estudiante, dc.ID_Curso, dc.ID_Evaluacion, dc.ID_Asistencia, dc.ID_Periodo, dc.Nota, dc.Estado_Registro,
                   c.Nombre_Curso, e.Nombre_Evaluacion, a.Nombre_Tipo_Asistencia, p.Nombre_Periodo, 
                   est.Nombre, est.Apellido
            FROM Detalle_Curso dc
            INNER JOIN Curso c ON dc.ID_Curso = c.ID_Curso
            INNER JOIN Evaluacion e ON dc.ID_Evaluacion = e.ID_Evaluacion
            LEFT JOIN Asistencia a ON dc.ID_Asistencia = a.ID_Asistencia
            INNER JOIN Periodo p ON dc.ID_Periodo = p.ID_Periodo
            INNER JOIN Estudiante est ON dc.ID_Estudiante = est.ID_Estudiante
            WHERE dc.ID_Estudiante = @idEstudiante
              AND (@idCurso IS NULL OR dc.ID_Curso = @idCurso)
              AND (@idEvaluacion IS NULL OR dc.ID_Evaluacion = @idEvaluacion)
              AND (@idAsistencia IS NULL OR dc.ID_Asistencia = @idAsistencia)
              AND (@idPeriodo IS NULL OR dc.ID_Periodo = @idPeriodo)
              AND dc.Estado_Registro = 'Registrado'";

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@IDEstudiante", idEstudiante);
            cmd.Parameters.AddWithValue("@idCurso", (object)idCurso ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@idEvaluacion", (object)idEvaluacion ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@idAsistencia", (object)idAsistencia ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@idPeriodo", (object)idPeriodo ?? DBNull.Value);
            SqlDataReader dr = null;

            try
            {
                conn.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    DetalleCurso detalle = new DetalleCurso
                    {
                        ID_Estudiante = dr.GetInt32(0),
                        ID_Curso = dr.GetInt32(1),
                        ID_Evaluacion = dr.GetInt32(2),
                        ID_Asistencia = (int)(dr.IsDBNull(3) ? (int?)null : dr.GetInt32(3)),
                        ID_Periodo = dr.GetInt32(4),
                        Nota = dr.GetDecimal(5),
                        Estado_Registro = dr.GetString(6),
                        NombreCurso = dr.GetString(7),
                        NombreEvaluacion = dr.GetString(8),
                        NombreAsistencia = dr.IsDBNull(9) ? null : dr.GetString(9),
                        NombrePeriodo = dr.GetString(10),
                        NombreEstudiante = dr.GetString(11),
                        ApellidoEstudiante = dr.GetString(12)
                    };
                    lista.Add(detalle);
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al obtener las notas del estudiante: " + ex.Message);
            }
            finally
            {
                conn.Close();
                cmd.Dispose();
                dr?.Close();
            }
            return lista;
        }
        //Editar nota
        public void EditarNota(DetalleCurso model)
        {
            string sql = "UPDATE Detalle_Curso SET Nota = @Nota " +
                         "WHERE ID_Estudiante = @IDEstudiante " +
                         "AND ID_Curso = @IDCurso " +
                         "AND ID_Evaluacion = @IDEvaluacion " +
                         "AND ID_Asistencia = @IDAsistencia " +
                         "AND ID_Periodo = @IDPeriodo";

            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@IDEstudiante", model.ID_Estudiante);
                cmd.Parameters.AddWithValue("@IDCurso", model.ID_Curso);
                cmd.Parameters.AddWithValue("@IDEvaluacion", model.ID_Evaluacion);
                cmd.Parameters.AddWithValue("@IDAsistencia", model.ID_Asistencia);
                cmd.Parameters.AddWithValue("@IDPeriodo", model.ID_Periodo);
                cmd.Parameters.AddWithValue("@Nota", model.Nota);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error al actualizar la nota: " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public DetalleCurso ObtenerNotaPorIds(int idEstudiante, int idCurso, int idEvaluacion, int idAsistencia, int idPeriodo)
        {
            string sql = @"SELECT dc.*, 
                   e.Nombre + ' ' + e.Apellido as NombreEstudiante,
                   c.Nombre_Curso as NombreCurso,
                   ev.Nombre_Evaluacion as NombreEvaluacion,
                   a.Nombre_Tipo_Asistencia as NombreAsistencia,
                   p.Nombre_Periodo as NombrePeriodo,
                   e.Apellido as ApellidoEstudiante
                   FROM Detalle_Curso dc
                   INNER JOIN Estudiante e ON dc.ID_Estudiante = e.ID_Estudiante
                   INNER JOIN Curso c ON dc.ID_Curso = c.ID_Curso
                   INNER JOIN Evaluacion ev ON dc.ID_Evaluacion = ev.ID_Evaluacion
                   INNER JOIN Asistencia a ON dc.ID_Asistencia = a.ID_Asistencia
                   INNER JOIN Periodo p ON dc.ID_Periodo = p.ID_Periodo
                   WHERE dc.ID_Estudiante = @IDEstudiante 
                   AND dc.ID_Curso = @IDCurso 
                   AND dc.ID_Evaluacion = @IDEvaluacion 
                   AND dc.ID_Asistencia = @IDAsistencia 
                   AND dc.ID_Periodo = @IDPeriodo";

            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@IDEstudiante", idEstudiante);
                cmd.Parameters.AddWithValue("@IDCurso", idCurso);
                cmd.Parameters.AddWithValue("@IDEvaluacion", idEvaluacion);
                cmd.Parameters.AddWithValue("@IDAsistencia", idAsistencia);
                cmd.Parameters.AddWithValue("@IDPeriodo", idPeriodo);

                DetalleCurso detalle = null;
                try
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            detalle = new DetalleCurso
                            {
                                ID_Estudiante = reader.GetInt32(reader.GetOrdinal("ID_Estudiante")),
                                ID_Curso = reader.GetInt32(reader.GetOrdinal("ID_Curso")),
                                ID_Evaluacion = reader.GetInt32(reader.GetOrdinal("ID_Evaluacion")),
                                ID_Asistencia = reader.GetInt32(reader.GetOrdinal("ID_Asistencia")),
                                ID_Periodo = reader.GetInt32(reader.GetOrdinal("ID_Periodo")),
                                Nota = reader.GetDecimal(reader.GetOrdinal("Nota")),
                                Estado_Registro = reader.GetString(reader.GetOrdinal("Estado_Registro")),
                                NombreEstudiante = reader.GetString(reader.GetOrdinal("NombreEstudiante")),
                                NombreCurso = reader.GetString(reader.GetOrdinal("NombreCurso")),
                                NombreEvaluacion = reader.GetString(reader.GetOrdinal("NombreEvaluacion")),
                                NombreAsistencia = reader.GetString(reader.GetOrdinal("NombreAsistencia")),
                                NombrePeriodo = reader.GetString(reader.GetOrdinal("NombrePeriodo")),
                                ApellidoEstudiante = reader.GetString(reader.GetOrdinal("ApellidoEstudiante"))
                            };
                        }
                    }
                }
                finally
                {
                    conn.Close();
                }
                return detalle;
            }
        }

        //cambiar estado a eliminado
        public void EliminarNota(int idEstudiante, int idCurso, int idEvaluacion, int idAsistencia, int idPeriodo)
        {
            string sql = "UPDATE Detalle_Curso SET Estado_Registro = 'Eliminado' " +
                         "WHERE ID_Estudiante = @IDEstudiante " +
                         "AND ID_Curso = @IDCurso " +
                         "AND ID_Evaluacion = @IDEvaluacion " +
                         "AND ID_Asistencia = @IDAsistencia " +
                         "AND ID_Periodo = @IDPeriodo";

            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@IDEstudiante", idEstudiante);
                cmd.Parameters.AddWithValue("@IDCurso", idCurso);
                cmd.Parameters.AddWithValue("@IDEvaluacion", idEvaluacion);
                cmd.Parameters.AddWithValue("@IDAsistencia", idAsistencia);
                cmd.Parameters.AddWithValue("@IDPeriodo", idPeriodo);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error al eliminar la nota: " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

    }
}