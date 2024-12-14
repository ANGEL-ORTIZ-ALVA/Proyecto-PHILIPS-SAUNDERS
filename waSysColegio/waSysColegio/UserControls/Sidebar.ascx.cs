using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using waSysColegio.Models;

namespace waSysColegio.UserControls
{
    public partial class Sidebar : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Obtener el rol del usuario desde la sesión
            if (Session["Rol"] != null)
            {
                Roles_Enum rol = (Roles_Enum)Session["Rol"];

                // Configurar visibilidad de elementos según el rol
                ConfigurarVisibilidadMenu(rol);
            }
            else
            {
                // Si no hay sesión, redirigir al login
                Response.Redirect("~/Login.aspx");
            }
        }

        private void ConfigurarVisibilidadMenu(Roles_Enum rol)
        {
            // Administradores tienen acceso completo
            MenuDashboard.Visible = true; // Siempre visible
            MenuNotas.Visible = rol == Roles_Enum.Administrador || rol == Roles_Enum.Directiva || rol == Roles_Enum.Docente || rol == Roles_Enum.Apoderado || rol == Roles_Enum.Estudiante;
            MenuCalificaciones.Visible = rol == Roles_Enum.Administrador || rol == Roles_Enum.Directiva || rol == Roles_Enum.Docente;
            MenuEstudiantes.Visible = rol == Roles_Enum.Administrador || rol == Roles_Enum.Directiva || rol == Roles_Enum.Docente;

            MenuApoderados.Visible = rol == Roles_Enum.Administrador || rol == Roles_Enum.Directiva;
            MenuCursos.Visible = rol == Roles_Enum.Administrador || rol == Roles_Enum.Directiva;
            MenuPeriodos.Visible = rol == Roles_Enum.Administrador || rol == Roles_Enum.Directiva;
            MenuGeneros.Visible = rol == Roles_Enum.Administrador || rol == Roles_Enum.Directiva;
            MenuLibretas.Visible = rol == Roles_Enum.Administrador || rol == Roles_Enum.Directiva;
            MenuSecciones.Visible = rol == Roles_Enum.Administrador || rol == Roles_Enum.Directiva;
            MenuDetaApo.Visible = rol == Roles_Enum.Administrador || rol == Roles_Enum.Directiva;
            MenuPersonal.Visible = rol == Roles_Enum.Administrador || rol == Roles_Enum.Directiva;
            MenuGrado.Visible = rol == Roles_Enum.Administrador || rol == Roles_Enum.Directiva;
            MenuTipoPer.Visible = rol == Roles_Enum.Administrador || rol == Roles_Enum.Directiva;

            MenuRoles.Visible = rol == Roles_Enum.Administrador;
            MenuUsuarios.Visible = rol == Roles_Enum.Administrador;
            MenuEstUsu.Visible = rol == Roles_Enum.Administrador;
        }
    }
}