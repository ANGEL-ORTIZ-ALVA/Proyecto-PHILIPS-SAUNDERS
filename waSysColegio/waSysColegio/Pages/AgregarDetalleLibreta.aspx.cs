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
    public partial class AgregarDetalleLibreta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Obtener el ID de la libreta desde la URL
                if (Request.QueryString["ID_Libreta"] != null)
                {
                    int idLibreta = int.Parse(Request.QueryString["ID_Libreta"]);
                    hdnIDLibreta.Value = idLibreta.ToString();
                }
                else
                {
                    // Si no hay un ID de libreta en la URL, redirigir a una página de error o manejar la excepción
                    Response.Redirect("VerLibreta.aspx");
                    return;
                }
                CargarPersonal(); // Llamar al método para cargar el personal
                lblMensaje.Text = string.Empty;
            }
        }

        private void CargarPersonal()
        {
            // Cargar solo directores, tutores y docentes en el DropDownList
            PersonalDAO daoPersonal = new PersonalDAO();
            DataTable dtPersonal = daoPersonal.listarPersonalParaLibreta();

            ddlPersonal.DataSource = dtPersonal;
            ddlPersonal.DataTextField = "Nombre";
            ddlPersonal.DataValueField = "ID_Personal";
            ddlPersonal.DataBind();

            // Agregar la opción "Seleccione un personal" al principio
            ddlPersonal.Items.Insert(0, new ListItem("Seleccione un personal", ""));
        }


        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (ddlPersonal.SelectedValue == "")
            {
                lblMensaje.Text = "Debe seleccionar un personal.";
                return;
            }

            // Obtener el ID de la libreta desde el HiddenField
            int idLibreta = int.Parse(hdnIDLibreta.Value);

            // Crear una nueva instancia de Detalle_Libreta
            Detalle_Libreta detalleLibreta = new Detalle_Libreta
            {
                ID_Libreta = idLibreta,
                ID_Personal = int.Parse(ddlPersonal.SelectedValue),
                Estado_Registro = "Registrado"
            };

            // Verificar si se ha subido un archivo para la firma
            if (fileFirma.HasFile)
            {
                detalleLibreta.Firma = fileFirma.FileBytes;
            }
            else
            {
                // Asignar un valor predeterminado para indicar "Sin Firma"
                detalleLibreta.Firma = System.Text.Encoding.UTF8.GetBytes("Sin Firma");
            }

            // Verificar si se ha subido un archivo para el sello
            if (fileSello.HasFile)
            {
                detalleLibreta.Sello = fileSello.FileBytes;
            }
            else
            {
                // Asignar un valor predeterminado para indicar "Sin Sello"
                detalleLibreta.Sello = System.Text.Encoding.UTF8.GetBytes("Sin Sello");
            }

            // Insertar el detalle de la libreta
            DetalleLibretaDAO daoDetalleLibreta = new DetalleLibretaDAO();
            string mensaje = daoDetalleLibreta.InsertarDetalleLibreta(detalleLibreta);

            // Mostrar el mensaje
            lblMensaje.Text = mensaje;

            if (mensaje.Contains("registrado"))
            {
                // Redirigir a la lista de detalles de la libreta, filtrada por el ID de la libreta registrada
                Response.Redirect($"VerDetalleLibreta.aspx?ID_Libreta={detalleLibreta.ID_Libreta}");
            }
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            int idLibreta = int.Parse(hdnIDLibreta.Value);
            Response.Redirect($"VerDetalleLibreta.aspx?ID_Libreta={idLibreta}");
        }
    }
}