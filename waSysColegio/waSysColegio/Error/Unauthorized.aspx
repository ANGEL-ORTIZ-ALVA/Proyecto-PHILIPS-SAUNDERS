<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Unauthorized.aspx.cs" Inherits="waSysColegio.Error.Unauthorized" %>

<!DOCTYPE html>
<html>
<head>
    <title>Acceso Denegado</title>
    <link rel="stylesheet" href="../Styles/Unauthorized.css" />
</head>
<body>
    <div class="container">
        <div class="copy-container center-xy">
            <div>
                <img src="../Resources/error_401.jpg" width="600" height="450" />
            </div>
            <p>
                401, acceso no autorizado.
            </p>
            <div class="link-cont">
                <a class="link" href="/Pages/Dashboard.aspx">Volver al inicio</a>
            </div>
        </div>
    </div>

</body>
</html>