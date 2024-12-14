using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace waSysColegio.Dao
{
    public class ApoderadoDAO
    {
        static String connectionString = ConfigurationManager.ConnectionStrings["colegioBD"].ConnectionString;
        SqlConnection aes = new SqlConnection(connectionString);


        //Listar apoderados
        public DataTable listarApoderados()
        {
            DataTable dt = new DataTable();
            string query = @"SELECT a.ID_Apoderado, a.Nombre, a.Apellido, a.DNI, a.Correo, a.Telefono, a.Direccion, a.Estado_Registro, 
                            g.Nombre_Genero, u.Nombre_Usuario
                            FROM Apoderado a
                            JOIN Genero g ON a.ID_Genero = g.ID_Genero
                            JOIN Usuario u ON a.ID_Usuario = u.ID_Usuario";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.Fill(dt);
            }

            return dt;
        }
        //listaApoderado
        public DataTable listaApoderado()
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("select * from Apoderado;", aes);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la lista de apoderados: " + ex.Message);
            }
        }

        //Agregar apoderados
        public void agregarApoderado(string nombre, string apellido, string dni, string correo, string telefono, string direccion, string estadoRegistro, string genero, string nombreUsuario)
        {
            try
            {
                //string connString = ConfigurationManager.ConnectionStrings["colegioBD"].ToString();

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string getUserQuery = "SELECT ID_Usuario FROM Usuario WHERE Nombre_Usuario = @Nombre_Usuario";
                    SqlCommand getUserCmd = new SqlCommand(getUserQuery, conn);
                    getUserCmd.Parameters.AddWithValue("@Nombre_Usuario", nombreUsuario);
                    object result = getUserCmd.ExecuteScalar();

                    if (result == null)
                    {
                        throw new Exception("No se encontró ningún usuario con el nombre de usuario proporcionado.");
                    }
                    else
                    {
                        int idUsuario = Convert.ToInt32(result);

                        string query = @"
                    INSERT INTO Apoderado (Nombre, Apellido, DNI, Correo, Telefono, Direccion, Estado_Registro, ID_Genero, ID_Usuario)
                    VALUES (@Nombre, @Apellido, @DNI, @Correo, @Telefono, @Direccion, @Estado_Registro, @ID_Genero, @ID_Usuario)
                ";

                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@Nombre", nombre);
                        cmd.Parameters.AddWithValue("@Apellido", apellido);
                        cmd.Parameters.AddWithValue("@DNI", dni);
                        cmd.Parameters.AddWithValue("@Correo", correo);
                        cmd.Parameters.AddWithValue("@Telefono", telefono);
                        cmd.Parameters.AddWithValue("@Direccion", direccion);
                        cmd.Parameters.AddWithValue("@Estado_Registro", estadoRegistro);
                        cmd.Parameters.AddWithValue("@ID_Genero", genero);
                        cmd.Parameters.AddWithValue("@ID_Usuario", idUsuario);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar el apoderado: " + ex.Message);
            }
        }

        //Obtener apoderado por id
        public DataRow obtenerApoderadoPorID(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Apoderado WHERE ID_Apoderado = @ID_Apoderado";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@ID_Apoderado", id);
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

        //Actualizar apoderado
        public void actualizarApoderado(int id, string nombre, string apellido, string dni, string correo, string telefono, string direccion)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE Apoderado SET Nombre = @Nombre, Apellido = @Apellido, DNI = @DNI, Correo = @Correo, Telefono = @Telefono, Direccion = @Direccion WHERE ID_Apoderado = @ID_Apoderado";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ID_Apoderado", id);
                cmd.Parameters.AddWithValue("@Nombre", nombre);
                cmd.Parameters.AddWithValue("@Apellido", apellido);
                cmd.Parameters.AddWithValue("@DNI", dni);
                cmd.Parameters.AddWithValue("@Correo", correo);
                cmd.Parameters.AddWithValue("@Telefono", telefono);
                cmd.Parameters.AddWithValue("@Direccion", direccion);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        //Eliminar apoderado
        public void eliminarApoderado(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Apoderado WHERE ID_Apoderado = @ID_Apoderado";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ID_Apoderado", id);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }

}