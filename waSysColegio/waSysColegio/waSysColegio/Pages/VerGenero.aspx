<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VerGenero.aspx.cs" Inherits="waSysColegio.Pages.VerGenero" %>

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

    <title>Generos</title>
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
                    <h3 class="form-title">Lista de Géneros</h3>
                    <div style="margin: 10px 0px;">
                        <asp:Button ID="btnNuevo" runat="server" CssClass="btn btn-success" Text="Nuevo Género" OnClick="btnNuevo_Click" />
                    </div>

                    <div class="table-responsive">
                        <asp:GridView ID="gvGeneros" runat="server" AutoGenerateColumns="False" OnRowCommand="gvGeneros_RowCommand" CssClass="table">
                            <Columns>
                                <asp:BoundField DataField="ID_Genero" HeaderText="ID" />
                                <asp:BoundField DataField="Nombre_Genero" HeaderText="Nombre del Género" />
                                <asp:BoundField DataField="Estado_Registro" HeaderText="Estado Registro" />
                                <asp:TemplateField HeaderText="Acciones">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEditar" runat="server" CssClass="editar"
                                            CommandName="Editar"
                                            CommandArgument='<%# Eval("ID_Genero") %>' Text="Editar" />
                                        &nbsp;|&nbsp;
                                        <asp:LinkButton ID="btnEliminar" runat="server" CssClass="borrar"
                                            CommandName="EliminarGenero"
                                            CommandArgument='<%# Eval("ID_Genero") %>'
                                            OnClientClick="return confirm('¿Estás seguro de que deseas eliminar este género?');"
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
</html>
