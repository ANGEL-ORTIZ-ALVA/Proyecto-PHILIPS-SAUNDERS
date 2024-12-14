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
    public partial class EditarLibreta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarEstudiantes();

                if (Request.QueryString["ID_Libreta"] != null)
                {
                    int idLibreta = Convert.ToInt32(Request.QueryString["ID_Libreta"]);
                    CargarDatos(idLibreta);
                }
            }
        }
        private void CargarEstudiantes()
        {
            EstudianteDAO daoEstudiante = new EstudianteDAO();
            DataTable dtEstudiantes = daoEstudiante.ListarEstudiantesRegistrados();

            ddlEstudiantes.DataSource = dtEstudiantes;
            ddlEstudiantes.DataTextField = "NombreCompleto";
            ddlEstudiantes.DataValueField = "ID_Estudiante";
            ddlEstudiantes.DataBind();

            ddlEstudiantes.Items.Insert(0, new ListItem("Seleccione un estudiante", ""));
        }

        private void CargarDatos(int idLibreta)
        {
            LibretaDAO daoLibreta = new LibretaDAO();
            DataTable dt = daoLibreta.BuscarLibretaPorID(idLibreta);

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                txtAnioEscolar.Text = Convert.ToDateTime(row["Anio_Escolar"]).ToString("yyyy-MM-dd");
                ddlEstudiantes.SelectedValue = row["ID_Estudiante"].ToString();
                hdnIDLibreta.Value = row["ID_Libreta"].ToString();
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            int idLibreta = Convert.ToInt32(hdnIDLibreta.Value);
            Libreta libreta = new Libreta
            {
                ID_Libreta = idLibreta,
                Anio_Escolar = DateTime.Parse(txtAnioEscolar.Text),
                Estado_Registro = "Registrado",
                ID_Estudiante = int.Parse(ddlEstudiantes.SelectedValue)
            };

            LibretaDAO daoLibreta = new LibretaDAO();
            string mensaje = daoLibreta.ActualizarLibreta(libreta);

            lblMensaje.Text = mensaje;
            if (mensaje.Contains("actualizada"))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "alert('Libreta actualizada exitosamente'); window.location='VerLibreta.aspx';", true);
            }
            Response.Redirect("VerLibreta.aspx");
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("VerLibreta.aspx");
        }
    }
}