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
    public partial class AgregarPersonal : System.Web.UI.Page
    {
        //Rellenaar los combobox de seccion y grado
        private void LlenarCombos()
        {
            // Cargar todos los dropdowns aquí
            GeneroDAO daoGenero = new GeneroDAO();
            DataTable dtGenero = daoGenero.ListarGeneros();
            ddlGenero.DataSource = dtGenero;
            ddlGenero.DataTextField = "Nombre_Genero";
            ddlGenero.DataValueField = "ID_Genero";
            ddlGenero.DataBind();
            ddlGenero.Items.Insert(0, new ListItem("-- Seleccione Genero --", "0"));

            TipoPersonalDAO daoTipopersonal = new TipoPersonalDAO();
            DataTable dtTipoPersonal = daoTipopersonal.listarTipopersonal();
            ddlTipopersonal.DataSource = dtTipoPersonal;
            ddlTipopersonal.DataTextField = "Nombre_Tipo_Personal";
            ddlTipopersonal.DataValueField = "ID_Tipo_Personal";
            ddlTipopersonal.DataBind();
            ddlTipopersonal.Items.Insert(0, new ListItem("-- Seleccione Tipo de Personal --", "0"));

            GradoDAO daoGrado = new GradoDAO();
            DataTable dtGrado = daoGrado.listarGrado();
            ddlGrado.DataSource = dtGrado;
            ddlGrado.DataTextField = "Numero_Grado";
            ddlGrado.DataValueField = "ID_Grado";
            ddlGrado.DataBind();

            Dao.SeccionDAO daoSeccion = new Dao.SeccionDAO();
            DataTable dtSeccion = daoSeccion.ListarSecciones();
            ddlSeccion.DataSource = dtSeccion;
            ddlSeccion.DataTextField = "Nombre_Seccion";
            ddlSeccion.DataValueField = "ID_Seccion";
            ddlSeccion.DataBind();

            UsuarioDAO daoUsuario = new UsuarioDAO();
            DataTable dtUsuario = daoUsuario.listarUsuariosSinAsignar();
            ddlUsuario.DataSource = dtUsuario;
            ddlUsuario.DataTextField = "Nombre_Usuario";
            ddlUsuario.DataValueField = "ID_Usuario";
            ddlUsuario.DataBind();
            ddlUsuario.Items.Insert(0, new ListItem("-- Seleccione Usuario --", "0"));
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LlenarCombos();
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            Personal personal = new Personal();
            PersonalDAO daoPersonal = new PersonalDAO();
            personal.Nombre = this.txtNombres.Text.Trim();
            personal.Apellido = this.txtApellidos.Text.Trim();
            personal.Fecha_Nacimiento = DateTime.Parse(this.txtFechaNacimiento.Text);
            personal.DNI = this.txtDNI.Text.Trim();
            personal.Correo = this.txtCorreo.Text.Trim();
            personal.Telefono = this.txtTelefono.Text.Trim();
            personal.Direccion = this.txtDireccion.Text.Trim();
            personal.ID_Tipo_Personal = int.Parse(this.ddlTipopersonal.SelectedValue);
            personal.ID_Genero = int.Parse(this.ddlGenero.SelectedValue);

            if (ddlTipopersonal.SelectedValue == "3") // El valor 3 es para Docente
            {
                personal.ID_Grado = int.Parse(this.ddlGrado.SelectedValue);
                personal.ID_Seccion = int.Parse(this.ddlSeccion.SelectedValue);
            }

            personal.ID_Usuario = int.Parse(this.ddlUsuario.SelectedValue);
            string mensaje = daoPersonal.insertar(personal);
            this.lblmensaje.Text = mensaje;

            Response.Redirect("wfListarPersonal.aspx");
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            // Redirigir a la página de listado de personal
            Response.Redirect("VerPersonal.aspx");
        }

        protected void ddlTipopersonal_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Mostrar/ocultar los campos de Sección y Grado si se selecciona "Docente"
            if (ddlTipopersonal.SelectedValue == "3") // El valor 3 corresponde a "Docente"
            {
                tutorFields.Style["display"] = "block";
            }
            else
            {
                tutorFields.Style["display"] = "none";
            }
        }
    }
}