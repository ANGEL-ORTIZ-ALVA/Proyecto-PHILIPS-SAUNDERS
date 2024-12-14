using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using waSysColegio.Dao;
using waSysColegio.Models;

namespace waSysColegio.Pages
{
    public partial class EditarEstudiante : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int id;
                if (int.TryParse(Request.QueryString["ID_Estudiante"], out id))
                {
                    CargarEstudiante(id);
                }
                else
                {
                    Response.Redirect("VerEstudiantes.aspx");
                }
            }
        }
        private void CargarEstudiante(int id)
        {
            EstudianteDAO obj = new EstudianteDAO();
            DataRow dr = obj.ListarEstudiantePorID(id);
            if (dr != null)
            {
                txtNombre.Text = dr["Nombre"].ToString();
                txtApellido.Text = dr["Apellido"].ToString();
                txtFechanacimiento.Text = dr["Fecha_Nacimiento"].ToString();
                txtDNI.Text = dr["DNI"].ToString();
                txtDireccion.Text = dr["Direccion"].ToString();

            }
            else
            {
                Response.Redirect("VerEstudiantes.aspx");
            }
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            int id;
            if (int.TryParse(Request.QueryString["ID_Estudiante"], out id))
            {
                EstudianteDAO obj = new EstudianteDAO();
                obj.ActualizarEstudiante(id, txtNombre.Text, txtApellido.Text, DateTime.Parse(txtFechanacimiento.Text), txtDNI.Text, txtDireccion.Text);
                Response.Redirect("VerEstudiantes.aspx");
            }
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("VerEstudiantes.aspx");
        }
    }
}