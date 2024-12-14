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
    public class UsuarioDAO
    {
        static string cadena = ConfigurationManager.ConnectionStrings["colegioBD"].ConnectionString;
        SqlConnection cn = new SqlConnection(cadena);

        //Lista de usuarios
        public DataTable listarUsuario()
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from Usuario", cn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        //Obtener usuarios sin asignar
        public DataTable listarUsuariosSinAsignar()
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT u.ID_Usuario, u.Nombre_Usuario 
                   FROM Usuario u
                   LEFT JOIN Personal p ON u.ID_Usuario = p.ID_Usuario
                   WHERE p.ID_Usuario IS NULL and ID_Rol != 5 and ID_Rol != 6";
            SqlCommand cmd = new SqlCommand(sql, cn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            return dt;
        }

        // Obtener usuario por ID
        public DataTable obtenerUsuarioPorID(int id)
        {
            string mensaje = null;
            string sql = "select * from Usuario where ID_Usuario = @id";
            SqlCommand cmd = new SqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@id", id);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            try
            {
                cn.Open();
                da.Fill(dt);
            }
            catch (SqlException ex)
            {
                mensaje = "Error en obtener usuario por id: " + ex.Message;
            }
            finally
            {
                cn.Close();
                cmd.Dispose();
            }
            return dt;
        }

        // Obtener usuario por nombre de usuario
        public DataTable obtenerUsuarioPorNombreUsuario(string nombreUsuario)
        {
            string mensaje = null;
            string sql = "select * from Usuario where Nombre_Usuario = @nom";
            SqlCommand cmd = new SqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@nom", nombreUsuario);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            try
            {
                cn.Open();
                da.Fill(dt);
            }
            catch (SqlException ex)
            {
                mensaje = "Error en obtener usuario por el username: " + ex.Message;
            }
            finally
            {
                cn.Close();
                cmd.Dispose();
            }
            return dt;
        }

        //insertar
        public string insertar(Usuario obj)
        {
            string mensaje = null;
            string sql = "insert into Usuario (Nombre_Usuario, Password, Fecha_Creacion, Ultimo_Acceso, Estado_Registro, ID_Estado_Usuario, ID_Rol) values (@nom, @pass, @fec, @ult, @est, @idest, @idrol)";
            SqlCommand cmd = new SqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@nom", obj.Nombre_Usuario);
            cmd.Parameters.AddWithValue("@pass", obj.Password);
            cmd.Parameters.AddWithValue("@fec", obj.Fecha_Creacion);
            cmd.Parameters.AddWithValue("@ult", obj.Ultimo_Acceso);
            cmd.Parameters.AddWithValue("@est", obj.Estado_Registro);
            cmd.Parameters.AddWithValue("@idest", obj.ID_Estado_Usuario);
            cmd.Parameters.AddWithValue("@idrol", obj.ID_Rol);

            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                mensaje = "Usuario registrado";
            }
            catch (SqlException ex)
            {
                mensaje = "Error en insertar: " + ex.Message;
            }
            finally
            {
                cn.Close();
                cmd.Dispose();
            }
            return mensaje;
        }

        /*========== 
         Metodos adicionales 
         (se usaran en una posteriodiad para el mantenimiento de usuarios) 
         * ==========*/

        // Obtener todos los usuarios
        public DataTable obtenerUsuarios()
        {
            string mensaje = null;
            string sql = "select * from Usuario";
            SqlCommand cmd = new SqlCommand(sql, cn);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            try
            {
                cn.Open();
                da.Fill(dt);
            }
            catch (SqlException ex)
            {
                mensaje = "Error en insertar: " + ex.Message;
            }
            finally
            {
                cn.Close();
                cmd.Dispose();
            }
            return dt;
        }


        // Actualizar
        public string actualizar(Usuario obj)
        {
            string mensaje = null;
            string sql = "update Usuario set Nombre_Usuario = @nom, Password = @pass, Fecha_Creacion = @fec, Ultimo_Acceso = @ult, Estado_Registro = @est, ID_Estado_Usuario = @idest, ID_Rol = @idrol where ID_Usuario = @id";
            SqlCommand cmd = new SqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@id", obj.ID_Usuario);
            cmd.Parameters.AddWithValue("@nom", obj.Nombre_Usuario);
            cmd.Parameters.AddWithValue("@pass", obj.Password);
            cmd.Parameters.AddWithValue("@fec", obj.Fecha_Creacion);
            cmd.Parameters.AddWithValue("@ult", obj.Ultimo_Acceso);
            cmd.Parameters.AddWithValue("@est", obj.Estado_Registro);
            cmd.Parameters.AddWithValue("@idest", obj.ID_Estado_Usuario);
            cmd.Parameters.AddWithValue("@idrol", obj.ID_Rol);

            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                mensaje = "Usuario actualizado";
            }
            catch (SqlException ex)
            {
                mensaje = "Error en actualizar: " + ex.Message;
            }
            finally
            {
                cn.Close();
                cmd.Dispose();
            }
            return mensaje;
        }
        // Eliminar
        public string eliminar(int id)
        {
            string mensaje = null;
            string sql = "delete from Usuario where ID_Usuario = @id";
            SqlCommand cmd = new SqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@id", id);

            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                mensaje = "Usuario eliminado";
            }
            catch (SqlException ex)
            {
                mensaje = "Error en eliminar: " + ex.Message;
            }
            finally
            {
                cn.Close();
                cmd.Dispose();
            }
            return mensaje;
        }
    }
}