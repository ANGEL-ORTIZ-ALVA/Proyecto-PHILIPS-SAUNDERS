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
    public partial class AgregarApoderado_Estudiante : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["colegioBD"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarApoderados();
                CargarEstudiantes();
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
                    else
                    {
                        ddlApoderado.Items.Add(new ListItem("No hay apoderados disponibles", ""));
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
                    else
                    {
                        ddlEstudiante.Items.Add(new ListItem("No hay estudiantes disponibles", ""));
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

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            string idApoderado = ddlApoderado.SelectedValue;
            string idEstudiante = ddlEstudiante.SelectedValue;
            string parentesco = ddlParentesco.SelectedValue;
            string estadoRegistro = ddlEstadoRegistro.SelectedValue;

            if (string.IsNullOrEmpty(idApoderado) || string.IsNullOrEmpty(idEstudiante) || string.IsNullOrEmpty(parentesco))
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
                    string query = "INSERT INTO Apoderado_Estudiante (ID_Apoderado, ID_Estudiante, Parentesco, Estado_Registro) " +
                                   "VALUES (@ID_Apoderado, @ID_Estudiante, @Parentesco, @Estado_Registro)";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ID_Apoderado", idApoderado);
                    cmd.Parameters.AddWithValue("@ID_Estudiante", idEstudiante);
                    cmd.Parameters.AddWithValue("@Parentesco", parentesco);
                    cmd.Parameters.AddWithValue("@Estado_Registro", estadoRegistro);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                lblMensaje.Text = "Registro agregado correctamente.";
                lblMensaje.ForeColor = System.Drawing.Color.Green;
                lblMensaje.Visible = true;
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al agregar el registro: " + ex.Message;
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