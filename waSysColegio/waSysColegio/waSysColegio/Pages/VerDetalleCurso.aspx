<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VerDetalleCurso.aspx.cs" Inherits="waSysColegio.Pages.VerDetalleCurso" %>
<!--FRACCION DEL SIDEBAR y FOOTER-->
<%@ Register Src="~/UserControls/Sidebar.ascx" TagName="Sidebar" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/Footer.ascx" TagName="Footer" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/Head_Content.ascx" TagName="Head_Content" TagPrefix="uc" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    
    <!--HEAD CONTENT-->
    <uc:Head_Content runat="server" />
    <!--FIN HEAD CONTENT-->
    
    <title>VER DETALLES DE CURSO</title>
</head>
<body class="container-fluid">

    <!--===== SIDEBAR =====-->
    <uc:Sidebar runat="server" />
    <!--FIN SIDEBAR-->

    <!--===== CONTENIDO =====-->
    <section class="home">
        <div class="text">
            <figure>
                <img src="../Resources/logo-school.png" width="50" height="50" />
            </figure>
            <h2>Colegio 'Philip P. Saunders'</h2>
        </div>
        <!--===== TABLA DE GASTION DE DETALLE DE CURSOS =====-->
        <div class="text">
            <h2 class=""><strong>REGISTRO DE NOTAS</strong></h2>
            <form id="form1" runat="server">
                <div class="form-container">
                        <h3 class="form-title">Detalle de cursos</h3>
                        <p>Interfaz grafica con acceso a las funciones de Leer,Registrar, Editar y Anular</p>
                        <div class="search-box">
                            <asp:TextBox ID="txtBuscar" runat="server" placeholder="Buscar por Curso, Evaluación o Asistencia" CssClass="input"></asp:TextBox>
                            <asp:Button ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" CssClass="btn btn-primary" />
                        </div>
                        <br />
                        <div class="table-responsive">
                            <asp:GridView ID="gvDetalleCurso" runat="server" AutoGenerateColumns="False"
                                DataKeyNames="ID_Estudiante,ID_Curso,ID_Evaluacion,ID_Asistencia,ID_Periodo"
                                OnRowDeleting="gvDetalleCurso_RowDeleting" AllowPaging="True" PageSize="10"
                                OnPageIndexChanging="gvDetalleCurso_PageIndexChanging" CssClass="table" Width="100%" CellSpacing="0">
                                <Columns>
                                    <asp:BoundField DataField="ID_Curso" HeaderText="ID Curso" ReadOnly="True" />
                                    <asp:BoundField DataField="NombreCompleto" HeaderText="Estudiante" />
                                    <asp:BoundField DataField="Nombre_Curso" HeaderText="Curso" />
                                    <asp:BoundField DataField="Nombre_Evaluacion" HeaderText="Evaluación" />
                                    <asp:BoundField DataField="Nombre_Tipo_Asistencia" HeaderText="Asistencia" />
                                    <asp:BoundField DataField="Nombre_Periodo" HeaderText="Periodo" />
                                    <asp:BoundField DataField="Nota" HeaderText="Nota" />
                                    <asp:BoundField DataField="Estado_Registro" HeaderText="Estado" />


                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:HyperLink ID="lnkEditar" runat="server" Text="Editar" CssClass="editar"
                                                NavigateUrl='<%# "EditarDetalleCurso.aspx?ID_Estudiante=" + Eval("ID_Estudiante") + "&ID_Curso=" + Eval("ID_Curso") + "&ID_Evaluacion=" + Eval("ID_Evaluacion") + "&ID_Asistencia=" + Eval("ID_Asistencia") + "&ID_Periodo=" + Eval("ID_Periodo") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:CommandField ShowDeleteButton="True" DeleteText="Borrar" ControlStyle-CssClass="borrar"/>
                                </Columns>
                            </asp:GridView>
                        </div>

                    <asp:Button ID="btnNuevoRegistro" runat="server" Text="Nuevo Registro" OnClick="btnNuevoRegistro_Click" CssClass="btn btn-success"/>
                </div>
            </form>
        </div>
    </section>

    <!--====== FOOTER ======-->
    <uc:Footer runat="server" />
    <!--====== FIN FOOTER ======-->

</body>

<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/js/all.min.js"></script>

</html>


