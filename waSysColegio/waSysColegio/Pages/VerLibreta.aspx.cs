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
    public partial class VerLibreta : System.Web.UI.Page
    {
        private void ListarLibretas()
        {
            try
            {
                LibretaDAO daoLibreta = new LibretaDAO();
                DataTable dt = daoLibreta.ListarLibretas();
                gvLibretas.DataSource = dt;
                gvLibretas.DataBind();

                lblNoRecords.Visible = dt.Rows.Count == 0;
            }
            catch (Exception ex)
            {
                lblNoRecords.Text = "Error al cargar la lista de libretas: " + ex.Message;
                lblNoRecords.Visible = true;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ListarLibretas();
            }
        }

        protected void btnRegistrarLibreta_Click(object sender, EventArgs e)
        {
            Response.Redirect("AgregarLibreta.aspx");
        }

        protected void gvLibretas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int idLibreta = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Editar")
            {
                Response.Redirect($"EditarLibreta.aspx?ID_Libreta={idLibreta}");
            }
            else if (e.CommandName == "Eliminar")
            {
                EliminarLibreta(idLibreta);
                ListarLibretas();
            }
            else if (e.CommandName == "Ver")
            {
                // Redirigir a la página de detalle de la libreta
                Response.Redirect($"VerDetalleLibreta.aspx?ID_Libreta={idLibreta}");
            }
        }

        private void EliminarLibreta(int idLibreta)
        {
            try
            {
                LibretaDAO daoLibreta = new LibretaDAO();
                daoLibreta.EliminarLibreta(idLibreta);
            }
            catch (Exception ex)
            {
                lblNoRecords.Text = "Error al eliminar la libreta: " + ex.Message;
                lblNoRecords.Visible = true;
            }
        }

        private void ListarLibretasConFiltro(string filtroNombre = "", string anioDesde = "", string anioHasta = "")
        {
            try
            {
                LibretaDAO daoLibreta = new LibretaDAO();
                DataTable dt = daoLibreta.ListarLibretasFiltradas(filtroNombre, anioDesde, anioHasta);
                gvLibretas.DataSource = dt;
                gvLibretas.DataBind();

                lblNoRecords.Visible = dt.Rows.Count == 0;
            }
            catch (Exception ex)
            {
                lblNoRecords.Text = "Error al cargar la lista de libretas: " + ex.Message;
                lblNoRecords.Visible = true;
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string filtroNombre = txtBuscar.Text.Trim();
            string anioDesde = txtAnioDesde.Text.Trim();
            string anioHasta = txtAnioHasta.Text.Trim();
            ListarLibretasConFiltro(filtroNombre, anioDesde, anioHasta);
        }

        protected void btnListarTodo_Click(object sender, EventArgs e)
        {
            // Llamar a ListarLibretas para mostrar todos los registros
            ListarLibretas();
            // Limpiar los campos de búsqueda
            txtBuscar.Text = "";
            txtAnioDesde.Text = "";
            txtAnioHasta.Text = "";
        }
    }
}