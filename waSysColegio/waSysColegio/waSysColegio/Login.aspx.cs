using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using waSysColegio.Dao;

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

            DataTable usuario = dao.obtenerUsuarioPorNombreUsuario(nombreUsuario);

            if (usuario != null && usuario.Rows.Count > 0)
            {
                string passwordBD = usuario.Rows[0]["Password"].ToString();

                if (passwordBD == password)
                {
                    //Inicio de session exitoso
                    //Redirigir a pagina principal: dashboard
                    FormsAuthentication.SetAuthCookie(nombreUsuario, false);
                    Response.Redirect("~/Pages/Dashboard.aspx");
                }
                else
                {
                    //contraseña incorrecta
                    lblMensaje.Text = "Contraseña incorrecta";
                }
            }
            else
            {
                //usuario no existe
                lblMensaje.Text = "El usuario ingresado no existe";
            }
        }
    }
}