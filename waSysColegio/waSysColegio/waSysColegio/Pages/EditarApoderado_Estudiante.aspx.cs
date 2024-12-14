using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace waSysColegio.Pages
{
    public partial class EditarApoderado_Estudiante : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["colegioBD"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarApoderados();
                CargarEstudiantes();
                CargarDatosExistentes();
            }
        }

        private void CargarApoderados()
        {
            string query = "SELECT ID_Apoderado, Nombre FROM Apoderado";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        ddlApoderado.DataSource = reader;
                        ddlApoderado.DataTextField = "Nombre";
                        ddlApoderado.DataValueField = "ID_Apoderado";
                        ddlApoderado.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al cargar los apoderados: " + ex.Message;
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                lblMensaje.Visible = true;
            }
        }

        private void CargarEstudiantes()
        {
            string query = "SELECT ID_Estudiante, Nombre FROM Estudiante";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        ddlEstudiante.DataSource = reader;
                        ddlEstudiante.DataTextField = "Nombre";
                        ddlEstudiante.DataValueField = "ID_Estudiante";
                        ddlEstudiante.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al cargar los estudiantes: " + ex.Message;
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                lblMensaje.Visible = true;
            }
        }

        private void CargarDatosExistentes()
        {
            string idApoderado = Request.QueryString["ID_Apoderado"];
            string idEstudiante = Request.QueryString["ID_Estudiante"];

            if (!string.IsNullOrEmpty(idApoderado) && !string.IsNullOrEmpty(idEstudiante))
            {
                string query = "SELECT Parentesco, Estado_Registro FROM Apoderado_Estudiante WHERE ID_Apoderado = @ID_Apoderado AND ID_Estudiante = @ID_Estudiante";

                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@ID_Apoderado", idApoderado);
                        cmd.Parameters.AddWithValue("@ID_Estudiante", idEstudiante);
                        conn.Open();

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            ddlParentesco.SelectedValue = reader["Parentesco"].ToString();
                            ddlEstadoRegistro.SelectedValue = reader["Estado_Registro"].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    lblMensaje.Text = "Error al cargar los datos: " + ex.Message;
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                    lblMensaje.Visible = true;
                }
            }
        }
        protected void btnEditar_Click(object sender, EventArgs e)
        {
            string idApoderado = ddlApoderado.SelectedValue;
            string idEstudiante = ddlEstudiante.SelectedValue;
            string parentesco = ddlParentesco.SelectedValue;
            string estadoRegistro = ddlEstadoRegistro.SelectedValue;

            if (string.IsNullOrEmpty(parentesco))
            {
                lblMensaje.Text = "Por favor complete todos los campos obligatorios.";
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                lblMensaje.Visible = true;
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Apoderado_Estudiante SET Parentesco = @Parentesco, Estado_Registro = @Estado_Registro " +
                                   "WHERE ID_Apoderado = @ID_Apoderado AND ID_Estudiante = @ID_Estudiante";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ID_Apoderado", idApoderado);
                    cmd.Parameters.AddWithValue("@ID_Estudiante", idEstudiante);
                    cmd.Parameters.AddWithValue("@Parentesco", parentesco);
                    cmd.Parameters.AddWithValue("@Estado_Registro", estadoRegistro);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                lblMensaje.Text = "Registro actualizado correctamente.";
                lblMensaje.ForeColor = System.Drawing.Color.Green;
                lblMensaje.Visible = true;
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al actualizar el registro: " + ex.Message;
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                lblMensaje.Visible = true;
            }
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("VerApoderado_Estudiante.aspx");
        }
    }
}