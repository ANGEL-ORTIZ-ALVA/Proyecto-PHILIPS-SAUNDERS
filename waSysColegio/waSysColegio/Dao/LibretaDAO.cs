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
    public class LibretaDAO
    {
        private static readonly string cadena = ConfigurationManager.ConnectionStrings["ColegioBD"].ConnectionString;

        // Método para insertar una nueva libreta
        public string InsertarLibreta(Libreta libreta)
        {
            string mensaje;
            string sql = @"INSERT INTO Libreta (Anio_Escolar, Estado_Registro, ID_Estudiante) 
                           VALUES (@AnioEscolar, @EstadoRegistro, @IDEstudiante)";

            using (SqlConnection cn = new SqlConnection(cadena))
            using (SqlCommand cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@AnioEscolar", libreta.Anio_Escolar);
                cmd.Parameters.AddWithValue("@EstadoRegistro", libreta.Estado_Registro);
                cmd.Parameters.AddWithValue("@IDEstudiante", libreta.ID_Estudiante);

                try
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    mensaje = "Libreta registrada con éxito.";
                }
                catch (Exception ex)
                {
                    mensaje = $"Error al registrar la libreta: {ex.Message}";
                }
            }

            return mensaje;
        }

        // Método para listar todas las libretas registradas
        public DataTable ListarLibretas()
        {
            DataTable dtLibretas = new DataTable();
            string sql = @"SELECT l.ID_Libreta, l.Anio_Escolar, l.Estado_Registro, 
                                  CONCAT(e.Nombre, ' ', e.Apellido) AS NombreCompletoEstudiante
                           FROM Libreta l
                           JOIN Estudiante e ON l.ID_Estudiante = e.ID_Estudiante
                           WHERE l.Estado_Registro = 'Registrado'";

            using (SqlConnection cn = new SqlConnection(cadena))
            using (SqlDataAdapter da = new SqlDataAdapter(sql, cn))
            {
                try
                {
                    cn.Open();
                    da.Fill(dtLibretas);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error al listar libretas: {ex.Message}");
                }
            }

            return dtLibretas;
        }
        public DataTable ListarLibretasFiltradas(string filtroNombre, string anioDesde, string anioHasta)
        {
            DataTable dtLibretas = new DataTable();
            string sql = @"SELECT l.ID_Libreta, l.Anio_Escolar, 
                   CONCAT(e.Nombre, ' ', e.Apellido) AS NombreCompletoEstudiante
                   FROM Libreta l
                   JOIN Estudiante e ON l.ID_Estudiante = e.ID_Estudiante
                   WHERE l.Estado_Registro = 'Registrado'";

            // Agregar filtros dinámicamente
            if (!string.IsNullOrEmpty(filtroNombre))
            {
                sql += " AND (e.Nombre LIKE @filtroNombre OR e.Apellido LIKE @filtroNombre)";
            }
            if (!string.IsNullOrEmpty(anioDesde) && !string.IsNullOrEmpty(anioHasta))
            {
                sql += " AND l.Anio_Escolar BETWEEN @anioDesde AND @anioHasta";
            }
            else if (!string.IsNullOrEmpty(anioDesde))
            {
                sql += " AND l.Anio_Escolar = @anioDesde";
            }

            using (SqlConnection cn = new SqlConnection(cadena))
            using (SqlDataAdapter da = new SqlDataAdapter(sql, cn))
            {
                if (!string.IsNullOrEmpty(filtroNombre))
                {
                    da.SelectCommand.Parameters.AddWithValue("@filtroNombre", "%" + filtroNombre + "%");
                }
                if (!string.IsNullOrEmpty(anioDesde) && !string.IsNullOrEmpty(anioHasta))
                {
                    da.SelectCommand.Parameters.AddWithValue("@anioDesde", anioDesde);
                    da.SelectCommand.Parameters.AddWithValue("@anioHasta", anioHasta);
                }
                else if (!string.IsNullOrEmpty(anioDesde))
                {
                    da.SelectCommand.Parameters.AddWithValue("@anioDesde", anioDesde);
                }

                try
                {
                    cn.Open();
                    da.Fill(dtLibretas);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error al listar libretas: {ex.Message}");
                }
            }

            return dtLibretas;
        }




        // Método para buscar una libreta por su ID
        public DataTable BuscarLibretaPorID(int idLibreta)
        {
            DataTable dtLibreta = new DataTable();
            string sql = @"SELECT l.ID_Libreta, l.Anio_Escolar, l.Estado_Registro, 
                                  l.ID_Estudiante, CONCAT(e.Nombre, ' ', e.Apellido) AS NombreCompletoEstudiante
                           FROM Libreta l
                           JOIN Estudiante e ON l.ID_Estudiante = e.ID_Estudiante
                           WHERE l.ID_Libreta = @idLibreta";

            using (SqlConnection cn = new SqlConnection(cadena))
            using (SqlDataAdapter da = new SqlDataAdapter(sql, cn))
            {
                da.SelectCommand.Parameters.AddWithValue("@idLibreta", idLibreta);

                try
                {
                    cn.Open();
                    da.Fill(dtLibreta);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error al buscar la libreta: {ex.Message}");
                }
            }

            return dtLibreta;
        }

        // Método para actualizar una libreta
        public string ActualizarLibreta(Libreta libreta)
        {
            string mensaje;
            string sql = @"UPDATE Libreta 
                           SET Anio_Escolar = @AnioEscolar, 
                               Estado_Registro = @EstadoRegistro, 
                               ID_Estudiante = @IDEstudiante 
                           WHERE ID_Libreta = @IDLibreta";

            using (SqlConnection cn = new SqlConnection(cadena))
            using (SqlCommand cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@AnioEscolar", libreta.Anio_Escolar);
                cmd.Parameters.AddWithValue("@EstadoRegistro", libreta.Estado_Registro);
                cmd.Parameters.AddWithValue("@IDEstudiante", libreta.ID_Estudiante);
                cmd.Parameters.AddWithValue("@IDLibreta", libreta.ID_Libreta);

                try
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    mensaje = "Libreta actualizada con éxito.";
                }
                catch (Exception ex)
                {
                    mensaje = $"Error al actualizar la libreta: {ex.Message}";
                }
            }

            return mensaje;
        }

        // Método para eliminar lógicamente una libreta
        public void EliminarLibreta(int idLibreta)
        {
            string sql = "UPDATE Libreta SET Estado_Registro = 'Eliminado' WHERE ID_Libreta = @idLibreta";

            using (SqlConnection cn = new SqlConnection(cadena))
            using (SqlCommand cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@idLibreta", idLibreta);

                try
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error al eliminar la libreta: {ex.Message}");
                }
            }
        }
    }
}