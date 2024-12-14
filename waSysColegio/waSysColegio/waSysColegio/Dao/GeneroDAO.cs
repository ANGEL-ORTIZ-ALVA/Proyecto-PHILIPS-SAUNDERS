using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace waSysColegio.Dao
{
    public class GeneroDAO
    {
        static String connectionString = ConfigurationManager.ConnectionStrings["ColegioBD"].ConnectionString;
        SqlConnection cn = new SqlConnection(connectionString);

         public DataTable ListarGeneros()
        {
            DataTable dt = new DataTable();
            string query = "SELECT ID_Genero, Nombre_Genero, Estado_Registro FROM Genero";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.Fill(dt);
            }

            return dt;
        }

        public void AgregarGenero(string nombreGenero, string estadoRegistro)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Genero (Nombre_Genero, Estado_Registro) VALUES (@Nombre_Genero, @Estado_Registro)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Nombre_Genero", nombreGenero);
                cmd.Parameters.AddWithValue("@Estado_Registro", estadoRegistro);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public DataRow ObtenerGeneroPorID(int idGenero)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Genero WHERE ID_Genero = @ID_Genero";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@ID_Genero", idGenero);
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

        public void ActualizarGenero(int idGenero, string nombreGenero, string estadoRegistro)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE Genero SET Nombre_Genero = @Nombre_Genero, Estado_Registro = @Estado_Registro WHERE ID_Genero = @ID_Genero";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ID_Genero", idGenero);
                cmd.Parameters.AddWithValue("@Nombre_Genero", nombreGenero);
                cmd.Parameters.AddWithValue("@Estado_Registro", estadoRegistro);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public void EliminarGenero(int idGenero)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Genero WHERE ID_Genero = @ID_Genero";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ID_Genero", idGenero);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

    }
}