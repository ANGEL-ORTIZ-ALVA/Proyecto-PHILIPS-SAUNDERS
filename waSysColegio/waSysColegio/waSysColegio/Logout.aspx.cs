using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace waSysColegio
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Limpiar la sesión en el servidor
            Session.Clear();
            Session.Abandon();

            // Eliminar la cookie de autenticación de Forms Authentication
            FormsAuthentication.SignOut();

            // Expirar la cookie de autenticación manualmente
            HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            authCookie.Expires = DateTime.Now.AddDays(-1); // Expirar la cookie inmediatamente
            authCookie.HttpOnly = true;
            Response.Cookies.Add(authCookie);

            // Redirigir al usuario a la página de login
            Response.Redirect("~/Login.aspx");
        }
    }
}