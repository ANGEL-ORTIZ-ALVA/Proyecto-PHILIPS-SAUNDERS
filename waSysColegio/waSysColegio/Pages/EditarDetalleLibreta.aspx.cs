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
    public partial class EditarDetalleLibreta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarPersonal();
                lblMensaje.Text = string.Empty;

                // Obtener los IDs de la URL
                if (Request.QueryString["ID_Libreta"] != null && Request.QueryString["ID_Personal"] != null)
                {
                    int idLibreta = Convert.ToInt32(Request.QueryString["ID_Libreta"]);
                    int idPersonal = Convert.ToInt32(Request.QueryString["ID_Personal"]);
                    hdnIDLibreta.Value = idLibreta.ToString();
                    hdnIDPersonal.Value = idPersonal.ToString();

                    // Cargar los datos del detalle de libreta
                    CargarDatos(idLibreta, idPersonal);
                }
            }
        }

        private void CargarPersonal()
        {
            // Cargar el personal en el DropDownList
            PersonalDAO  daoPersonal = new PersonalDAO();
            DataTable dtPersonal = daoPersonal.listadoPersonal();

            ddlPersonal.DataSource = dtPersonal;
            ddlPersonal.DataTextField = "Nombre";
            ddlPersonal.DataValueField = "ID_Personal";
            ddlPersonal.DataBind();

            // Agregar la opción "Seleccione un personal" al principio
            ddlPersonal.Items.Insert(0, new ListItem("Seleccione un personal", ""));
        }

        private void CargarDatos(int idLibreta, int idPersonal)
        {
            DetalleLibretaDAO daoDetalleLibreta = new DetalleLibretaDAO();
            DataTable dt = daoDetalleLibreta.BuscarDetalleLibretaPorID(idLibreta, idPersonal);

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                ddlPersonal.SelectedValue = row["ID_Personal"].ToString();
                hdnIDPersonal.Value = row["ID_Personal"].ToString(); // Asignar el valor original aquí

                // Mostrar la firma actual si está disponible
                if (row["Firma"] != DBNull.Value)
                {
                    byte[] firmaBytes = (byte[])row["Firma"];
                    string base64Firma = Convert.ToBase64String(firmaBytes);
                    imgFirmaActual.ImageUrl = $"data:image/png;base64,{base64Firma}";
                    imgFirmaActual.Visible = true;
                    lblFirmaActual.Text = "Firma actual";
                }
                else
                {
                    lblFirmaActual.Text = "Sin Firma";
                    imgFirmaActual.Visible = false;
                }

                // Mostrar el sello actual si está disponible
                if (row["Sello"] != DBNull.Value)
                {
                    byte[] selloBytes = (byte[])row["Sello"];
                    string base64Sello = Convert.ToBase64String(selloBytes);
                    imgSelloActual.ImageUrl = $"data:image/png;base64,{base64Sello}";
                    imgSelloActual.Visible = true;
                    lblSelloActual.Text = "Sello actual";
                }
                else
                {
                    lblSelloActual.Text = "Sin Sello";
                    imgSelloActual.Visible = false;
                }
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (ddlPersonal.SelectedValue == "")
            {
                lblMensaje.Text = "Debe seleccionar un personal.";
                return;
            }

            // Crear una instancia de Detalle_Libreta con los datos actuales
            Detalle_Libreta detalleLibreta = new Detalle_Libreta
            {
                ID_Libreta = int.Parse(hdnIDLibreta.Value),
                ID_Personal = int.Parse(ddlPersonal.SelectedValue),
                Estado_Registro = "Registrado"
            };

            DetalleLibretaDAO daoDetalleLibreta = new DetalleLibretaDAO();

            // Verificar si no se seleccionó un nuevo archivo para Firma, mantener la firma actual
            if (!fileFirma.HasFile)
            {
                DataTable dt = daoDetalleLibreta.BuscarDetalleLibretaPorID(detalleLibreta.ID_Libreta, int.Parse(hdnIDPersonal.Value));
                detalleLibreta.Firma = dt.Rows.Count > 0 && dt.Rows[0]["Firma"] != DBNull.Value ? (byte[])dt.Rows[0]["Firma"] : null;
            }
            else
            {
                detalleLibreta.Firma = fileFirma.FileBytes;
            }

            // Verificar si no se seleccionó un nuevo archivo para Sello, mantener el sello actual
            if (!fileSello.HasFile)
            {
                DataTable dt = daoDetalleLibreta.BuscarDetalleLibretaPorID(detalleLibreta.ID_Libreta, int.Parse(hdnIDPersonal.Value));
                detalleLibreta.Sello = dt.Rows.Count > 0 && dt.Rows[0]["Sello"] != DBNull.Value ? (byte[])dt.Rows[0]["Sello"] : null;
            }
            else
            {
                detalleLibreta.Sello = fileSello.FileBytes;
            }

            // Obtener el valor del ID de Personal original desde el campo oculto
            int idPersonalOriginal = int.Parse(hdnIDPersonal.Value);

            // Actualizar el detalle de la libreta, pasando el idPersonalOriginal
            string mensaje = daoDetalleLibreta.ActualizarDetalleLibreta(detalleLibreta, idPersonalOriginal);

            // Mostrar el mensaje
            lblMensaje.Text = mensaje;

            if (mensaje.Contains("actualizado"))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showSuccessModal();", true);
            }

            // Redirigir a la lista de detalles de la libreta filtrada por el ID de libreta
            Response.Redirect($"VerDetalleLibreta.aspx?ID_Libreta={detalleLibreta.ID_Libreta}");
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            // Obtener el ID de la libreta desde el campo oculto
            int idLibreta = int.Parse(hdnIDLibreta.Value);
            // Redirigir a la página de listado de detalles de la libreta con el ID de la libreta
            Response.Redirect($"VerDetalleLibreta.aspx?ID_Libreta={idLibreta}");
        }
    }
}