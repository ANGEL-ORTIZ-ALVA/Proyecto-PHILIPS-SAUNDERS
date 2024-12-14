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
    public class TipoPersonalDAO
    {
        static String cadena = ConfigurationManager.ConnectionStrings["colegioBD"].ConnectionString;//conectando
        SqlConnection cn = new SqlConnection(cadena);

        // Listar Tipo Personal
        public DataTable listarTipopersonal()
        {
            SqlDataAdapter da = new SqlDataAdapter(
                "SELECT * FROM Tipo_Personal WHERE Estado_Registro = 'Registrado'",
                cn
            );
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public String insertarTipoPersonal(TipoPersonal tipo_Personal)
        {
            string mensaje = null;

            // Comando para insertar Tipo_Personal
            string sqlTipoPersonal = "INSERT INTO Tipo_Personal (Nombre_Tipo_Personal, Descripcion, Estado_Registro) VALUES (@Nombre_Tipo_Personal, @Descripcion, @Estado_Registro)";
            SqlCommand cmdTipoPersonal = new SqlCommand(sqlTipoPersonal, cn);
            cmdTipoPersonal.Parameters.AddWithValue("@Nombre_Tipo_Personal", tipo_Personal.Nombre_Tipo_Personal);
            cmdTipoPersonal.Parameters.AddWithValue("@Descripcion", tipo_Personal.Descripcion);
            cmdTipoPersonal.Parameters.AddWithValue("@Estado_Registro", "Registrado");

            try
            {
                cn.Open();
                cmdTipoPersonal.ExecuteNonQuery();
                mensaje = "Tipo Personal registrado :)";
            }
            catch (Exception ex)
            {
                mensaje = ">:( Error al registrar Tipo Personal: " + ex.Message;
            }
            finally
            {
                cn.Close();
                cmdTipoPersonal.Dispose();
            }
            return mensaje;
        }

        // Método para actualizar un Tipo_Personal
        public String actualizarTipoPersonal(TipoPersonal tipoPersonal)
        {
            string mensaje = null;

            string sql = "UPDATE Tipo_Personal SET Nombre_Tipo_Personal = @Nombre_Tipo_Personal, Descripcion = @Descripcion WHERE ID_Tipo_Personal = @ID_Tipo_Personal";
            SqlCommand cmd = new SqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@ID_Tipo_Personal", tipoPersonal.ID_Tipo_Personal);
            cmd.Parameters.AddWithValue("@Nombre_Tipo_Personal", tipoPersonal.Nombre_Tipo_Personal);
            cmd.Parameters.AddWithValue("@Descripcion", tipoPersonal.Descripcion);

            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                mensaje = "Tipo Personal actualizado correctamente :)";
            }
            catch (Exception ex)
            {
                mensaje = ">:( Error al actualizar Tipo Personal: " + ex.Message;
            }
            finally
            {
                cn.Close();
                cmd.Dispose();
            }

            return mensaje;
        }

        public DataTable buscarTipoPersonalPorID(int idTipoPersonal)
        {
            SqlDataAdapter daTipoPersonal = new SqlDataAdapter(
                "SELECT * FROM Tipo_Personal WHERE ID_Tipo_Personal = @idTipoPersonal AND Estado_Registro = 'Registrado'", cn);
            daTipoPersonal.SelectCommand.Parameters.AddWithValue("@idTipoPersonal", idTipoPersonal);
            DataTable dtTipoPersonal = new DataTable();
            daTipoPersonal.Fill(dtTipoPersonal);
            return dtTipoPersonal;
        }

    }
}