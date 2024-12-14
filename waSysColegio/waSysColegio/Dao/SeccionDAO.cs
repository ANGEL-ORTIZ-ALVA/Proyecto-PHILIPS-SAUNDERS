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
        static String cadena = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
        SqlConnection cn = new SqlConnection(cadena);

        //Listar secciones
        public List<Models.Seccion> ObtenerTodasLasSecciones()
        {
            List<Models.Seccion> secciones = new List<Models.Seccion>();
            string query = "SELECT ID_Seccion, Nombre_Seccion FROM Seccion";

            using (SqlConnection connection = new SqlConnection(cadena))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Models.Seccion seccion = new Models.Seccion
                            {
                                ID_Seccion = reader.GetInt32(0),
                                Nombre_Seccion = reader.GetString(1)
                            };
                            secciones.Add(seccion);
                        }
                    }
                }
            }

            return secciones;
        }

        public Models.Seccion ObtenerSeccionPorID(int idSeccion)
        {
            Models.Seccion seccion = null;
            string query = "SELECT ID_Seccion, Nombre FROM Seccion WHERE ID_Seccion = @idSeccion";

            using (SqlConnection connection = new SqlConnection(cadena))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@idSeccion", idSeccion);

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            seccion = new Models.Seccion
                            {
                                ID_Seccion = reader.GetInt32(0),
                                Nombre_Seccion = reader.GetString(1)
                            };
                        }
                    }
                }
            }
            return seccion;
        }

        // Método para agregar una nueva sección
        public void AgregarSeccion(Models.Seccion seccion)
        {
            string query = "INSERT INTO Seccion (Nombre_Seccion) VALUES (@nombre)";

            using (SqlConnection connection = new SqlConnection(cadena))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nombre", seccion.Nombre_Seccion);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        // Método para actualizar una sección existente
        public void ActualizarSeccion(Models.Seccion seccion)
        {
            string query = "UPDATE Seccion SET Nombre = @nombre WHERE ID_Seccion = @idSeccion";

            using (SqlConnection connection = new SqlConnection(cadena))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nombre", seccion.Nombre_Seccion);
                    command.Parameters.AddWithValue("@idSeccion", seccion.ID_Seccion);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        // Método para eliminar una sección por su ID
        public void EliminarSeccion(int idSeccion)
        {
            string query = "DELETE FROM Seccion WHERE ID_Seccion = @idSeccion";

            using (SqlConnection connection = new SqlConnection(cadena))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@idSeccion", idSeccion);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
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