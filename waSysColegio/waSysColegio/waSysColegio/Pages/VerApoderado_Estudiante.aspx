<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VerApoderado_Estudiante.aspx.cs" Inherits="waSysColegio.Pages.VerApoderado_Estudiante" %>

<!--FRACCION DEL SIDEBAR y FOOTER-->
<%@ Register Src="~/UserControls/Sidebar.ascx" TagName="Sidebar" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/Footer.ascx" TagName="Footer" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/Head_Content.ascx" TagName="Head_Content" TagPrefix="uc" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />

    <!--HEAD CONTENT-->
    <uc:Head_Content runat="server" />
    <!--FIN  HEAD CONTENT-->

    <title>Relación Apoderado-Estudiante</title>
    <style> 
            /*===== Opcion a cambiar el encabezado de la tabla =====*/
            .table th {
                background-color: #008fe6;
                color: white;
            }

    </style>
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
            <form id="form2" runat="server">
                <div class="form-container">
                    <h3 class="form-title">Relación Apoderado - Estudiante</h3>
                    <div style="margin: 10px 0px;">
                        <asp:Button ID="btnAgregar" runat="server" CssClass="btn btn-success" Text="Agregar Relación" OnClick="btnAgregar_Click" />
                    </div>

                    <div class="table-responsive">
                        <table class="table">
                            <thead>
                                <tr>
                                    <th>ID Apoderado</th>
                                    <th>ID Estudiante</th>
                                    <th>Parentesco</th>
                                    <th>Estado Registro</th>
                                    <th>Acciones</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="rptApoEst" runat="server" OnItemCommand="rptApoEst_ItemCommand">
                                    <ItemTemplate>
                                        <tr>
                                            <td><%# Eval("ID_Apoderado") %></td>
                                            <td><%# Eval("ID_Estudiante") %></td>
                                            <td><%# Eval("Parentesco") %></td>
                                            <td><%# Eval("Estado_Registro") %></td>
                                            <td>
                                                <asp:Button ID="btnEditar" runat="server" CssClass="editar" Text="Editar"
                                                    CommandName="Editar" CommandArgument='<%# Eval("ID_Apoderado") + "," + Eval("ID_Estudiante") %>' />
                                                <asp:Button ID="btnEliminar" runat="server" CssClass="borrar" Text="Eliminar"
                                                    CommandName="Eliminar" CommandArgument='<%# Eval("ID_Apoderado") + "," + Eval("ID_Estudiante") %>'
                                                    OnClientClick="return confirmarEliminacion();" />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                    </div>
                </div>
            </form>
        </div>
    </section>

    <!--====== FOOTER ======-->
    <uc:Footer runat="server" />
    <!--====== FIN FOOTER ======-->

    <script type="text/javascript">
        function confirmarEliminacion() {
            return confirm('¿Estás seguro de que deseas eliminar esta relación?');
        }
    </script>
</body>
</html>


