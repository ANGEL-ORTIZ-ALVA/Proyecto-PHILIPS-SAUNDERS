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
    public class PersonalDAO
    {
        static String cadena = ConfigurationManager.ConnectionStrings["ColegioBD"].ConnectionString;
        SqlConnection cn = new SqlConnection(cadena);
        SqlConnection conn = new SqlConnection(cadena);

        //listar personal
        public DataTable listadoPersonal()
        {
            SqlDataAdapter daPersonal = new SqlDataAdapter(
                "select ID_Personal, Nombre, Apellido, Fecha_Nacimiento, DNI, Correo, Telefono, Direccion, " +
                "Nombre_Tipo_Personal as 'Tipo Personal', Nombre_Genero as Genero, Numero_Grado as Grado, " +
                "Nombre_Seccion as Seccion, Nombre_Usuario as Usuario " +
                "from Personal p " +
                "left join Tipo_Personal tp on tp.ID_Tipo_Personal = p.ID_Tipo_Personal " +
                "join Genero ge on ge.ID_Genero = p.ID_Genero " +
                "join Grado gr on gr.ID_Grado = p.ID_Grado " +
                "join Seccion s on s.ID_Seccion = p.ID_Seccion " +
                "join Usuario u on u.ID_Usuario = p.ID_Usuario " +
                "where p.Estado_Registro = 'Registrado'", conn);

            DataTable dtPersonal = new DataTable();
            daPersonal.Fill(dtPersonal);
            return dtPersonal;
        }

        //Configurar el Buscar Personal a que solo busque los que tienen el estado de registro como Registrado
        public DataTable buscarPersonal(string pNombre)
        {
            SqlDataAdapter daPersonal = new SqlDataAdapter("select ID_Personal, Nombre, Apellido, Fecha_Nacimiento, " +
                "DNI, Correo, Telefono, Direccion, Nombre_Tipo_Personal as 'Tipo Personal', Nombre_Genero as Genero, " +
                "Numero_Grado as Grado, Nombre_Seccion as Seccion, Nombre_Usuario as Usuario from Personal p left join " +
                "Tipo_Personal tp on tp.ID_Tipo_Personal = p.ID_Tipo_Personal join Genero ge on ge.ID_Genero = p.ID_Genero " +
                "join Grado gr on gr.ID_Grado = p.ID_Grado join Seccion s on s.ID_Seccion = p.ID_Seccion join Usuario u on " +
                "u.ID_Usuario = p.ID_Usuario where Nombre = @nombre", conn);
            daPersonal.SelectCommand.Parameters.AddWithValue("@nombre", pNombre);
            DataTable dtPersonal = new DataTable();
            daPersonal.Fill(dtPersonal);
            return dtPersonal;
        }

        public void actualizarPersonal(Personal personal)
        {
            SqlTransaction transaction = null;
            try
            {
                conn.Open();
                transaction = conn.BeginTransaction();

                // Verificar si el usuario ya está asignado a otro personal
                string verificarUsuarioSql = "SELECT ID_Personal FROM Personal WHERE ID_Usuario = @idUsuario AND ID_Personal != @idPersonal";
                SqlCommand verificarUsuarioCmd = new SqlCommand(verificarUsuarioSql, conn, transaction);
                verificarUsuarioCmd.Parameters.AddWithValue("@idUsuario", personal.ID_Usuario);
                verificarUsuarioCmd.Parameters.AddWithValue("@idPersonal", personal.ID_Personal);

                object otroPersonalId = verificarUsuarioCmd.ExecuteScalar();

                if (otroPersonalId != null) // El usuario está asignado a otro personal
                {
                    // Obtener el ID_Usuario del personal actual (para intercambiar)
                    string obtenerUsuarioActualSql = "SELECT ID_Usuario FROM Personal WHERE ID_Personal = @idPersonal";
                    SqlCommand obtenerUsuarioActualCmd = new SqlCommand(obtenerUsuarioActualSql, conn, transaction);
                    obtenerUsuarioActualCmd.Parameters.AddWithValue("@idPersonal", personal.ID_Personal);

                    int usuarioActual = (int)obtenerUsuarioActualCmd.ExecuteScalar();

                    // Intercambiar los usuarios
                    string intercambiarUsuariosSql = "UPDATE Personal SET ID_Usuario = @usuarioActual WHERE ID_Personal = @otroPersonalId";
                    SqlCommand intercambiarUsuariosCmd = new SqlCommand(intercambiarUsuariosSql, conn, transaction);
                    intercambiarUsuariosCmd.Parameters.AddWithValue("@usuarioActual", usuarioActual);
                    intercambiarUsuariosCmd.Parameters.AddWithValue("@otroPersonalId", otroPersonalId);

                    intercambiarUsuariosCmd.ExecuteNonQuery();
                }

                // Actualizar el personal con el nuevo usuario y otros datos
                string actualizarPersonalSql = @"UPDATE Personal 
                                         SET Nombre = @nombre, Apellido = @apellido, Fecha_Nacimiento = @fechaNacimiento, 
                                             DNI = @dni, Correo = @correo, Telefono = @telefono, Direccion = @direccion, 
                                             ID_Tipo_Personal = @idTipoPersonal, ID_Genero = @idGenero, 
                                             ID_Grado = @idGrado, ID_Seccion = @idSeccion, ID_Usuario = @idUsuario
                                         WHERE ID_Personal = @idPersonal";

                SqlCommand actualizarPersonalCmd = new SqlCommand(actualizarPersonalSql, conn, transaction);
                actualizarPersonalCmd.Parameters.AddWithValue("@idPersonal", personal.ID_Personal);
                actualizarPersonalCmd.Parameters.AddWithValue("@nombre", personal.Nombre);
                actualizarPersonalCmd.Parameters.AddWithValue("@apellido", personal.Apellido);
                actualizarPersonalCmd.Parameters.AddWithValue("@fechaNacimiento", personal.Fecha_Nacimiento);
                actualizarPersonalCmd.Parameters.AddWithValue("@dni", personal.DNI);
                actualizarPersonalCmd.Parameters.AddWithValue("@correo", personal.Correo);
                actualizarPersonalCmd.Parameters.AddWithValue("@telefono", personal.Telefono);
                actualizarPersonalCmd.Parameters.AddWithValue("@direccion", personal.Direccion);
                actualizarPersonalCmd.Parameters.AddWithValue("@idTipoPersonal", personal.ID_Tipo_Personal);
                actualizarPersonalCmd.Parameters.AddWithValue("@idGenero", personal.ID_Genero);
                actualizarPersonalCmd.Parameters.AddWithValue("@idGrado", personal.ID_Grado);
                actualizarPersonalCmd.Parameters.AddWithValue("@idSeccion", personal.ID_Seccion);
                actualizarPersonalCmd.Parameters.AddWithValue("@idUsuario", personal.ID_Usuario);

                actualizarPersonalCmd.ExecuteNonQuery();

                // Commit de la transacción
                transaction.Commit();
            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback(); // Revertir los cambios si hay error
                }
                throw new Exception("Error al actualizar el personal: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }


        //Insertar personal
        public String insertar(Personal personal)
        {
            string mensaje = null;

            // Comando para insertar Personal
            string sqlPersonal = "insert into Personal values(@nombre, @apellido, @fechaNacimiento, @dni, @correo, " +
                "@telefono, @direccion, @estadoRegistro, @idTipoPersonal, @idGenero, @idGrado, @idSeccion, @idUsuario)";
            SqlCommand cmdPersonal = new SqlCommand(sqlPersonal, cn);
            cmdPersonal.Parameters.AddWithValue("@nombre", personal.Nombre);
            cmdPersonal.Parameters.AddWithValue("@apellido", personal.Apellido);
            cmdPersonal.Parameters.AddWithValue("@fechaNacimiento", personal.Fecha_Nacimiento);
            cmdPersonal.Parameters.AddWithValue("@dni", personal.DNI);
            cmdPersonal.Parameters.AddWithValue("@correo", personal.Correo);
            cmdPersonal.Parameters.AddWithValue("@telefono", personal.Telefono);
            cmdPersonal.Parameters.AddWithValue("@direccion", personal.Direccion);
            cmdPersonal.Parameters.AddWithValue("@estadoRegistro", "Registrado");
            cmdPersonal.Parameters.AddWithValue("@idTipoPersonal", personal.ID_Tipo_Personal);
            cmdPersonal.Parameters.AddWithValue("@idGenero", personal.ID_Genero);
            cmdPersonal.Parameters.AddWithValue("@idGrado", personal.ID_Grado);
            cmdPersonal.Parameters.AddWithValue("@idSeccion", personal.ID_Seccion);
            cmdPersonal.Parameters.AddWithValue("@idUsuario", personal.ID_Usuario);


            try
            {
                cn.Open();

                cmdPersonal.ExecuteNonQuery();

                // Ejecutar la inserción de Personal
                mensaje = "Personal registrado :)";

            }
            catch (Exception ex)
            {
                mensaje = ">:( Error al registrar Personal: " + ex.Message;
            }
            finally
            {
                cn.Close();
                cmdPersonal.Dispose();
            }
            return mensaje;
        }

        //Buscar personal por id
        public DataTable buscarPersonalPorID(int idPersonal)
        {
            SqlDataAdapter daPersonal = new SqlDataAdapter("select * from Personal where ID_Personal = @idPersonal", cn);
            daPersonal.SelectCommand.Parameters.AddWithValue("@idPersonal", idPersonal);
            DataTable dtPersonal = new DataTable();
            daPersonal.Fill(dtPersonal);
            return dtPersonal;
        }

        
        //Eliminar personal
        public void eliminarPersonal(int idPersonal)
        {
            // Cambiar el estado de registro a "Eliminado"
            string sql = "UPDATE Personal SET Estado_Registro = 'Eliminado' WHERE ID_Personal = @idPersonal";
            SqlCommand cmd = new SqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@idPersonal", idPersonal);

            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cambiar el estado del personal: " + ex.Message);
            }
            finally
            {
                cn.Close();
                cmd.Dispose();
            }
        }

        public void eliminarTipoPersonal(int idTipoPersonal)
        {
            string sql = "UPDATE Tipo_Personal SET Estado_Registro = 'Eliminado' WHERE ID_Tipo_Personal = @idTipoPersonal";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@idTipoPersonal", idTipoPersonal);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cambiar el estado del tipo de personal: " + ex.Message);
            }
            finally
            {
                conn.Close();
                cmd.Dispose();
            }
        }

        public DataTable listarPersonalParaLibreta()
        {
            SqlDataAdapter daPersonal = new SqlDataAdapter(
                "SELECT ID_Personal, Nombre " +
                "FROM Personal p " +
                "JOIN Tipo_Personal tp ON tp.ID_Tipo_Personal = p.ID_Tipo_Personal " +
                "WHERE tp.Nombre_Tipo_Personal IN ('Director', 'Tutor', 'Docente') " +
                "AND p.Estado_Registro = 'Registrado'", conn);

            DataTable dtPersonal = new DataTable();
            daPersonal.Fill(dtPersonal);
            return dtPersonal;
        }

        public DataTable listarPersonalFiltrado(string filtroNombre, string tipoPersonal, string seccion, string grado, string genero)
        {
            string sql = @"SELECT ID_Personal, Nombre, Apellido, Fecha_Nacimiento, DNI, Correo, Telefono, Direccion,
                  Nombre_Tipo_Personal as 'Tipo Personal', Nombre_Genero as Genero, Numero_Grado as Grado,
                  Nombre_Seccion as Seccion, Nombre_Usuario as Usuario
           FROM Personal p
           LEFT JOIN Tipo_Personal tp ON tp.ID_Tipo_Personal = p.ID_Tipo_Personal
           JOIN Genero ge ON ge.ID_Genero = p.ID_Genero
           JOIN Grado gr ON gr.ID_Grado = p.ID_Grado
           JOIN Seccion s ON s.ID_Seccion = p.ID_Seccion
           JOIN Usuario u ON u.ID_Usuario = p.ID_Usuario
           WHERE p.Estado_Registro = 'Registrado'";

            // Agregar filtros dinámicamente
            if (!string.IsNullOrEmpty(filtroNombre))
            {
                sql += " AND (p.Nombre LIKE @filtroNombre OR p.Apellido LIKE @filtroNombre)";
            }
            if (!string.IsNullOrEmpty(tipoPersonal))
            {
                sql += " AND tp.ID_Tipo_Personal = @tipoPersonal";
            }
            if (!string.IsNullOrEmpty(seccion))
            {
                sql += " AND s.ID_Seccion = @seccion";
            }
            if (!string.IsNullOrEmpty(grado))
            {
                sql += " AND gr.ID_Grado = @grado";
            }
            if (!string.IsNullOrEmpty(genero))
            {
                sql += " AND ge.ID_Genero = @genero";
            }

            SqlDataAdapter daPersonal = new SqlDataAdapter(sql, conn);

            // Asignar parámetros
            if (!string.IsNullOrEmpty(filtroNombre))
            {
                daPersonal.SelectCommand.Parameters.AddWithValue("@filtroNombre", "%" + filtroNombre + "%");
            }
            if (!string.IsNullOrEmpty(tipoPersonal))
            {
                daPersonal.SelectCommand.Parameters.AddWithValue("@tipoPersonal", tipoPersonal);
            }
            if (!string.IsNullOrEmpty(seccion))
            {
                daPersonal.SelectCommand.Parameters.AddWithValue("@seccion", seccion);
            }
            if (!string.IsNullOrEmpty(grado))
            {
                daPersonal.SelectCommand.Parameters.AddWithValue("@grado", grado);
            }
            if (!string.IsNullOrEmpty(genero))
            {
                daPersonal.SelectCommand.Parameters.AddWithValue("@genero", genero);
            }

            DataTable dtPersonal = new DataTable();
            daPersonal.Fill(dtPersonal);
            return dtPersonal;
        }

        public DataTable ListarTiposPersonal()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT ID_Tipo_Personal, Nombre_Tipo_Personal FROM Tipo_Personal WHERE Estado_Registro = 'Registrado'", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

    }
}