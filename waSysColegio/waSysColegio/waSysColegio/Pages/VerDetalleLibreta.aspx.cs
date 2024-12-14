using waSysColegio.Dao;
using System;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace waSysColegio.Pages
{
    public partial class VerDetalleLibreta : System.Web.UI.Page
    {
        private void ListarDetallesLibreta(int? idLibreta = null)
        {
            try
            {
                DetalleLibretaDAO daoDetalleLibreta = new DetalleLibretaDAO();
                DataTable dt;

                // Si se proporciona un ID de libreta, filtra los resultados por esa libreta
                if (idLibreta.HasValue)
                {
                    dt = daoDetalleLibreta.ListarDetallesLibretaPorLibreta(idLibreta.Value);
                }
                else
                {
                    dt = daoDetalleLibreta.ListarDetallesLibreta();
                }

                gvDetallesLibreta.DataSource = dt;
                gvDetallesLibreta.DataBind();

                lblNoRecords.Visible = dt.Rows.Count == 0;
            }
            catch (Exception ex)
            {
                lblNoRecords.Text = "Error al cargar la lista de detalles de libretas: " + ex.Message;
                lblNoRecords.Visible = true;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Obtener el ID de libreta de la URL si existe
                int? idLibreta = null;
                if (Request.QueryString["ID_Libreta"] != null)
                {
                    idLibreta = Convert.ToInt32(Request.QueryString["ID_Libreta"]);
                }

                // Listar los detalles según el ID de libreta
                ListarDetallesLibreta(idLibreta);
            }
        }

        protected void btnRegistrarDetalleLibreta_Click(object sender, EventArgs e)
        {
            // Obtener el ID de la libreta de la URL
            if (Request.QueryString["ID_Libreta"] != null)
            {
                int idLibreta = int.Parse(Request.QueryString["ID_Libreta"]);
                // Redirigir al formulario de registro de detalles, pasando el ID de la libreta en la URL
                Response.Redirect($"AgregarDetalleLibreta.aspx?ID_Libreta={idLibreta}");
            }
            else
            {
                // Manejar el caso en que no haya un ID de libreta en la URL
                lblNoRecords.Text = "No se ha seleccionado una libreta válida.";
                lblNoRecords.Visible = true;
            }
        }

        protected void gvDetallesLibreta_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string[] commandArgs = e.CommandArgument.ToString().Split(',');
            int idLibreta = Convert.ToInt32(commandArgs[0]);
            int idPersonal = Convert.ToInt32(commandArgs[1]);

            if (e.CommandName == "Editar")
            {
                Response.Redirect($"EditarDetalleLibreta.aspx?ID_Libreta={idLibreta}&ID_Personal={idPersonal}");
            }
            else if (e.CommandName == "Eliminar")
            {
                EliminarDetalleLibreta(idLibreta, idPersonal);
                ListarDetallesLibreta();
            }
        }

        private void EliminarDetalleLibreta(int idLibreta, int idPersonal)
        {
            try
            {
                DetalleLibretaDAO daoDetalleLibreta = new DetalleLibretaDAO();
                daoDetalleLibreta.EliminarDetalleLibreta(idLibreta, idPersonal);
            }
            catch (Exception ex)
            {
                lblNoRecords.Text = "Error al eliminar el detalle de libreta: " + ex.Message;
                lblNoRecords.Visible = true;
            }

            // Redirigir a la lista de detalles de la libreta filtrada por el ID de libreta
            Response.Redirect($"VerDetalleLibreta.aspx?ID_Libreta={idLibreta}");
        }

        protected void btnVolverListarLibreta_Click(object sender, EventArgs e)
        {
            Response.Redirect("VerLibreta.aspx");
        }
    }
}