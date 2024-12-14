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
    public partial class AgregarGenero : System.Web.UI.Page
    {
        static String connectionString = ConfigurationManager.ConnectionStrings["ColegioBD"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            string nombreGenero = txtNombreGenero.Text.Trim();

            if (string.IsNullOrEmpty(nombreGenero))
            {
                lblMensaje.Text = "Por favor ingrese el nombre del género.";
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                lblMensaje.Visible = true;
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Genero (Descripcion) VALUES (@NombreGenero)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@NombreGenero", nombreGenero);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                lblMensaje.Text = "Género agregado correctamente.";
                lblMensaje.ForeColor = System.Drawing.Color.Green;
                lblMensaje.Visible = true;
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al agregar género: " + ex.Message;
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                lblMensaje.Visible = true;
            }
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("VerGenero.aspx");
        }
    }
}