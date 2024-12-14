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
    public partial class EditarTipoPersonal : System.Web.UI.Page
    {
        private void cargarDatos(int idTipoPersonal)
        {
            // Cargar los datos del tipo de personal seleccionado
            TipoPersonalDAO obj = new TipoPersonalDAO();
            DataTable dt = obj.buscarTipoPersonalPorID(idTipoPersonal);

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                txtNombreTipoPersonal.Text = row["Nombre_Tipo_Personal"].ToString();
                txtDescripcion.Text = row["Descripcion"].ToString();

                hdnIDTipoPersonal.Value = row["ID_Tipo_Personal"].ToString(); // Guardar el ID_Tipo_Personal en un campo oculto
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Obtener el ID_Tipo_Personal de la URL
                if (Request.QueryString["ID_Tipo_Personal"] != null)
                {
                    int idTipoPersonal = Convert.ToInt32(Request.QueryString["ID_Tipo_Personal"]);
                    cargarDatos(idTipoPersonal);
                }
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            // Guardar los cambios
            int idTipoPersonal = Convert.ToInt32(hdnIDTipoPersonal.Value);
            TipoPersonal tipoPersonal = new TipoPersonal
            {
                ID_Tipo_Personal = idTipoPersonal,
                Nombre_Tipo_Personal = txtNombreTipoPersonal.Text.Trim(),
                Descripcion = txtDescripcion.Text.Trim()
            };

            TipoPersonalDAO obj = new TipoPersonalDAO();
            obj.actualizarTipoPersonal(tipoPersonal);

            // Mostrar el modal de éxito
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showSuccessModal();", true);
            // Redirigir de nuevo al listado
            Response.Redirect("VerTipoPersonal.aspx");
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            // Redirigir a la página de listado de tipos de personal
            Response.Redirect("VerTipoPersonal.aspx");
        }
    }
}