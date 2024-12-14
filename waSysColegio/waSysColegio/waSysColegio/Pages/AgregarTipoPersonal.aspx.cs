using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using waSysColegio.Dao;
using waSysColegio.Models;

namespace waSysColegio.Pages
{
    public partial class AgregarTipoPersonal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            // Crear una instancia de Tipo_Personal y DaoTipoPersonal
            TipoPersonal tipoPersonal = new TipoPersonal();
            TipoPersonalDAO daoTipoPersonal = new TipoPersonalDAO();

            // Asignar valores de los campos del formulario
            tipoPersonal.Nombre_Tipo_Personal = txtNombreTipoPersonal.Text.Trim();
            tipoPersonal.Descripcion = txtDescripcion.Text.Trim();

            // Insertar el nuevo registro de Tipo_Personal en la base de datos
            string mensaje = daoTipoPersonal.insertarTipoPersonal(tipoPersonal);

            // Mostrar el mensaje de confirmación o error en la etiqueta lblMensaje
            lblMensaje.Text = mensaje;

            // Ejecutar el script para mostrar el modal de éxito
            if (mensaje.Contains("registrado")) // Asegúrate de que el mensaje sea positivo
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showSuccessModal();", true);
            }
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