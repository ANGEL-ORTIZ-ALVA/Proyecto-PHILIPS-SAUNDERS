<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="waPlantillaVerDash.aspx.cs" Inherits="waSysColegio.Pages.waPlantillaVer" %>

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
                    <h3 class="form-title">Titulo</h3>
                    <div style="margin: 10px 0px;">
                        <asp:Button ID="btnRegistrarPersonal" runat="server" CssClass="btn btn-success" Text="Registrar Personal" OnClick="btnRegistrarPersonal_Click" />
                    </div>

                    <div class="table-responsive">
                        <!--******* GRIDVIEW *******-->
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

