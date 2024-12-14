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
    public class DetalleLibretaDAO
    {
        private static string cadena = ConfigurationManager.ConnectionStrings["ColegioBD"].ConnectionString;
        private SqlConnection cn = new SqlConnection(cadena);


        // Método para insertar un nuevo Detalle_Libreta
        public string InsertarDetalleLibreta(Detalle_Libreta detalleLibreta)
        {
            string mensaje = null;
            string sql = "INSERT INTO Detalle_Libreta (ID_Libreta, ID_Personal, Firma, Sello, Estado_Registro) " +
                         "VALUES (@ID_Libreta, @ID_Personal, @Firma, @Sello, @Estado_Registro)";
            SqlCommand cmd = new SqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@ID_Libreta", detalleLibreta.ID_Libreta);
            cmd.Parameters.AddWithValue("@ID_Personal", detalleLibreta.ID_Personal);
            cmd.Parameters.AddWithValue("@Firma", detalleLibreta.Firma ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Sello", detalleLibreta.Sello ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Estado_Registro", detalleLibreta.Estado_Registro);

            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                mensaje = "Detalle de Libreta registrado con éxito.";
            }
            catch (Exception ex)
            {
                mensaje = "Error al registrar el detalle de libreta: " + ex.Message;
            }
            finally
            {
                cn.Close();
                cmd.Dispose();
            }

            return mensaje;
        }

        // Método para listar todos los Detalle_Libreta
        public DataTable ListarDetallesLibreta()
        {
            DataTable dtDetallesLibreta = new DataTable();
            string sql = @"SELECT dl.ID_Libreta, dl.ID_Personal, p.Nombre + ' ' + p.Apellido AS NombrePersonal, 
                          dl.Firma, dl.Sello, dl.Estado_Registro
                   FROM Detalle_Libreta dl
                   JOIN Personal p ON dl.ID_Personal = p.ID_Personal
                   WHERE dl.Estado_Registro = 'Registrado'";

            SqlDataAdapter da = new SqlDataAdapter(sql, cn);
            try
            {
                cn.Open();
                da.Fill(dtDetallesLibreta);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar los detalles de libreta: " + ex.Message);
            }
            finally
            {
                cn.Close();
                da.Dispose();
            }

            return dtDetallesLibreta;
        }

        public DataTable ListarDetallesLibretaPorLibreta(int idLibreta)
        {
            DataTable dtDetallesLibreta = new DataTable();
            string sql = @"SELECT dl.ID_Libreta, dl.ID_Personal, p.Nombre AS NombrePersonal, dl.Firma, dl.Sello, dl.Estado_Registro
                   FROM Detalle_Libreta dl
                   JOIN Personal p ON dl.ID_Personal = p.ID_Personal
                   WHERE dl.ID_Libreta = @ID_Libreta AND dl.Estado_Registro = 'Registrado'";

            SqlDataAdapter da = new SqlDataAdapter(sql, cn);
            da.SelectCommand.Parameters.AddWithValue("@ID_Libreta", idLibreta);

            try
            {
                cn.Open();
                da.Fill(dtDetallesLibreta);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar los detalles de la libreta: " + ex.Message);
            }
            finally
            {
                cn.Close();
                da.Dispose();
            }

            return dtDetallesLibreta;
        }

        // Método para actualizar un Detalle_Libreta existente
        public string ActualizarDetalleLibreta(Detalle_Libreta detalleLibreta, int idPersonalOriginal)
        {
            string mensaje = null;
            string sql = "UPDATE Detalle_Libreta SET ID_Personal = @ID_Personal, Firma = @Firma, Sello = @Sello, Estado_Registro = @Estado_Registro " +
                         "WHERE ID_Libreta = @ID_Libreta AND ID_Personal = @ID_PersonalOriginal";
            SqlCommand cmd = new SqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@ID_Personal", detalleLibreta.ID_Personal);
            cmd.Parameters.AddWithValue("@Firma", detalleLibreta.Firma ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Sello", detalleLibreta.Sello ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Estado_Registro", detalleLibreta.Estado_Registro);
            cmd.Parameters.AddWithValue("@ID_Libreta", detalleLibreta.ID_Libreta);
            cmd.Parameters.AddWithValue("@ID_PersonalOriginal", idPersonalOriginal);

            try
            {
                cn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                mensaje = rowsAffected > 0 ? "Detalle de libreta actualizado con éxito." : "No se pudo actualizar el detalle de libreta.";
            }
            catch (Exception ex)
            {
                mensaje = "Error al actualizar el detalle de libreta: " + ex.Message;
            }
            finally
            {
                cn.Close();
                cmd.Dispose();
            }

            return mensaje;
        }




        // Método para eliminar (lógicamente) un Detalle_Libreta
        public void EliminarDetalleLibreta(int idLibreta, int idPersonal)
        {
            string sql = "UPDATE Detalle_Libreta SET Estado_Registro = 'Eliminado' WHERE ID_Libreta = @ID_Libreta AND ID_Personal = @ID_Personal";
            SqlCommand cmd = new SqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@ID_Libreta", idLibreta);
            cmd.Parameters.AddWithValue("@ID_Personal", idPersonal);

            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el detalle de libreta: " + ex.Message);
            }
            finally
            {
                cn.Close();
                cmd.Dispose();
            }
        }

        // Método para buscar un Detalle_Libreta por su ID
        public DataTable BuscarDetalleLibretaPorID(int idLibreta, int idPersonal)
        {
            DataTable dtDetalleLibreta = new DataTable();
            string sql = @"SELECT dl.ID_Libreta, dl.ID_Personal, p.Nombre AS NombrePersonal, dl.Firma, dl.Sello, dl.Estado_Registro
                           FROM Detalle_Libreta dl
                           JOIN Personal p ON dl.ID_Personal = p.ID_Personal
                           WHERE dl.ID_Libreta = @ID_Libreta AND dl.ID_Personal = @ID_Personal";

            SqlDataAdapter da = new SqlDataAdapter(sql, cn);
            da.SelectCommand.Parameters.AddWithValue("@ID_Libreta", idLibreta);
            da.SelectCommand.Parameters.AddWithValue("@ID_Personal", idPersonal);

            try
            {
                cn.Open();
                da.Fill(dtDetalleLibreta);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar el detalle de libreta: " + ex.Message);
            }
            finally
            {
                cn.Close();
                da.Dispose();
            }

            return dtDetalleLibreta;
        }
    }
}