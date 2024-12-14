<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VerTipoPersonal.aspx.cs" Inherits="waSysColegio.Pages.VerTipoPersonal" %>
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
                    <h3 class="form-title">Gestion de Tipo de Personal</h3>
                    <div style="margin: 10px 0px;">
                        <asp:Button ID="btnRegistrarTipoPersonal" runat="server" CssClass="btn btn-success" Text="&#10010; Nuevo Tipo Personal" OnClick="btnRegistrarTipoPersonal_Click" />                        
                    </div>

                    <div class="table-responsive">
                        <asp:GridView ID="gvTipoPersonal" runat="server" AutoGenerateColumns="False" class="table" OnRowCommand="gvTipoPersonal_RowCommand" OnSelectedIndexChanged="gvTipoPersonal_SelectedIndexChanged">
                            <Columns>
                                <asp:BoundField DataField="ID_Tipo_Personal" HeaderText="ID" ReadOnly="True" Visible="False" />
                                <asp:BoundField DataField="Nombre_Tipo_Personal" HeaderText="Nombre" />
                                <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                                <asp:TemplateField HeaderText="Acciones">
                                    <ItemTemplate>
                                        <div class="btn-group">
                                            <asp:Button ID="btnEditar" runat="server" CommandName="Editar" CommandArgument='<%# Eval("ID_Tipo_Personal") %>' Text="&#9998; Editar" CssClass="editar" />
                                            <asp:Button ID="btnEliminar" runat="server" CommandName="Eliminar" CommandArgument='<%# Eval("ID_Tipo_Personal") %>' Text="&#128465; Eliminar" CssClass="borrar" OnClientClick="return confirm('¿Está seguro de que desea eliminar este registro?');" />
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
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
    <script>
        // Mostrar el modal de éxito al guardar
        function showSuccessModal() {
            var modal = new bootstrap.Modal(document.getElementById('successModal'));
            modal.show();

            // Escuchar el evento de cierre del modal
            document.getElementById('successModal').addEventListener('hidden.bs.modal', function () {
                // Redirigir a la página de listado
                window.location.href = 'wfListarTipoPersonal.aspx';
            });
        }
    </script>
</html>
