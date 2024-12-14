using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using waSysColegio.Dao;

namespace waSysColegio.Pages
{
    public partial class VerApoderados : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Verifica si el usuario está autenticado
            if (!User.Identity.IsAuthenticated)
            {
                // Si no está autenticado, redirigir a la página de login
                Response.Redirect("~/Login.aspx");
            }

            int idApoderado = Convert.ToInt32(Request.QueryString["ID_Apoderado"]);
            if (!IsPostBack)
            {
                listado();
            }
        }
        private void listado()
        {
            string connString = ConfigurationManager.ConnectionStrings["colegioBD"].ToString();

            string query = @"
            SELECT a.ID_Apoderado, a.Nombre, a.Apellido, a.DNI, a.Correo, a.Telefono, a.Direccion, a.Estado_Registro, 
                   g.Nombre_Genero, u.Nombre_Usuario
            FROM Apoderado a
            LEFT JOIN Genero g ON a.ID_Genero = g.ID_Genero
            LEFT JOIN Usuario u ON a.ID_Usuario = u.ID_Usuario";

            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();

                da.Fill(dt);

                string lastUserName = string.Empty;
                foreach (DataRow row in dt.Rows)
                {
                    string currentUser = row["Nombre_Usuario"].ToString();
                    if (currentUser == lastUserName)
                    {
                        row["Nombre_Usuario"] = currentUser + dt.Rows.IndexOf(row).ToString();
                    }
                    lastUserName = row["Nombre_Usuario"].ToString();
                }

                gvApo.DataSource = dt;
                gvApo.DataBind();
            }
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("AgregarApoderado.aspx");
        }
        protected void gvApo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Editar")
            {
                int idApoderado = Convert.ToInt32(e.CommandArgument);
                Response.Redirect($"EditarApoderado.aspx?ID_Apoderado={idApoderado}");
            }
            else if (e.CommandName == "EliminarApoderado")
            {
                int idApoderado = Convert.ToInt32(e.CommandArgument);
                ApoderadoDAO obj = new ApoderadoDAO();
                obj.eliminarApoderado(idApoderado);
                listado();
            }
        }
    }
}