<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Footer.ascx.cs" Inherits="waSysColegio.UserControls.Footer" %>
<script src="../Scripts/Dashboard.js"></script>
<script>
    function logout() {
        if (confirm("¿Estás seguro de que deseas cerrar sesión?")) {
            // Eliminar la cookie de sesión en el cliente
            document.cookie = "ASP.NET_SessionId=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;";
            document.cookie = ".ASPXAUTH=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;";

            // Redirigir a la página de logout en el servidor
            window.location.href = "../Logout.aspx";
            alert("Sesión cerrada con éxito");
        } else{
            window.location.href = "Dashboard.aspx";
        }
    }
</script>