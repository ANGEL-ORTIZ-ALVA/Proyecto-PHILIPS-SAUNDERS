using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace waSysColegio.Pages
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Verifica si el usuario está autenticado
            if (!User.Identity.IsAuthenticated)
            {
                // Si no está autenticado, redirigir a la página de login
                Response.Redirect("~/Login.aspx");
            }
        }
    }
}