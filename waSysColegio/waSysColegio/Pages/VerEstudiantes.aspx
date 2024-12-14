<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VerEstudiantes.aspx.cs" Inherits="waSysColegio.Pages.VerEstudiantes" %>
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
        <!--===== TABLA DE GESTION DE ESTUDIANTES =====-->
        <div class="text">
            <form id="form1" runat="server">
                <div class="form-container">
                    <h3 class="form-title">Lista de Estudiantes</h3>
                    <div style="margin: 10px 10px;">
                        <asp:Button ID="btnNuevo" runat="server" CssClass="btn btn-success" Text="Nuevo Estudiante" OnClick="btnNuevo_Click" />
                    </div>
                    <div class="table-responsive">

                        <asp:GridView ID="gvEs" runat="server" AutoGenerateColumns="False" OnRowCommand="gvApo_RowCommand" CssClass="table" Width="100%" CellSpacing="0" OnSelectedIndexChanged="gvApo_SelectedIndexChanged">
                            <Columns>
                                <asp:BoundField DataField="ID_Estudiante" HeaderText="ID" />
                                <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                                <asp:BoundField DataField="Apellido" HeaderText="Apellido" />
                                <asp:BoundField DataField="Fecha_Nacimiento" HeaderText="Fecha Nacimiento" />
                                <asp:BoundField DataField="DNI" HeaderText="Dni" />
                                <asp:BoundField DataField="Estado_Registro" HeaderText="Estado Registro" />
                                <asp:BoundField DataField="Nombre_Genero" HeaderText="Género" />
                                <asp:BoundField DataField="Direccion" HeaderText="Direccion" />
                                <asp:BoundField DataField="Numero_Grado" HeaderText="Grado" />
                                <asp:BoundField DataField="Nombre_Seccion" HeaderText="Seccion" />
                                <asp:BoundField DataField="Nombre_Usuario" HeaderText="Usuario" />
                                <asp:TemplateField HeaderText="Acciones">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEditar" runat="server" CssClass="editar"
                                            CommandName="Editar"
                                            CommandArgument='<%# Eval("ID_Estudiante") %>'
                                            Text="Editar" />
                                        &nbsp;|&nbsp;
    <asp:LinkButton ID="btnEliminar" runat="server" ControlStyle-CssClass="borrar"
        CommandName="EliminarEstudiante"
        CommandArgument='<%# Eval("ID_Estudiante") %>'
        OnClientClick="return confirm('¿Estás seguro de que deseas eliminar este estudiante?');"
        Text="Eliminar" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
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


