using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using waSysColegio.Models;

namespace waSysColegio.Pages
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/Login.aspx");
            }

            if (Session["Usuario"] != null && Session["Rol"] != null)
            {
                string nombreUsuario = Session["Usuario"].ToString();
                int idRol = Convert.ToInt32(Session["Rol"]);

                // Convierte el ID del rol a su descripción usando el enum
                string nombreRol = ((Roles_Enum)idRol).GetDescription();

                lblUsuario.Text = $"Bienvenido, {nombreUsuario}.";
                lblRol.Text = $"Usted es: {nombreRol}";
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }


            /*/ Verifica si el usuario está autenticado
            if (!User.Identity.IsAuthenticated)
            {
                // Si no está autenticado, redirigir a la página de login
                Response.Redirect("~/Login.aspx");
            }*/
        }

        private string ObtenerNombreRol(int idRol)
        {
            switch (idRol)
            {
                case 1:
                    return "Administrador";
                case 2:
                    return "Docente";
                case 3:
                    return "Estudiante";
                case 4:
                    return "Apoderado";
                case 5:
                    return "Directiva";
                default:
                    return "Usuario"; 
            }
        }
    }
}