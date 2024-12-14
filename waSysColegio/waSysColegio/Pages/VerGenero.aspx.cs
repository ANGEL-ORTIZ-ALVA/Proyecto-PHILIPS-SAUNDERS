using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using waSysColegio.Dao;

namespace waSysColegio.Pages
{
    public partial class VerGenero : System.Web.UI.Page
    {
        private GeneroDAO daoGenero = new GeneroDAO();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ListarGeneros();
            }
        }

        private void ListarGeneros()
        {
            try
            {
                gvGeneros.DataSource = daoGenero.ListarGeneros();
                gvGeneros.DataBind();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("AgregarGenero.aspx");
        }

        protected void gvGeneros_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int idGenero = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "Editar")
            {
                Response.Redirect($"EditarGenero.aspx?ID_Genero={idGenero}");
            }
            else if (e.CommandName == "EliminarGenero")
            {
                daoGenero.EliminarGenero(idGenero);
                ListarGeneros();
            }
        }
    }
}