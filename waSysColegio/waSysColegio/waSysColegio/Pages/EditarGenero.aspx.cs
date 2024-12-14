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
    public partial class EditarGenero : System.Web.UI.Page
    {
            static String connectionString = ConfigurationManager.ConnectionStrings["ColegioBD"].ConnectionString;
            
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarDatosGenero();
            }
        }
        private void CargarDatosGenero()
        {
            int idGenero = Convert.ToInt32(Request.QueryString["id"]);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT Nombre_Genero FROM Genero WHERE ID_Genero = @ID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ID", idGenero);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    txtNombreGenero.Text = reader["Nombre_Genero"].ToString();
                }
            }
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            string nombreGenero = txtNombreGenero.Text.Trim();

            if (string.IsNullOrEmpty(nombreGenero))
            {
                lblMensaje.Text = "El nombre del género no puede estar vacío.";
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "UPDATE Genero SET Nombre_Genero = @Nombre_Genero WHERE ID_Genero = @ID";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@Nombre_Genero", nombreGenero);
                    cmd.Parameters.AddWithValue("@ID", Request.QueryString["id"]);

                    cmd.ExecuteNonQuery();
                }

                Response.Redirect("VerGenero.aspx");
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error: " + ex.Message;
            }
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("VerGenero.aspx");
        }
    }
}