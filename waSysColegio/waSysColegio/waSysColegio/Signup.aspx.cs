using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using waSysColegio.Dao;
using waSysColegio.Models;

namespace waSysColegio
{
    public partial class Signup : System.Web.UI.Page
    {
        private void llenarComboRol()
        {
            RolDAO dao = new RolDAO();
            DataTable dt = dao.listarRoles();
            this.ddlRol.DataSource = dt;
            this.ddlRol.DataTextField = "Nombre_Rol";
            this.ddlRol.DataValueField = "ID_Rol";
            this.ddlRol.DataBind();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                llenarComboRol();
            }
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            UsuarioDAO dao = new UsuarioDAO();
            Usuario usuario = new Usuario();
            usuario.ID_Rol = int.Parse(ddlRol.SelectedValue);
            // Asignar valores predeterminados para los demás campos
            usuario.Fecha_Creacion = DateTime.Now;
            usuario.Ultimo_Acceso = DateTime.Now.AddMinutes(1); //suma 1 minuto para asegurar que sea mayor
            usuario.Estado_Registro = "Registrado";
            usuario.ID_Estado_Usuario = 1; // ID de estado "Activo"

            usuario.Nombre_Usuario = txtNombreUsuario.Text;
            usuario.Password = txtPassword.Text;

            if (txtPassword.Text == txtConfirmPassword.Text)
            {
                string mensaje = dao.insertar(usuario);

                if (mensaje == "Usuario registrado")
                {
                    Response.Redirect("/Login.aspx");
                }
                else
                {
                    lblMensaje.Text = mensaje;
                }
            }
            else
            {
                lblMensaje.Text = "Las contraseñas no coinciden";
            }
        }
    }
}