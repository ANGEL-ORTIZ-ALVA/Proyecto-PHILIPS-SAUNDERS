using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using waSysColegio.Dao;

namespace waSysColegio.Pages
{
    public partial class AgregarApoderado : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["colegioBD"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarApoderado();
            }
        }

        private void CargarApoderado()
        {

            string query = "SELECT ID_Usuario, Nombre_Usuario FROM Usuario WHERE Nombre_Usuario LIKE 'apo%'";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        ddlUsuario.DataSource = reader;
                        ddlUsuario.DataTextField = "Nombre_Usuario";
                        ddlUsuario.DataValueField = "ID_Usuario";
                        ddlUsuario.DataBind();
                    }
                    else
                    {
                        ddlUsuario.Items.Add(new ListItem("No hay usuarios disponibles", ""));
                    }
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al cargar los usuarios: " + ex.Message;
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                lblMensaje.Visible = true;
            }
        }



        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text.Trim();
            string apellido = txtApellido.Text.Trim();
            string dni = txtDNI.Text.Trim();
            string correo = txtCorreo.Text.Trim();
            string telefono = txtTelefono.Text.Trim();
            string direccion = txtDireccion.Text.Trim();
            string genero = ddlGenero.SelectedValue;
            string estadoRegistro = ddlEstadoRegistro.SelectedValue;
            string idUsuario = ddlUsuario.SelectedValue;

            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(apellido) || string.IsNullOrEmpty(dni))
            {
                lblMensaje.Text = "Por favor complete todos los campos obligatorios.";
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                lblMensaje.Visible = true;
                return;
            }


            if (dni.Length != 8 || !System.Text.RegularExpressions.Regex.IsMatch(dni, @"^\d{8}$"))
            {
                lblMensaje.Text = "El DNI debe tener exactamente 8 dígitos.";
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                lblMensaje.Visible = true;
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Apoderado (Nombre, Apellido, DNI, Correo, Telefono, Direccion, ID_Genero, Estado_Registro, ID_Usuario) " +
                        "VALUES (@Nombre, @Apellido, @DNI, @Correo, @Telefono, @Direccion, @ID_Genero, @Estado_Registro, @ID_Usuario)";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Nombre", nombre);
                    cmd.Parameters.AddWithValue("@Apellido", apellido);
                    cmd.Parameters.AddWithValue("@DNI", dni);
                    cmd.Parameters.AddWithValue("@Correo", correo);
                    cmd.Parameters.AddWithValue("@Telefono", telefono);
                    cmd.Parameters.AddWithValue("@Direccion", direccion);
                    cmd.Parameters.AddWithValue("@ID_Genero", int.Parse(genero));  // Convertir a entero
                    cmd.Parameters.AddWithValue("@Estado_Registro", estadoRegistro);
                    cmd.Parameters.AddWithValue("@ID_Usuario", idUsuario);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                lblMensaje.Text = "Apoderado agregado correctamente.";
                lblMensaje.ForeColor = System.Drawing.Color.Green;
                lblMensaje.Visible = true;
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al agregar apoderado: " + ex.Message;
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                lblMensaje.Visible = true;
            }
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            this.Page.Validate("GroupNoValidation");
            Response.Redirect("VerApoderados.aspx");
        }
    }
}