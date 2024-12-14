<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="waSysColegio.Pages.Dashboard" %>
<!--FRACCION DEL SIDEBAR y FOOTER-->
<%@ Register Src="~/UserControls/Sidebar.ascx" TagName="Sidebar" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/Footer.ascx" TagName="Footer" TagPrefix="uc" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta charset="UTF-8" />
    <!-- Favicon -->
    <link rel="icon" type="image/png" href="../Resources/logo-school.png" />
    <link rel="shortcut icon" type="image/x-icon" href="favicon.ico" />

    <!----======== CSS ======== -->
    <link rel="stylesheet" href="../Styles/Dashboard.css" />
    <!----===== Boxicons CSS ===== -->
    <link href='https://unpkg.com/boxicons@2.1.4/css/boxicons.min.css' rel='stylesheet' />
    <title>Dashboard</title>
</head>
<body>
    <!--SIDEBAR-->
    <uc:Sidebar runat="server" />
    <!--FIN SIDEBAR-->
    <section class="home">
        <div class="text">
            <figure>
                <img src="../Resources/logo-school.png" width="50" height="50" />
            </figure>
            <h2>Colegio 'Philip P. Saunders'</h2>
            Lorem ipsum, dolor sit amet consectetur adipisicing elit. Odit quibusdam necessitatibus, laboriosam veniam
            quaerat est suscipit aspernatur rem itaque expedita vel iste nobis soluta aliquid aliquam placeat, ad,
            temporibus dolor.
        </div>
        <div class="text">Dashboard Sidebar</div>
    </section>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.7.1/jquery.min.js"></script>
    <!--====== FOOTER ======-->
    <uc:Footer runat="server" />
    <!--====== FIN FOOTER ======-->
</body>
</html>

