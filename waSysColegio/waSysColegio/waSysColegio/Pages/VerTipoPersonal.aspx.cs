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
    public partial class VerTipoPersonal : System.Web.UI.Page
    {
        private void listado()
        {
            TipoPersonalDAO obj = new TipoPersonalDAO();
            DataTable dt = obj.listarTipopersonal(); // Ahora solo lista los que están 'Registrado'
            gvTipoPersonal.DataSource = dt;
            gvTipoPersonal.DataBind();

            lblNoRecords.Visible = dt.Rows.Count == 0;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                listado();
            }
        }

        protected void btnRegistrarTipoPersonal_Click(object sender, EventArgs e)
        {
            // Redirigir al formulario de registro de tipo de personal
            Response.Redirect("AgregarTipoPersonal.aspx");
        }

        protected void gvTipoPersonal_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int idTipoPersonal = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Editar")
            {
                Response.Redirect($"EditarTipoPersonal.aspx?ID_Tipo_Personal={idTipoPersonal}");
            }
            else if (e.CommandName == "Eliminar")
            {
                eliminarTipoPersonal(idTipoPersonal);
                listado();
            }
        }

        private void eliminarTipoPersonal(int idTipoPersonal)
        {
            PersonalDAO obj = new PersonalDAO();
            obj.eliminarTipoPersonal(idTipoPersonal);
        }

        protected void gvTipoPersonal_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}