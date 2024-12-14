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
    public partial class VerEstudiantes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Verifica si el usuario está autenticado
            if (!User.Identity.IsAuthenticated)
            {
                // Si no está autenticado, redirigir a la página de login
                Response.Redirect("~/Login.aspx");
            }

            int idEstudiante = Convert.ToInt32(Request.QueryString["ID_Estudiante"]);
            if (!IsPostBack)
            {
                listado();
            }
        }
        private void listado()
        {
            EstudianteDAO obj = new EstudianteDAO();
            DataTable dt = obj.listaestudiantes();
            if (dt != null && dt.Rows.Count > 0)
            {
                this.gvEs.DataSource = dt;
                this.gvEs.DataBind();
            }
            else
            {
                // Maneja el caso cuando no hay datos
                this.gvEs.DataSource = null;
                this.gvEs.DataBind();
            }
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("AgregarEstudiante.aspx");
        }

        protected void gvApo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Editar")
            {
                int id_Estudiante = Convert.ToInt32(e.CommandArgument);
                Response.Redirect($"EditarEstudiante.aspx?ID_Estudiante={id_Estudiante}");
            }
            else if (e.CommandName == "EliminarEstudiante")
            {
                int id_Estudiante = Convert.ToInt32(e.CommandArgument);
                EstudianteDAO obj = new EstudianteDAO();
                obj.EliminarEstudiante(id_Estudiante);
                listado();
            }
        }
        protected void gvApo_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Tu lógica aquí
        }
    }
}