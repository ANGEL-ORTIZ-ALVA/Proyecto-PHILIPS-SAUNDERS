using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using waSysColegio.Models;

namespace waSysColegio.Dao
{
    public class DetalleCursoDAO
    {
       /* private string connectionString = ConfigurationManager.ConnectionStrings["colegioBD"].ConnectionString;
        static string cadena = ConfigurationManager.ConnectionStrings["colegioBD"].ConnectionString;
        private SqlConnection con = new SqlConnection(cadena);*/

        static string caden = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
        private SqlConnection conn = new SqlConnection(caden);


        public void GuardarNotas(List<Models.DetalleCurso> detalleCursos)
        {
            string query = @"
IF EXISTS (SELECT 1 FROM Detalle_Curso WHERE ID_Estudiante = @ID_Estudiante AND ID_Curso = @ID_Curso AND ID_Periodo = @ID_Periodo)
BEGIN
    UPDATE Detalle_Curso
    SET Competencia1 = @Competencia1, Competencia2 = @Competencia2, Competencia3 = @Competencia3, Competencia4 = @Competencia4, 
        Proyecto = @Proyecto, ExamenFinal = @ExamenFinal, Estado_Registro = @Estado_Registro
    WHERE ID_Estudiante = @ID_Estudiante AND ID_Curso = @ID_Curso AND ID_Periodo = @ID_Periodo
END
ELSE
BEGIN
    INSERT INTO Detalle_Curso (ID_Estudiante, ID_Curso, ID_Periodo, Competencia1, Competencia2, Competencia3, Competencia4, Proyecto, ExamenFinal, Estado_Registro)
    VALUES (@ID_Estudiante, @ID_Curso, @ID_Periodo, @Competencia1, @Competencia2, @Competencia3, @Competencia4, @Proyecto, @ExamenFinal, @Estado_Registro)
END";

            using (SqlConnection connection = new SqlConnection(caden))
            {
                connection.Open();
                SqlTransaction transaction = null;
                try
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add("@ID_Estudiante", SqlDbType.Int);
                        command.Parameters.Add("@ID_Curso", SqlDbType.Int);
                        command.Parameters.Add("@ID_Periodo", SqlDbType.Int);
                        command.Parameters.Add("@Competencia1", SqlDbType.Decimal);
                        command.Parameters.Add("@Competencia2", SqlDbType.Decimal);
                        command.Parameters.Add("@Competencia3", SqlDbType.Decimal);
                        command.Parameters.Add("@Competencia4", SqlDbType.Decimal);
                        command.Parameters.Add("@Proyecto", SqlDbType.Decimal);
                        command.Parameters.Add("@ExamenFinal", SqlDbType.Decimal);
                        command.Parameters.Add("@Estado_Registro", SqlDbType.VarChar, 15);

                        transaction = connection.BeginTransaction();
                        command.Transaction = transaction;

                        foreach (var detalle in detalleCursos)
                        {
                            command.Parameters["@ID_Estudiante"].Value = detalle.ID_Estudiante;
                            command.Parameters["@ID_Curso"].Value = detalle.ID_Curso;
                            command.Parameters["@ID_Periodo"].Value = detalle.ID_Periodo;
                            command.Parameters["@Competencia1"].Value = detalle.Competencia1;
                            command.Parameters["@Competencia2"].Value = detalle.Competencia2;
                            command.Parameters["@Competencia3"].Value = detalle.Competencia3;
                            command.Parameters["@Competencia4"].Value = detalle.Competencia4;
                            command.Parameters["@Proyecto"].Value = detalle.Proyecto;
                            command.Parameters["@ExamenFinal"].Value = detalle.ExamenFinal;
                            command.Parameters["@Estado_Registro"].Value = detalle.Estado_Registro;
                            command.ExecuteNonQuery();
                        }
                        transaction.Commit();
                    }
                }
                catch (Exception ex)
                {
                    if (transaction != null)
                    {
                        transaction.Rollback();
                    }
                    throw ex;
                }
            }
        }


        public List<Models.Estudiante> ObtenerEstudiantesPorSeccion(int idSeccion)
        {
            List<Models.Estudiante> estudiantes = new List<Models.Estudiante>();

            string query = "SELECT ID_Estudiante, Nombre, Apellido FROM Estudiante WHERE ID_Seccion = @idSeccion";

            using (SqlConnection connection = new SqlConnection(caden))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@idSeccion", idSeccion);

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Models.Estudiante estudiante = new Models.Estudiante
                            {
                                ID_Estudiante = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Apellido = reader.GetString(2)
                            };
                            estudiantes.Add(estudiante);
                        }
                    }
                }
            }

            return estudiantes;
        }

        public List<Models.DetalleCurso> ObtenerEstudiantesConNotas(int idSeccion, int idCurso, int idPeriodo)
        {
            List<Models.DetalleCurso> estudiantesConNotas = new List<Models.DetalleCurso>();

            string query = @"
        SELECT 
            e.ID_Estudiante,
            ISNULL(dc.Competencia1, 0) AS Competencia1,
            ISNULL(dc.Competencia2, 0) AS Competencia2,
            ISNULL(dc.Competencia3, 0) AS Competencia3,
            ISNULL(dc.Competencia4, 0) AS Competencia4,
            ISNULL(dc.Proyecto, 0) AS Proyecto,
            ISNULL(dc.ExamenFinal, 0) AS ExamenFinal,
            ISNULL(dc.Estado_Registro, 'Registrado') AS Estado_Registro
        FROM Estudiante e
        LEFT JOIN Detalle_Curso dc ON e.ID_Estudiante = dc.ID_Estudiante 
                                    AND dc.ID_Curso = @ID_Curso 
                                    AND dc.ID_Periodo = @ID_Periodo
        WHERE e.ID_Seccion = @ID_Seccion
        ORDER BY e.Apellido, e.Nombre";

            using (SqlConnection connection = new SqlConnection(caden))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID_Seccion", idSeccion);
                    command.Parameters.AddWithValue("@ID_Curso", idCurso);
                    command.Parameters.AddWithValue("@ID_Periodo", idPeriodo);

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Models.DetalleCurso detalle = new Models.DetalleCurso
                            {
                                ID_Estudiante = Convert.ToInt32(reader["ID_Estudiante"]),
                                Competencia1 = Convert.ToDecimal(reader["Competencia1"]),
                                Competencia2 = Convert.ToDecimal(reader["Competencia2"]),
                                Competencia3 = Convert.ToDecimal(reader["Competencia3"]),
                                Competencia4 = Convert.ToDecimal(reader["Competencia4"]),
                                Proyecto = Convert.ToDecimal(reader["Proyecto"]),
                                ExamenFinal = Convert.ToDecimal(reader["ExamenFinal"]),
                                Estado_Registro = reader["Estado_Registro"].ToString()
                            };

                            estudiantesConNotas.Add(detalle);
                        }
                    }
                }
            }

            return estudiantesConNotas;
        }

    }
}