using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using waSysColegio.Dao;
using static waSysColegio.Dao.UsuarioDAO;

namespace waSysColegio
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            UsuarioDAO dao = new UsuarioDAO();
            string nombreUsuario = txtUsuario.Text;
            string password = txtPassword.Text;

            // Obtener datos del usuario
            DataTable usuario = dao.obtenerUsuarioPorNombreUsuario(nombreUsuario);

            if (usuario != null && usuario.Rows.Count > 0)
            {
                string passwordBD = usuario.Rows[0]["Password"].ToString();
                int idRol = Convert.ToInt32(usuario.Rows[0]["ID_Rol"]);

                if (passwordBD == password)
                {
                    // Guardar datos en la sesión
                    Session["Usuario"] = nombreUsuario;
                    Session["Rol"] = Convert.ToInt32(idRol);

                    int idUsuario = Convert.ToInt32(usuario.Rows[0]["ID_Usuario"]); // Obtener ID del usuario
                    Session["ID_Usuario"] = idUsuario; // Guardar en la sesión
                    //Session["Rol"] = idRol;

                    // Configurar autenticación
                    FormsAuthentication.SetAuthCookie(nombreUsuario, false);

                    // Redirigir a Dashboard
                    Response.Redirect("~/Pages/Dashboard.aspx");
                }
                else
                {
                    lblMensaje.Text = "Contraseña incorrecta";
                }
            }
            else
            {
                lblMensaje.Text = "El usuario ingresado no existe";
            }
        }
    }
}