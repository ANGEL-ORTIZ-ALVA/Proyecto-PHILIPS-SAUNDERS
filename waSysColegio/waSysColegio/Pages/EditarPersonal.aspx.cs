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
    public partial class EditarPersonal : System.Web.UI.Page
    {
        private void cargarDatos(int idPersonal)
        {
            // Cargar los datos del personal seleccionado
            PersonalDAO obj = new PersonalDAO();
            DataTable dt = obj.buscarPersonalPorID(idPersonal);

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                txtNombres.Text = row["Nombre"].ToString();
                txtApellidos.Text = row["Apellido"].ToString();
                txtFechaNacimiento.Text = Convert.ToDateTime(row["Fecha_Nacimiento"]).ToString("yyyy-MM-dd");
                txtDNI.Text = row["DNI"].ToString();
                txtCorreo.Text = row["Correo"].ToString();
                txtTelefono.Text = row["Telefono"].ToString();
                txtDireccion.Text = row["Direccion"].ToString();

                // Cargar valores de los dropdowns
                ddlTipoPersonal.SelectedValue = row["ID_Tipo_Personal"].ToString();
                ddlGenero.SelectedValue = row["ID_Genero"].ToString();
                ddlGrado.SelectedValue = row["ID_Grado"].ToString();
                ddlSeccion.SelectedValue = row["ID_Seccion"].ToString();
                ddlUsuario.SelectedValue = row["ID_Usuario"].ToString();

                hdnIDPersonal.Value = row["ID_Personal"].ToString(); // Guardar el ID_Personal en un campo oculto
            }
        }

        private void cargarDropdowns()
        {
            PersonalDAO per = new PersonalDAO();
            GeneroDAO gen = new GeneroDAO();
            GradoDAO gra = new GradoDAO();
            SeccionDAO sec = new SeccionDAO();

            UsuarioDAO objUsuario = new UsuarioDAO();

            // Cargar Tipo de Personal
            ddlTipoPersonal.DataSource = per.ListarTiposPersonal();
            ddlTipoPersonal.DataTextField = "Nombre_Tipo_Personal";
            ddlTipoPersonal.DataValueField = "ID_Tipo_Personal";
            ddlTipoPersonal.DataBind();

            // Cargar Género
            ddlGenero.DataSource = gen.ListarGeneros();
            ddlGenero.DataTextField = "Nombre_Genero";
            ddlGenero.DataValueField = "ID_Genero";
            ddlGenero.DataBind();

            // Cargar Grado
            ddlGrado.DataSource = gra.ListarGrados();
            ddlGrado.DataTextField = "Numero_Grado";
            ddlGrado.DataValueField = "ID_Grado";
            ddlGrado.DataBind();

            // Cargar Sección
            ddlSeccion.DataSource = sec.ListarSecciones();
            ddlSeccion.DataTextField = "Nombre_Seccion";
            ddlSeccion.DataValueField = "ID_Seccion";
            ddlSeccion.DataBind();

            // Cargar Usuario
            ddlUsuario.DataSource = objUsuario.listarUsuario();
            ddlUsuario.DataTextField = "Nombre_Usuario";
            ddlUsuario.DataValueField = "ID_Usuario";
            ddlUsuario.DataBind();
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Cargar los dropdowns
                cargarDropdowns();

                // Obtener el ID_Personal de la URL
                if (Request.QueryString["ID_Personal"] != null)
                {
                    int idPersonal = Convert.ToInt32(Request.QueryString["ID_Personal"]);
                    cargarDatos(idPersonal);
                }
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            // Guardar los cambios
            int idPersonal = Convert.ToInt32(hdnIDPersonal.Value);
            Personal personal = new Personal
            {
                ID_Personal = idPersonal,
                Nombre = txtNombres.Text.Trim(),
                Apellido = txtApellidos.Text.Trim(),
                Fecha_Nacimiento = DateTime.Parse(txtFechaNacimiento.Text),
                DNI = txtDNI.Text.Trim(),
                Correo = txtCorreo.Text.Trim(),
                Telefono = txtTelefono.Text.Trim(),
                Direccion = txtDireccion.Text.Trim(),
                ID_Tipo_Personal = Convert.ToInt32(ddlTipoPersonal.SelectedValue),
                ID_Genero = Convert.ToInt32(ddlGenero.SelectedValue),
                ID_Grado = Convert.ToInt32(ddlGrado.SelectedValue),
                ID_Seccion = Convert.ToInt32(ddlSeccion.SelectedValue),
                ID_Usuario = Convert.ToInt32(ddlUsuario.SelectedValue)
            };

            PersonalDAO obj = new PersonalDAO();
            obj.actualizarPersonal(personal);

            // Mostrar modal de éxito o redirigir al listado
            Response.Redirect("VerPersonal.aspx");
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            // Redirigir a la página de listado de personal
            Response.Redirect("VerPersonal.aspx");
        }
    }
}