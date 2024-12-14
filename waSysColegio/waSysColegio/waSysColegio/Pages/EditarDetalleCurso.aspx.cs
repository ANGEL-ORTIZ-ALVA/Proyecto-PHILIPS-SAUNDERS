using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using waSysColegio.Dao;
using waSysColegio.Models;

namespace waSysColegio.Pages
{
    public partial class EditarDetalleCurso : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                /*int idCurso = Convert.ToInt32(Request.QueryString["ID_Curso"]);
                int idEvaluacion = Convert.ToInt32(Request.QueryString["ID_Evaluacion"]);
                int idAsistencia = Convert.ToInt32(Request.QueryString["ID_Asistencia"]);
                int idPeriodo = Convert.ToInt32(Request.QueryString["ID_Periodo"]);
               */
                // Verificar si los parámetros de la URL existen
                if (Request.QueryString["ID_Estudiante"] != null &&
                    Request.QueryString["ID_Curso"] != null &&
                    Request.QueryString["ID_Evaluacion"] != null &&
                    Request.QueryString["ID_Asistencia"] != null &&
                    Request.QueryString["ID_Periodo"] != null)
                {
                    // Guardar los valores de las llaves compuestas
                    int idEstudiante = int.Parse(Request.QueryString["ID_Estudiante"]);
                    int idCurso = int.Parse(Request.QueryString["ID_Curso"]);
                    int idEvaluacion = int.Parse(Request.QueryString["ID_Evaluacion"]);
                    int idAsistencia = int.Parse(Request.QueryString["ID_Asistencia"]);
                    int idPeriodo = int.Parse(Request.QueryString["ID_Periodo"]);

                    llenarComboCurso();
                    llenarComboEvaluacion();
                    LlenarAsistencias();
                    LlenarPeriodos();
                    LlenarEstudiantes();
                    CargarDatosDetalleCurso(idEstudiante, idCurso, idEvaluacion, idAsistencia, idPeriodo);
                }

            }
        }
        private void llenarComboCurso()
        {
            CursoDAO dao = new CursoDAO();
            DataTable dt = dao.listarCurso();
            this.ddlCurso.DataSource = dt;
            this.ddlCurso.DataTextField = "Nombre_Curso";
            this.ddlCurso.DataValueField = "ID_Curso";
            this.ddlCurso.DataBind();
        }
        private void llenarComboEvaluacion()
        {
            EvaluacionDAO dao = new EvaluacionDAO();
            DataTable dt = dao.listarEvaluacion();
            ddlEvaluacion.DataSource = dt;
            ddlEvaluacion.DataTextField = "Nombre_Evaluacion";
            ddlEvaluacion.DataValueField = "ID_Evaluacion";
            ddlEvaluacion.DataBind();
        }

        private void LlenarAsistencias()
        {
            AsistenciaDAO dao = new AsistenciaDAO();
            DataTable dt = dao.listarAsistencia();
            ddlAsistencia.DataSource = dt;
            ddlAsistencia.DataTextField = "Nombre_Tipo_Asistencia";
            ddlAsistencia.DataValueField = "ID_Asistencia";
            ddlAsistencia.DataBind();
        }

        private void LlenarPeriodos()
        {
            PeriodoDAO dao = new PeriodoDAO();
            DataTable dt = dao.listarPeriodo();
            ddlPeriodo.DataSource = dt;
            ddlPeriodo.DataTextField = "Nombre_Periodo";
            ddlPeriodo.DataValueField = "ID_Periodo";
            ddlPeriodo.DataBind();
        }

        private void LlenarEstudiantes()
        {
            EstudianteDAO dao = new EstudianteDAO();
            DataTable dt = dao.listaestudiantes();
            ddlEstudiante.DataSource = dt;
            ddlEstudiante.DataTextField = "Nombre";
            ddlEstudiante.DataValueField = "ID_Estudiante";
            ddlEstudiante.DataBind();
        }
        private void CargarDatosDetalleCurso(int idEstudiante, int idCurso, int idEvaluacion, int idAsistencia, int idPeriodo)
        {
            DetalleCursoDAO dao = new DetalleCursoDAO();

            // Obtener el detalle del curso por ID
            DetalleCurso detalleCurso = dao.ObtenerDetalleCurso(idEstudiante, idCurso, idEvaluacion, idAsistencia, idPeriodo);

            if (detalleCurso != null)
            {
                llenarComboCurso();
                llenarComboEvaluacion();
                LlenarAsistencias();
                LlenarPeriodos();
                LlenarEstudiantes();

                // Seleccionar el valor en el DropDownList de Estudiante
                ddlEstudiante.SelectedValue = detalleCurso.ID_Estudiante.ToString();

                // Seleccionar el valor en el DropDownList de Evaluacion
                ddlEvaluacion.SelectedValue = detalleCurso.ID_Evaluacion.ToString();

                // Seleccionar el valor en el DropDownList de Curso
                ddlCurso.SelectedValue = detalleCurso.ID_Curso.ToString();

                // Seleccionar el valor en el DropDownList de Asistencia
                ddlAsistencia.SelectedValue = detalleCurso.ID_Asistencia.ToString();

                // Seleccionar el valor en el DropDownList de Periodo
                ddlPeriodo.SelectedValue = detalleCurso.ID_Periodo.ToString();

                // Asignar la nota al TextBox
                txtNota.Text = detalleCurso.Nota.ToString();

                // Seleccionar el valor en el DropDownList de Estado
                ddlEstado.SelectedValue = detalleCurso.Estado_Registro;
            }
            else
            {
                // Manejar el caso cuando no se encuentra el detalle del curso
                lblMensaje.Text = "No se encontró el detalle del curso.";
            }

        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            // Crear una instancia del DAO
            DetalleCursoDAO dao = new DetalleCursoDAO();

            // Crear un nuevo objeto DetalleCurso con los valores actualizados
            DetalleCurso detalle = new DetalleCurso
            {
                ID_Estudiante = int.Parse(ddlEstudiante.SelectedValue),
                ID_Curso = int.Parse(ddlCurso.SelectedValue),
                ID_Evaluacion = int.Parse(ddlEvaluacion.SelectedValue),
                ID_Asistencia = int.Parse(ddlAsistencia.SelectedValue),
                ID_Periodo = int.Parse(ddlPeriodo.SelectedValue),
                Nota = decimal.Parse(txtNota.Text),
                Estado_Registro = ddlEstado.SelectedValue
            };

            // Verificar si estamos en modo de edición
            if (Request.QueryString["ID_Estudiante"] != null &&
                Request.QueryString["ID_Curso"] != null &&
                Request.QueryString["ID_Evaluacion"] != null &&
                Request.QueryString["ID_Asistencia"] != null &&
                Request.QueryString["ID_Periodo"] != null)
            {
                // Crear un objeto DetalleCurso con los valores originales
                DetalleCurso originalDetalle = new DetalleCurso
                {
                    ID_Estudiante = int.Parse(Request.QueryString["ID_Estudiante"]),
                    ID_Curso = int.Parse(Request.QueryString["ID_Curso"]),
                    ID_Evaluacion = int.Parse(Request.QueryString["ID_Evaluacion"]),
                    ID_Asistencia = int.Parse(Request.QueryString["ID_Asistencia"]),
                    ID_Periodo = int.Parse(Request.QueryString["ID_Periodo"])
                };

                // Llamar al método de actualización
                bool resultado = dao.ActualizarDetalleCurso(detalle, originalDetalle);
                if (resultado)
                {
                    // Redirigir a la página VerDetalleCurso si la actualización es exitosa
                    Response.Redirect("VerDetalleCurso.aspx");
                }
                else
                {
                    lblMensaje.Text = "Error al actualizar el registro.";
                }

                lblMensaje.Text = resultado ? "Registro actualizado con éxito." : "Error al actualizar el registro.";
            }
            else
            {
                // Modo de creación
                bool resultado = dao.AgregarDetalleCurso(detalle);

                if (resultado)
                {
                    // Redirigir a la página VerDetalleCurso si el registro se agrega correctamente
                    Response.Redirect("VerDetalleCurso.aspx");
                }
                else
                {
                    lblMensaje.Text = "Error al agregar el registro.";
                }

                lblMensaje.Text = resultado ? "Registro agregado con éxito." : "Error al agregar el registro.";
            }

            LimpiarFormulario();
        }
        private void LimpiarFormulario()
        {
            ddlEstudiante.SelectedIndex = 0;
            ddlCurso.SelectedIndex = 0;
            ddlEvaluacion.SelectedIndex = 0;
            ddlAsistencia.SelectedIndex = 0;
            ddlPeriodo.SelectedIndex = 0;
            txtNota.Text = string.Empty;
            ddlEstado.SelectedIndex = 0;
        }
    }
}