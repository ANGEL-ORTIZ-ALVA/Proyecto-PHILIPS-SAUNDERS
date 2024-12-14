using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using waSysColegio.Models;

namespace waSysColegio.Dao
{
    public class EstudianteDAO
    {
        static String cadena = ConfigurationManager.ConnectionStrings["colegioBD"].ConnectionString;

        private String connectionString = ConfigurationManager.ConnectionStrings["colegioBD"].ConnectionString;

        SqlConnection cn = new SqlConnection(cadena);
        SqlConnection conn = new SqlConnection(cadena);

        // Método para listar todos los estudiantes registrados
        public DataTable ListarEstudiantesRegistrados()
        {
            SqlDataAdapter da = new SqlDataAdapter(
                "SELECT ID_Estudiante, CONCAT(Nombre, ' ', Apellido) AS NombreCompleto " +
                "FROM Estudiante WHERE Estado_Registro = 'Registrado'", cn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        //listar estudiante por id
        public DataRow ListarEstudiantePorID(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Estudiante WHERE ID_Estudiante = @ID_Estudiante";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@ID_Estudiante", id);
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

        // Método para buscar un estudiante por su ID
        public Estudiante BuscarEstudiantePorID(int idEstudiante)
        {
            Estudiante estudiante = null;
            string sql = "SELECT * FROM Estudiante WHERE ID_Estudiante = @idEstudiante AND Estado_Registro = 'Registrado'";
            SqlCommand cmd = new SqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@idEstudiante", idEstudiante);

            try
            {
                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    estudiante = new Estudiante
                    {
                        ID_Estudiante = (int)reader["ID_Estudiante"],
                        Nombre = reader["Nombre"].ToString(),
                        Apellido = reader["Apellido"].ToString(),
                        Fecha_Nacimiento = (DateTime)reader["Fecha_Nacimiento"],
                        DNI = reader["DNI"].ToString(),
                        Direccion = reader["Direccion"].ToString(),
                        Estado_Registro = reader["Estado_Registro"].ToString(),
                        ID_Genero = (int)reader["ID_Genero"],
                        ID_Grado = (int)reader["ID_Grado"],
                        ID_Seccion = (int)reader["ID_Seccion"],
                        ID_Usuario = (int)reader["ID_Usuario"]
                    };
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar el estudiante: " + ex.Message);
            }
            finally
            {
                cn.Close();
            }
            return estudiante;
        }

        // Método para insertar un nuevo estudiante
        public string InsertarEstudiante(Estudiante estudiante)
        {
            string mensaje = null;
            string sql = "INSERT INTO Estudiante (Nombre, Apellido, Fecha_Nacimiento, DNI, Direccion, Estado_Registro, " +
                         "ID_Genero, ID_Grado, ID_Seccion, ID_Usuario) " +
                         "VALUES (@nombre, @apellido, @fechaNacimiento, @dni, @direccion, @estadoRegistro, " +
                         "@idGenero, @idGrado, @idSeccion, @idUsuario)";
            SqlCommand cmd = new SqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@nombre", estudiante.Nombre);
            cmd.Parameters.AddWithValue("@apellido", estudiante.Apellido);
            cmd.Parameters.AddWithValue("@fechaNacimiento", estudiante.Fecha_Nacimiento);
            cmd.Parameters.AddWithValue("@dni", estudiante.DNI);
            cmd.Parameters.AddWithValue("@direccion", estudiante.Direccion);
            cmd.Parameters.AddWithValue("@estadoRegistro", "Registrado");
            cmd.Parameters.AddWithValue("@idGenero", estudiante.ID_Genero);
            cmd.Parameters.AddWithValue("@idGrado", estudiante.ID_Grado);
            cmd.Parameters.AddWithValue("@idSeccion", estudiante.ID_Seccion);
            cmd.Parameters.AddWithValue("@idUsuario", estudiante.ID_Usuario);

            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                mensaje = "Estudiante registrado correctamente.";
            }
            catch (Exception ex)
            {
                mensaje = "Error al registrar el estudiante: " + ex.Message;
            }
            finally
            {
                cn.Close();
                cmd.Dispose();
            }
            return mensaje;
        }

        // Método para actualizar un estudiante existente
        public string ActualizarEstudiante(Estudiante estudiante)
        {
            string mensaje = null;
            string sql = "UPDATE Estudiante SET Nombre = @nombre, Apellido = @apellido, Fecha_Nacimiento = @fechaNacimiento, " +
                         "DNI = @dni, Direccion = @direccion, ID_Genero = @idGenero, ID_Grado = @idGrado, " +
                         "ID_Seccion = @idSeccion, ID_Usuario = @idUsuario " +
                         "WHERE ID_Estudiante = @idEstudiante AND Estado_Registro = 'Registrado'";
            SqlCommand cmd = new SqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@idEstudiante", estudiante.ID_Estudiante);
            cmd.Parameters.AddWithValue("@nombre", estudiante.Nombre);
            cmd.Parameters.AddWithValue("@apellido", estudiante.Apellido);
            cmd.Parameters.AddWithValue("@fechaNacimiento", estudiante.Fecha_Nacimiento);
            cmd.Parameters.AddWithValue("@dni", estudiante.DNI);
            cmd.Parameters.AddWithValue("@direccion", estudiante.Direccion);
            cmd.Parameters.AddWithValue("@idGenero", estudiante.ID_Genero);
            cmd.Parameters.AddWithValue("@idGrado", estudiante.ID_Grado);
            cmd.Parameters.AddWithValue("@idSeccion", estudiante.ID_Seccion);
            cmd.Parameters.AddWithValue("@idUsuario", estudiante.ID_Usuario);

            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                mensaje = "Estudiante actualizado correctamente.";
            }
            catch (Exception ex)
            {
                mensaje = "Error al actualizar el estudiante: " + ex.Message;
            }
            finally
            {
                cn.Close();
                cmd.Dispose();
            }
            return mensaje;
        }

        // Método para eliminar un estudiante (cambiar su estado a 'Eliminado')
        public string EliminarEstudiante(int idEstudiante)
        {
            string mensaje = null;
            string sql = "UPDATE Estudiante SET Estado_Registro = 'Eliminado' WHERE ID_Estudiante = @idEstudiante";
            SqlCommand cmd = new SqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@idEstudiante", idEstudiante);

            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                mensaje = "Estudiante eliminado correctamente.";
            }
            catch (Exception ex)
            {
                mensaje = "Error al eliminar el estudiante: " + ex.Message;
            }
            finally
            {
                cn.Close();
                cmd.Dispose();
            }
            return mensaje;
        }

        //Agregar estudiante
        public void AgregarEstudiante(string nombre, string apellido, DateTime? fecha_Nacimiento, string dni, string direccion)
        {
            string query = "INSERT INTO Estudiante (Nombre, Apellido, Fecha_Nacimiento, DNI, Estado_Registro, ID_Genero, Direccion, ID_Grado, ID_Seccion, ID_Usuario) " +
                            "VALUES (@Nombre, @Apellido, @Fecha_Nacimiento, @DNI, 'Registrado', 1, @Direccion, 1, 1, 14)";


            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Nombre", nombre);
                    cmd.Parameters.AddWithValue("@Apellido", apellido);
                    cmd.Parameters.AddWithValue("@Fecha_Nacimiento", fecha_Nacimiento);
                    cmd.Parameters.AddWithValue("@DNI", dni);
                    cmd.Parameters.AddWithValue("@Direccion", direccion);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al agregar el apoderado: " + ex.Message);
                }
            }
        }

        public DataTable listaestudiantes()
        {
            DataTable dt = new DataTable();
            string query = @"SELECT e.ID_Estudiante, e.Nombre, e.Apellido, e.Fecha_Nacimiento, e.DNI, e.Estado_Registro, 
                            g.Nombre_Genero, e.Direccion, gr.Numero_Grado, s.Nombre_Seccion, u.Nombre_Usuario
                            FROM Estudiante e
                            JOIN Genero g ON e.ID_Genero = g.ID_Genero
                            JOIN Grado gr ON e.ID_Grado = gr.ID_Grado
                            JOIN Seccion s ON e.ID_Seccion = s.ID_Seccion
                            JOIN Usuario u ON e.ID_Usuario = u.ID_Usuario;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    da.Fill(dt);
                }
                catch (SqlException ex)
                {
                    // Maneja la excepción
                    Console.WriteLine("Error al obtener lista de estudiantes: " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }

            return dt;
        }

        //Para MVC
        //Listar Estudiante
        public List<Estudiante> listarEstudiante()
        {
            List<Estudiante> lista = new List<Estudiante>();
            string sql = "SELECT * FROM Estudiante";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader dr = null;

            try
            {
                conn.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Estudiante estudiante = new Estudiante(
                        dr.GetInt32(0),            // ID_Estudiante
                        dr.GetString(1),           // Nombre
                        dr.GetString(2),           // Apellido
                        dr.GetDateTime(3),         // Fecha_Nacimiento
                        dr.GetString(4),           // DNI
                        dr.GetString(5),           // Direccion
                        dr.GetString(6),           // Estado_Registro
                        dr.GetInt32(7),            // ID_Genero
                        dr.GetInt32(8),            // ID_Grado
                        dr.GetInt32(9),            // ID_Seccion
                        dr.GetInt32(10)            // ID_Usuario
                    );
                    lista.Add(estudiante);
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al listar estudiantes: " + ex.Message);
            }
            finally
            {
                conn.Close();
                cmd.Dispose();
                if (dr != null) dr.Close();
            }

            return lista;
        }



        //estudiante por id
        public Estudiante ObtenerEstudiantePorId(int idEstudiante)
        {
            Estudiante estudiante = null;
            string sql = "SELECT ID_Estudiante, Nombre, Apellido, Fecha_Nacimiento, DNI, Direccion, Estado_Registro, ID_Genero, ID_Grado, ID_Seccion, ID_Usuario " +
                         "FROM Estudiante WHERE ID_Estudiante = @ID_Estudiante";

            SqlCommand cmd = new SqlCommand(sql, conn);
            {
                cmd.Parameters.AddWithValue("@ID_Estudiante", idEstudiante);

                try
                {
                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            estudiante = new Estudiante
                            {
                                ID_Estudiante = dr.GetInt32(0),
                                Nombre = dr.GetString(1),
                                Apellido = dr.GetString(2),
                                Fecha_Nacimiento = dr.GetDateTime(3),
                                DNI = dr.GetString(4),
                                Direccion = dr.GetString(5),
                                Estado_Registro = dr.GetString(6),
                                ID_Genero = (int)(dr.IsDBNull(7) ? (int?)null : dr.GetInt32(7)),
                                ID_Grado = (int)(dr.IsDBNull(8) ? (int?)null : dr.GetInt32(8)),
                                ID_Seccion = (int)(dr.IsDBNull(9) ? (int?)null : dr.GetInt32(9)),
                                ID_Usuario = (int)(dr.IsDBNull(10) ? (int?)null : dr.GetInt32(10))
                            };
                        }
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error al obtener el estudiante: " + ex.Message);
                }
            }

            return estudiante;
        }

        public void ActualizarEstudiante(int id, string nombre, string apellido, DateTime fecha_Nacimiento, string dni, string direccion)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE Estudiante SET Nombre = @Nombre, Apellido = @Apellido, Fecha_Nacimiento = @Fecha_Nacimiento, DNI = @DNI, Direccion = @Direccion WHERE ID_Estudiante = @ID_Estudiante";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ID_Estudiante", id);
                cmd.Parameters.AddWithValue("@Nombre", nombre);
                cmd.Parameters.AddWithValue("@Apellido", apellido);
                cmd.Parameters.AddWithValue("@Fecha_Nacimiento", fecha_Nacimiento);
                cmd.Parameters.AddWithValue("@DNI", dni);
                cmd.Parameters.AddWithValue("@Direccion", direccion);


                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}