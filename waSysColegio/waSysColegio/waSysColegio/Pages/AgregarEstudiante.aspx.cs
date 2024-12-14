using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using waSysColegio.Dao;

namespace waSysColegio.Pages
{
    public partial class AgregarEstudiante : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                // Código para agregar estudiante
                EstudianteDAO obj = new EstudianteDAO();
                obj.AgregarEstudiante(txtNombre.Text, txtApellido.Text, DateTime.Parse(txtFechanacimiento.Text), txtDNI.Text, txtDireccion.Text);
                Response.Redirect("VerEstudiantes.aspx");

            }
            catch (SqlException ex)
            {
                // Maneja excepciones de
                this.lblMensaje.Text = "Error de SQL: " + ex.Message;
            }
            catch (FormatException ex)
            {
                // Maneja excepciones de formato
                this.lblMensaje.Text = "Error de formato: " + ex.Message;
            }
            catch (Exception ex)
            {
                // Maneja excepciones generales
                this.lblMensaje.Text = "Error general: " + ex.Message;
            }
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("VerEstudiantes.aspx");
        }
    }
}