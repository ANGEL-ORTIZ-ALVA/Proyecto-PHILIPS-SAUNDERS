using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace waSysColegio.Pages
{
    public partial class VerDetalleCurso : System.Web.UI.Page
    {
        //SqlConnection con = new SqlConnection("cn");
        static String cadena = ConfigurationManager.ConnectionStrings["colegioBD"].ConnectionString;
        SqlConnection con = new SqlConnection(cadena);
        protected void Page_Load(object sender, EventArgs e)
        {
            // Verifica si el usuario está autenticado
            if (!User.Identity.IsAuthenticated)
            {
                // Si no está autenticado, redirigir a la página de login
                Response.Redirect("~/Login.aspx");
            }

            if (!IsPostBack)
            {
                CargarDetalleCurso(); // Cargar todos los registros al cargar la página
            }
        }
        private void CargarDetalleCurso(string filtro = "")
        {
            // Consulta SQL con filtro opcional
            string query = @"SELECT
                    d.ID_Estudiante,
                    d.ID_Curso,
                    d.ID_Evaluacion,          -- Agregar ID_Evaluacion
                    d.ID_Asistencia,          -- Agregar ID_Asistencia
                    d.ID_Periodo,             -- Agregar ID_Periodo
                    (es.Nombre + ' ' + es.Apellido) AS NombreCompleto,
                    c.Nombre_Curso,
                    e.Nombre_Evaluacion,
                    a.Nombre_Tipo_Asistencia,
                    p.Nombre_Periodo,
                    d.Nota,
                    d.Estado_Registro
                 FROM Detalle_Curso d
                 INNER JOIN Estudiante es ON d.ID_Estudiante = es.ID_Estudiante
                 INNER JOIN Curso c ON d.ID_Curso = c.ID_Curso
                 INNER JOIN Evaluacion e ON d.ID_Evaluacion = e.ID_Evaluacion
                 INNER JOIN Asistencia a ON d.ID_Asistencia = a.ID_Asistencia
                 INNER JOIN Periodo p ON d.ID_Periodo = p.ID_Periodo";

            if (!string.IsNullOrEmpty(filtro))
            {
                query += " WHERE es.Nombre LIKE @Filtro OR c.Nombre_Curso LIKE @Filtro OR e.Nombre_Evaluacion LIKE @Filtro OR a.Nombre_Tipo_Asistencia LIKE @Filtro";
            }

            SqlCommand cmd = new SqlCommand(query, con);
            if (!string.IsNullOrEmpty(filtro))
            {
                cmd.Parameters.AddWithValue("@Filtro", "%" + filtro + "%");
            }

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            gvDetalleCurso.DataSource = dt;
            gvDetalleCurso.DataBind();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string filtro = txtBuscar.Text.Trim();
            CargarDetalleCurso(filtro); // Cargar registros filtrados
        }

        protected void gvDetalleCurso_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            // Obtener los valores de las claves compuestas
            int idEstudiante = Convert.ToInt32(gvDetalleCurso.DataKeys[e.RowIndex].Values["ID_Estudiante"].ToString());
            int idCurso = Convert.ToInt32(gvDetalleCurso.DataKeys[e.RowIndex].Values["ID_Curso"].ToString());
            int idEvaluacion = Convert.ToInt32(gvDetalleCurso.DataKeys[e.RowIndex].Values["ID_Evaluacion"].ToString());
            int idAsistencia = Convert.ToInt32(gvDetalleCurso.DataKeys[e.RowIndex].Values["ID_Asistencia"].ToString());
            int idPeriodo = Convert.ToInt32(gvDetalleCurso.DataKeys[e.RowIndex].Values["ID_Periodo"].ToString());

            // Consulta SQL para borrar utilizando todas las claves
            SqlCommand cmd = new SqlCommand(@"DELETE FROM Detalle_Curso 
                                      WHERE ID_Estudiante = @ID_Estudiante
                                      AND ID_Curso = @ID_Curso 
                                      AND ID_Evaluacion = @ID_Evaluacion 
                                      AND ID_Asistencia = @ID_Asistencia 
                                      AND ID_Periodo = @ID_Periodo", con);

            // Asignar los parámetros
            cmd.Parameters.AddWithValue("@ID_Estudiante", idEstudiante);
            cmd.Parameters.AddWithValue("@ID_Curso", idCurso);
            cmd.Parameters.AddWithValue("@ID_Evaluacion", idEvaluacion);
            cmd.Parameters.AddWithValue("@ID_Asistencia", idAsistencia);
            cmd.Parameters.AddWithValue("@ID_Periodo", idPeriodo);

            // Ejecutar la consulta
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            // Recargar el grid después de eliminar
            CargarDetalleCurso();
        }
        protected void gvDetalleCurso_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {
            gvDetalleCurso.PageIndex = e.NewPageIndex;
            CargarDetalleCurso(); // Cargar los datos en la nueva página
        }

        protected void btnNuevoRegistro_Click(object sender, EventArgs e)
        {
            // Redirigir al formulario para crear un nuevo registro
            Response.Redirect("AgregarDetalleCurso.aspx");
        }
    }
}