using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;

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
    }
}