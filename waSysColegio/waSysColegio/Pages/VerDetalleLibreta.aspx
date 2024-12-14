<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VerDetalleLibreta.aspx.cs" Inherits="waSysColegio.Pages.VerDetalleLibreta" %>

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
                    <h3 class="form-title">Gestion de Detalle de la libreta</h3>
                    <div style="margin: 10px 0px;">
                        <asp:Button ID="btnRegistrarDetalleLibreta" runat="server" CssClass="btn btn-success" Text="&#10004; Validar" OnClick="btnRegistrarDetalleLibreta_Click" />
                        <asp:Button ID="btnVolverListarLibreta" runat="server" CssClass="btn btn-secondary ms-2" Text="&#11172; Regresar" OnClick="btnVolverListarLibreta_Click" />
                    </div>

                    <div class="table-responsive">
                        <asp:GridView ID="gvDetallesLibreta" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped table-hover" OnRowCommand="gvDetallesLibreta_RowCommand">
                            <Columns>
                                <asp:BoundField DataField="NombrePersonal" HeaderText="Personal" />
                                <asp:TemplateField HeaderText="Firma">
                                    <ItemTemplate>
                                        <div class="image-cell">
                                            <asp:Image ID="imgFirma" runat="server"
                                                ImageUrl='<%# Eval("Firma") != DBNull.Value ? "data:image/png;base64," + Convert.ToBase64String((byte[])Eval("Firma")) : "~/images/sin-firma.png" %>'
                                                AlternateText='<%# Eval("Firma") != DBNull.Value ? "Sin Firmar" : "Firma Disponible" %>'
                                                Visible="true" />
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sello">
                                    <ItemTemplate>
                                        <div class="image-cell">
                                            <asp:Image ID="imgSello" runat="server"
                                                ImageUrl='<%# Eval("Sello") != DBNull.Value ? "data:image/png;base64," + Convert.ToBase64String((byte[])Eval("Sello")) : "~/images/sin-sello.png" %>'
                                                AlternateText='<%# Eval("Sello") != DBNull.Value ? "Sin Sellar" : "Sello Disponible" %>'
                                                Visible="true" />
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Acciones">
                                    <ItemTemplate>
                                        <div class="btn-group">
                                            <asp:Button ID="btnEditar" runat="server" CommandName="Editar" CommandArgument='<%# Eval("ID_Libreta") + "," + Eval("ID_Personal") %>' Text="&#9998; Editar" CssClass="btn btn-primary" />
                                            <asp:Button ID="btnEliminar" runat="server" CommandName="Eliminar" CommandArgument='<%# Eval("ID_Libreta") + "," + Eval("ID_Personal") %>' Text="&#128465; Eliminar" CssClass="btn btn-danger borrar" OnClientClick="return confirm('¿Está seguro de que desea eliminar este registro?');" />
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
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
</html>

