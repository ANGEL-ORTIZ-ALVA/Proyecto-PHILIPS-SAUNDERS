using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using waSysColegio.Dao;

namespace waSysColegio.Pages
{
    public partial class EditarApoderado : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int id;
                if (int.TryParse(Request.QueryString["ID_Apoderado"], out id))
                {
                    CargarApoderado(id);
                }
                else
                {
                    Response.Redirect("VerApoderados.aspx");
                }
            }
        }
        private void CargarApoderado(int id)
        {
            ApoderadoDAO obj = new ApoderadoDAO();
            DataRow dr = obj.obtenerApoderadoPorID(id);
            if (dr != null)
            {
                txtNombre.Text = dr["Nombre"].ToString();
                txtApellido.Text = dr["Apellido"].ToString();
                txtDNI.Text = dr["DNI"].ToString();
                txtCorreo.Text = dr["Correo"].ToString();
                txtTelefono.Text = dr["Telefono"].ToString(); // Agregar esta línea
                txtDireccion.Text = dr["Direccion"].ToString(); // Agregar esta línea
            }
            else
            {
                Response.Redirect("VerApoderados.aspx");
            }
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            int id;
            if (int.TryParse(Request.QueryString["ID_Apoderado"], out id))
            {
                ApoderadoDAO obj = new ApoderadoDAO();
                obj.actualizarApoderado(id, txtNombre.Text, txtApellido.Text, txtDNI.Text, txtCorreo.Text, txtTelefono.Text, txtDireccion.Text);
                Response.Redirect("VerApoderados.aspx");
            }
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("VerApoderados.aspx");
        }
    }
}