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
    public partial class AgregarLibreta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarEstudiantes();
                lblMensaje.Text = string.Empty;
            }
        }

        private void CargarEstudiantes()
        {
            try
            {
                // Cargar los estudiantes en el DropDownList
                EstudianteDAO daoEstudiante = new EstudianteDAO();
                DataTable dtEstudiantes = daoEstudiante.ListarEstudiantesRegistrados();

                ddlEstudiantes.DataSource = dtEstudiantes;
                ddlEstudiantes.DataTextField = "NombreCompleto";
                ddlEstudiantes.DataValueField = "ID_Estudiante";
                ddlEstudiantes.DataBind();

                // Agregar la opción "Seleccione un estudiante" al principio
                ddlEstudiantes.Items.Insert(0, new ListItem("Seleccione un estudiante", ""));
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al cargar la lista de estudiantes: " + ex.Message;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (ddlEstudiantes.SelectedValue == "")
            {
                lblMensaje.Text = "Debe seleccionar un estudiante.";
                return;
            }

            // Validar que el año escolar sea una fecha válida y no futura
            DateTime anioEscolar;
            if (!DateTime.TryParse(txtAnioEscolar.Text, out anioEscolar) || anioEscolar > DateTime.Now)
            {
                lblMensaje.Text = "Ingrese un año escolar válido (no en el futuro).";
                return;
            }

            // Crear una nueva instancia de Libreta
            Libreta nuevaLibreta = new Libreta
            {
                Anio_Escolar = anioEscolar,
                Estado_Registro = "Registrado",
                ID_Estudiante = int.Parse(ddlEstudiantes.SelectedValue)
            };

            try
            {
                // Insertar la libreta
                LibretaDAO daoLibreta = new LibretaDAO();
                string mensaje = daoLibreta.InsertarLibreta(nuevaLibreta);

                // Mostrar el mensaje
                lblMensaje.Text = mensaje;

                // Mostrar el modal de éxito si se registró correctamente
                if (mensaje.Contains("registrada"))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showSuccessModal();", true);
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al registrar la libreta: " + ex.Message;
            }
            Response.Redirect("VerLibreta.aspx");
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("VerLibreta.aspx");
        }
    }
}