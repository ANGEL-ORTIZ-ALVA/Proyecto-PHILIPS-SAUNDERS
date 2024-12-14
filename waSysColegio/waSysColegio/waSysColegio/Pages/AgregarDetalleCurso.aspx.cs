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
    public partial class AgregarDetalleCurso : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                llenarComboCurso();
                llenarComboEvaluacion();
                LlenarAsistencias();
                LlenarPeriodos();
                LlenarEstudiantes();
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
            DataTable dt = dao.ListarEstudiantesRegistrados();
            ddlEstudiante.DataSource = dt;
            ddlEstudiante.DataTextField = "Nombre";
            ddlEstudiante.DataValueField = "ID_Estudiante";
            ddlEstudiante.DataBind();
        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
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

            DetalleCursoDAO dao = new DetalleCursoDAO();
            if (dao.AgregarDetalleCurso(detalle))
            {
                Response.Write("<script>alert('Detalle del curso agregado exitosamente');</script>");
                Response.Redirect("VerDetalleCurso.aspx");
            }
            else
            {
                Response.Write("<script>alert('Error al agregar el detalle');</script>");
            }
        }
    }
}