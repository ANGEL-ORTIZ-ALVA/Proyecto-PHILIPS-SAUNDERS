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
    public partial class VerApoderado_Estudiante : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                listado();
            }
        }
        private void listado()
        {
            try
            {
                Apoderado_EstudianteDAO dao = new Apoderado_EstudianteDAO();
                DataTable dt = dao.ListarApoEstudiantes();
                rptApoEst.DataSource = dt;
                rptApoEst.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write($"Error al listar los datos: {ex.Message}");
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Response.Redirect("AgregarApoderado_Estudiante.aspx");
        }

        protected void rptApoEst_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string[] args = e.CommandArgument.ToString().Split(',');
            int idApoderado = Convert.ToInt32(args[0]);
            int idEstudiante = Convert.ToInt32(args[1]);

            if (e.CommandName == "Editar")
            {
                Response.Redirect($"EditarApoderado_Estudiante.aspx?ID_Apoderado={idApoderado}&ID_Estudiante={idEstudiante}");
            }
            else if (e.CommandName == "Eliminar")
            {
                try
                {
                    Apoderado_EstudianteDAO dao = new Apoderado_EstudianteDAO();
                    dao.EliminarApoEstudiante(idApoderado, idEstudiante);
                    listado();
                }
                catch (Exception ex)
                {
                    Response.Write($"Error al eliminar el registro: {ex.Message}");
                }
            }
        }
    }
}