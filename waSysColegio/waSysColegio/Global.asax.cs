using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;
using waSysColegio.Models;

namespace waSysColegio
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Código que se ejecuta al iniciar la aplicación
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            // Mapeo para jQuery, asegurándose de que jQuery esté disponible para la validación discreta.
            ScriptManager.ScriptResourceMapping.AddDefinition(
                "jquery",
                new ScriptResourceDefinition
                {
                    Path = "~/Scripts/jquery-3.6.0.min.js", // Cambia esto si jQuery está en una ubicación diferente
                    DebugPath = "~/Scripts/jquery-3.6.0.js", // Ruta para la versión no minificada (para depuración)
                    CdnPath = "https://code.jquery.com/jquery-3.6.0.min.js", // Opción para cargar desde CDN
                    CdnDebugPath = "https://code.jquery.com/jquery-3.6.0.js", // CDN para la versión de depuración
                    CdnSupportsSecureConnection = true, // Usa HTTPS
                    LoadSuccessExpression = "window.jQuery" // Verifica si jQuery está cargado
                });
        }

        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            // Obtener la URL solicitada
            string requestedUrl = HttpContext.Current.Request.Url.AbsolutePath;

            // Excluir páginas públicas como Login
            if (requestedUrl.Contains("/Login.aspx") || requestedUrl.Contains("/Error"))
            {
                return;
            }
            if (HttpContext.Current.Session == null)
            {
                // Manejo de error o redirección si la sesión es null
                Response.Redirect("~/Login.aspx");
            }

            // Verificar si existe un rol en la sesión
            var userRole = HttpContext.Current.Session["Rol"] != null
            ? (Roles_Enum?)Convert.ToInt32(HttpContext.Current.Session["Rol"])
            : (Roles_Enum?)null;

            if (!userRole.HasValue)
            {
                // Manejo de error si el rol es null
                Response.Redirect("~/Login.aspx");
            }

            if (userRole == null)
            {
                // Si no hay sesión, redirigir a la página de inicio de sesión
                HttpContext.Current.Response.Redirect("~/Login.aspx");
            }

            // Reglas de acceso para rutas basadas en los menús
            if (requestedUrl.Contains("/Notas/"))
            {
                // Rutas relacionadas con "MenuNotas"
                if (userRole != Roles_Enum.Administrador &&
                    userRole != Roles_Enum.Directiva &&
                    userRole != Roles_Enum.Docente &&
                    userRole != Roles_Enum.Apoderado &&
                    userRole != Roles_Enum.Estudiante)
                {
                    HttpContext.Current.Response.Redirect("~/Error/Unauthorized.aspx");
                }
            }
            else if (requestedUrl.Contains("/Seccion/"))
            {
                // Rutas relacionadas con "MenuCalificaciones"
                if (userRole != Roles_Enum.Administrador &&
                    userRole != Roles_Enum.Directiva &&
                    userRole != Roles_Enum.Docente)
                {
                    HttpContext.Current.Response.Redirect("~/Error/Unauthorized.aspx");
                }
            }
            else if (requestedUrl.Contains("/Estudiante/"))
            {
                // Rutas relacionadas con "MenuEstudiantes"
                if (userRole != Roles_Enum.Administrador &&
                    userRole != Roles_Enum.Directiva &&
                    userRole != Roles_Enum.Docente)
                {
                    HttpContext.Current.Response.Redirect("~/Error/Unauthorized.aspx");
                }
            }
            else if (requestedUrl.Contains("/Apoderado/"))
            {
                // Rutas relacionadas con "MenuApoderados"
                if (userRole != Roles_Enum.Administrador &&
                    userRole != Roles_Enum.Directiva)
                {
                    HttpContext.Current.Response.Redirect("~/Error/Unauthorized.aspx");
                }
            }
            else if (requestedUrl.Contains("/Curso/"))
            {
                // Rutas relacionadas con "MenuCursos"
                if (userRole != Roles_Enum.Administrador &&
                    userRole != Roles_Enum.Directiva)
                {
                    HttpContext.Current.Response.Redirect("~/Error/Unauthorized.aspx");
                }
            }
            else if (requestedUrl.Contains("/Periodo/"))
            {
                // Rutas relacionadas con "MenuPeriodos"
                if (userRole != Roles_Enum.Administrador &&
                    userRole != Roles_Enum.Directiva)
                {
                    HttpContext.Current.Response.Redirect("~/Error/Unauthorized.aspx");
                }
            }
            else if (requestedUrl.Contains("/Genero/"))
            {
                // Rutas relacionadas con "MenuGenero"
                if (userRole != Roles_Enum.Administrador &&
                    userRole != Roles_Enum.Directiva)
                {
                    HttpContext.Current.Response.Redirect("~/Error/Unauthorized.aspx");
                }
            }
            else if (requestedUrl.Contains("/Libreta/"))
            {
                // Rutas relacionadas con "MenuLibretas"
                if (userRole != Roles_Enum.Administrador &&
                    userRole != Roles_Enum.Directiva)
                {
                    HttpContext.Current.Response.Redirect("~/Error/Unauthorized.aspx");
                }
            }
            else if (requestedUrl.Contains("/Seccion/"))
            {
                // Rutas relacionadas con "MenuSecciones"
                if (userRole != Roles_Enum.Administrador &&
                    userRole != Roles_Enum.Directiva)
                {
                    HttpContext.Current.Response.Redirect("~/Error/Unauthorized.aspx");
                }
            }
            else if (requestedUrl.Contains("/Apoderado_Estudiante/"))
            {
                // Rutas relacionadas con "MenuDetaApo"
                if (userRole != Roles_Enum.Administrador &&
                    userRole != Roles_Enum.Directiva)
                {
                    HttpContext.Current.Response.Redirect("~/Error/Unauthorized.aspx");
                }
            }
            else if (requestedUrl.Contains("/Pages/VerPersonal.aspx") || requestedUrl.Contains("/Pages/EditarPersonal.aspx") || requestedUrl.Contains("/Pages/AgregarPersonal.aspx"))
            {
                // Rutas relacionadas con "MenuPersonal"
                if (userRole != Roles_Enum.Administrador &&
                    userRole != Roles_Enum.Directiva)
                {
                    HttpContext.Current.Response.Redirect("~/Error/Unauthorized.aspx");
                }
            }
            else if (requestedUrl.Contains("/Grado/"))
            {
                // Rutas relacionadas con "MenuGrado"
                if (userRole != Roles_Enum.Administrador &&
                    userRole != Roles_Enum.Directiva)
                {
                    HttpContext.Current.Response.Redirect("~/Error/Unauthorized.aspx");
                }
            }
            else if (requestedUrl.Contains("/Pages/VerTipoPersonal.aspx") || requestedUrl.Contains("/Pages/AgregarTipoPersonal.aspx") || requestedUrl.Contains("/Pages/EditarPersonal.aspx"))
            {
                // Rutas relacionadas con "MenuTipoPer"
                if (userRole != Roles_Enum.Administrador &&
                    userRole != Roles_Enum.Directiva)
                {
                    HttpContext.Current.Response.Redirect("~/Error/Unauthorized.aspx");
                }
            }
            else if (requestedUrl.Contains("/Rol/"))
            {
                // Rutas relacionadas con "MenuRoles"
                if (userRole != Roles_Enum.Administrador &&
                    userRole != Roles_Enum.Directiva)
                {
                    HttpContext.Current.Response.Redirect("~/Error/Unauthorized.aspx");
                }
            }
            else if (requestedUrl.Contains("/Usuario/"))
            {
                // Rutas relacionadas con "MenuUsuarios"
                if (userRole != Roles_Enum.Administrador &&
                    userRole != Roles_Enum.Directiva)
                {
                    HttpContext.Current.Response.Redirect("~/Error/Unauthorized.aspx");
                }
            }
            else if (requestedUrl.Contains("/Estado_Usuario/"))
            {
                // Rutas relacionadas con "MenuEstUsu"
                if (userRole != Roles_Enum.Administrador &&
                    userRole != Roles_Enum.Directiva)
                {
                    HttpContext.Current.Response.Redirect("~/Error/Unauthorized.aspx");
                }
            }
        }
    }
}