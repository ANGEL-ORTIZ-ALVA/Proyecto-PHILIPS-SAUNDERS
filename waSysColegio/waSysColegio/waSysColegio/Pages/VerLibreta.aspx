<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VerLibreta.aspx.cs" Inherits="waSysColegio.Pages.VerLibreta" %>

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
    <title></title>
</head>
<body>
    <!--===== SIDEBAR =====-->
    <uc:Sidebar runat="server" />
    <!--FIN SIDEBAR-->

    <section class="home">
        <div class="text">
            <figure>
                <img src="../Resources/logo-school.png" width="50" height="50" />
            </figure>
            <h2>Colegio 'Philip P. Saunders'</h2>
        </div>
        <!--===== TABLA DE GESTION DE PERSONAL =====-->
        <div class="text">
            <form id="form1" runat="server">
                <div class="form-container">
                    <h3 class="form-title">Gestión de Libretas</h3>
                    <div style="margin: 10px 0px;">
                        <asp:Button ID="btnRegistrarLibreta" runat="server" CssClass="btn btn-success" Text="&#10010; Nueva Libreta" OnClick="btnRegistrarLibreta_Click" />
                    </div>
                    <div class="search-box">
                        <asp:TextBox ID="txtBuscar" runat="server" CssClass="input" placeholder="Buscar estudiante..." />
                        <asp:TextBox ID="txtAnioDesde" runat="server" CssClass="input" placeholder="Año Desde..." />
                        <asp:TextBox ID="txtAnioHasta" runat="server" CssClass="input" placeholder="Año Hasta..." />
                        <asp:Button ID="btnBuscar" runat="server" CssClass="btn btn-primary" Text="&#128269; Buscar" OnClick="btnBuscar_Click" />
                        <asp:Button ID="btnListarTodo" runat="server" CssClass="btn btn-secondary ms-2" Text="&#128214; Listar Todo" OnClick="btnListarTodo_Click" />
                    </div>
                    <div class="table-responsive">
                        <asp:GridView ID="gvLibretas" runat="server" AutoGenerateColumns="False" CssClass="tables" OnRowCommand="gvLibretas_RowCommand">
                            <Columns>
                                <asp:BoundField DataField="ID_Libreta" HeaderText="ID" ReadOnly="True" Visible="False" />
                                <asp:BoundField DataField="Anio_Escolar" HeaderText="Año Escolar" />
                                <asp:BoundField DataField="NombreCompletoEstudiante" HeaderText="Estudiante" />
                                <asp:TemplateField HeaderText="Acciones">
                                    <ItemTemplate>
                                        <div class="btn-group" role="group">
                                            <asp:Button ID="btnVer" runat="server" CommandName="Ver" CommandArgument='<%# Eval("ID_Libreta") %>' Text="&#128065; Ver" CssClass="btn btn-outline-info btn-sm" />
                                            <asp:Button ID="btnEditar" runat="server" CommandName="Editar" CommandArgument='<%# Eval("ID_Libreta") %>' Text="&#9998; Editar" CssClass="editar" />
                                            <asp:Button ID="btnEliminar" runat="server" CommandName="Eliminar" CommandArgument='<%# Eval("ID_Libreta") %>' Text="&#128465; Eliminar" CssClass="borrar" OnClientClick="return confirm('¿Está seguro de que desea eliminar esta libreta?');" />
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <asp:Label ID="lblNoRecords" runat="server" Text="No se encontraron registros." CssClass="no-records" Visible="False"></asp:Label>
                    </div>
                </div>
            </form>
        </div>
    </section>

    <!--====== FOOTER ======-->
    <uc:Footer runat="server" />
    <!--====== FIN FOOTER ======-->
</body>
</html>

