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
    public partial class VerPersonal : System.Web.UI.Page
    {
        private void listado()
        {
            PersonalDAO obj = new PersonalDAO();
            DataTable dt = obj.listadoPersonal();
            gvPersonal.DataSource = dt;
            gvPersonal.DataBind();
        }

        private void CargarTipoPersonal()
        {
            PersonalDAO obj = new PersonalDAO();
            DataTable dt = obj.ListarTiposPersonal();
            ddlTipoPersonal.DataSource = dt;
            ddlTipoPersonal.DataTextField = "Nombre_Tipo_Personal"; // El nombre del campo a mostrar
            ddlTipoPersonal.DataValueField = "ID_Tipo_Personal"; // El nombre del campo que contiene el valor
            ddlTipoPersonal.DataBind();
            ddlTipoPersonal.Items.Insert(0, new ListItem("Seleccione Tipo Personal", ""));
        }

        private void CargarSeccion()
        {
            SeccionDAO obj = new SeccionDAO();
            DataTable dt = obj.ListarSecciones();
            ddlSeccion.DataSource = dt;
            ddlSeccion.DataTextField = "Nombre_Seccion";
            ddlSeccion.DataValueField = "ID_Seccion";
            ddlSeccion.DataBind();
            ddlSeccion.Items.Insert(0, new ListItem("Seleccione Sección", ""));
        }

        private void CargarGrado()
        {
            GradoDAO obj = new GradoDAO();
            DataTable dt = obj.ListarGrados();
            ddlGrado.DataSource = dt;
            ddlGrado.DataTextField = "Numero_Grado";
            ddlGrado.DataValueField = "ID_Grado";
            ddlGrado.DataBind();
            ddlGrado.Items.Insert(0, new ListItem("Seleccione Grado", ""));
        }

        private void CargarGenero()
        {
            GeneroDAO obj = new GeneroDAO();
            DataTable dt = obj.ListarGeneros();
            ddlGenero.DataSource = dt;
            ddlGenero.DataTextField = "Nombre_Genero";
            ddlGenero.DataValueField = "ID_Genero";
            ddlGenero.DataBind();
            ddlGenero.Items.Insert(0, new ListItem("Seleccione Género", ""));
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                listado();
                CargarTipoPersonal();
                CargarSeccion();
                CargarGrado();
                CargarGenero();
            }
        }
        //Llamar metodo de eliminar personal

        private void eliminarPersonal(int idPersonal)
        {
            PersonalDAO obj = new PersonalDAO();
            obj.eliminarPersonal(idPersonal);
        }

        protected void gvPersonal_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int idPersonal = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Editar")
            {
                Response.Redirect($"EditarPersonal.aspx?ID_Personal={idPersonal}");
            }
            else if (e.CommandName == "Eliminar")
            {
                eliminarPersonal(idPersonal);
                listado();
            }
        }

        protected void btnRegistrarPersonal_Click(object sender, EventArgs e)
        {
            // Redirigir al formulario de registro de personal
            Response.Redirect("AgregarPersonal.aspx");
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string filtroNombre = txtBuscar.Text.Trim();
            string tipoPersonal = ddlTipoPersonal.SelectedValue;
            string seccion = ddlSeccion.SelectedValue;
            string grado = ddlGrado.SelectedValue;
            string genero = ddlGenero.SelectedValue;

            // Verificar los valores seleccionados para depuración
            System.Diagnostics.Debug.WriteLine("Filtro Nombre: " + filtroNombre);
            System.Diagnostics.Debug.WriteLine("Tipo Personal: " + tipoPersonal);
            System.Diagnostics.Debug.WriteLine("Sección: " + seccion);
            System.Diagnostics.Debug.WriteLine("Grado: " + grado);
            System.Diagnostics.Debug.WriteLine("Género: " + genero);

            // Llamar al método para listar con los filtros aplicados
            ListarPersonalConFiltro(filtroNombre, tipoPersonal, seccion, grado, genero);
        }

        private void ListarPersonalConFiltro(string filtroNombre, string tipoPersonal, string seccion, string grado, string genero)
        {
            try
            {
                PersonalDAO obj = new PersonalDAO();
                DataTable dt = obj.listarPersonalFiltrado(filtroNombre, tipoPersonal, seccion, grado, genero);
                gvPersonal.DataSource = dt;
                gvPersonal.DataBind();
                lblNoRecords.Visible = dt.Rows.Count == 0;
            }
            catch (Exception ex)
            {
                lblNoRecords.Text = "Error al cargar la lista de personal: " + ex.Message;
                lblNoRecords.Visible = true;
            }
        }

        protected void btnMostrarTodo_Click(object sender, EventArgs e)
        {
            // Limpiar los filtros
            txtBuscar.Text = "";
            ddlTipoPersonal.SelectedIndex = 0;
            ddlSeccion.SelectedIndex = 0;
            ddlGrado.SelectedIndex = 0;
            ddlGenero.SelectedIndex = 0;

            // Llamar al método para listar todos los registros
            listado();
        }
    }
}