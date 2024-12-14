<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VerPersonal.aspx.cs" Inherits="waSysColegio.Pages.VerPersonal" %>

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

    <title>Listado de Personal</title>
</head>
<body>
    <!--===== SIDEBAR =====-->
    <uc:Sidebar runat="server" />
    <!--FIN SIDEBAR-->

    <!--****** CONTENIDO ******-->
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
                    <h3 class="form-title">Gestión de Personal</h3>
                    <div class="search-box">
                        <asp:TextBox ID="txtBuscar" runat="server" CssClass="input" placeholder="Buscar por Nombre / Apellido..." />
                        <asp:DropDownList ID="ddlTipoPersonal" runat="server" CssClass="input">
                            <asp:ListItem Text="Seleccione Tipo Personal" Value="" />
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlSeccion" runat="server" CssClass="input">
                            <asp:ListItem Text="Seleccione Sección" Value="" />
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlGrado" runat="server" CssClass="input">
                            <asp:ListItem Text="Seleccione Grado" Value="" />
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlGenero" runat="server" CssClass="input">
                            <asp:ListItem Text="Seleccione Género" Value="" />
                        </asp:DropDownList>
                        <asp:Button ID="btnBuscar" runat="server" CssClass="btn btn-primary me-2" Text="&#128269; Buscar" OnClick="btnBuscar_Click" />
                        <asp:Button ID="btnMostrarTodo" runat="server" CssClass="btn btn-secondary" Text="&#128214; Listar Todo" OnClick="btnMostrarTodo_Click" />
                    </div>

                     <asp:Button ID="btnRegistrar" runat="server" CssClass="btn btn-secondary" Text="&#128214; Registrar Personal" OnClick="btnRegistrarPersonal_Click" />

                    <div class="table-responsive">
                        <asp:GridView ID="gvPersonal" runat="server" AutoGenerateColumns="False" CssClass="table table-dark table-striped" OnRowCommand="gvPersonal_RowCommand">
                            <Columns>
                                <asp:BoundField DataField="ID_Personal" HeaderText="ID" ReadOnly="True" Visible="False" />
                                <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                                <asp:BoundField DataField="Apellido" HeaderText="Apellido" />
                                <asp:BoundField DataField="Fecha_Nacimiento" HeaderText="Fecha de Nacimiento" />
                                <asp:BoundField DataField="DNI" HeaderText="DNI" />
                                <asp:BoundField DataField="Correo" HeaderText="Correo" />
                                <asp:BoundField DataField="Telefono" HeaderText="Teléfono" />
                                <asp:BoundField DataField="Direccion" HeaderText="Dirección" />
                                <asp:BoundField DataField="Tipo Personal" HeaderText="Tipo Personal" />
                                <asp:BoundField DataField="Genero" HeaderText="Género" />
                                <asp:BoundField DataField="Grado" HeaderText="Grado" />
                                <asp:BoundField DataField="Seccion" HeaderText="Sección" />
                                <asp:BoundField DataField="Usuario" HeaderText="Usuario" />

                                <asp:TemplateField HeaderText="Acciones">
                                    <ItemTemplate>
                                        <div class="btn-group">
                                            <asp:Button ID="btnEditar" runat="server" CommandName="Editar" CommandArgument='<%# Eval("ID_Personal") %>' Text="&#9998; Editar" CssClass="btn btn-primary btn-sm me-2" />
                                            <asp:Button ID="btnEliminar" runat="server" CommandName="Eliminar" CommandArgument='<%# Eval("ID_Personal") %>' Text="&#128465; Eliminar" CssClass="btn btn-danger borrar" OnClientClick="return confirm('¿Está seguro de que desea eliminar este registro?');" />
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

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
</body>
<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/js/all.min.js"></script>

</html>

