<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Signup.aspx.cs" Inherits="waSysColegio.Signup" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="stylesheet" href="Styles/Login.css" />
    <title>Sign Up</title>
</head>
<body>
    <div class="signup-container">
        <div class="signup-header">
            <div>
                <img src="Resources/logo-school.png" width="60" height="60" />
            </div>
            <h1 style="text-align:center;">Colegio Philip P. Saunders</h1>
        </div>
        <h2 style="text-align: center; color: #fff">Registro</h2>
        <form id="form1" runat="server">
            <div class="signup-form-group">
                <asp:Label ID="lblNombreUsuario" runat="server" Text="Nombre de usuario:" CssClass="label"></asp:Label>
                <asp:TextBox ID="txtNombreUsuario" runat="server" CssClass="input" Required="true"></asp:TextBox>
            </div>
            <div class="signup-form-group">
                <div style="display: flex; justify-content: space-between; align-items: center;">
                    <asp:Label ID="lblPassword" runat="server" Text="Password:" CssClass="label"></asp:Label>
                </div>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="input" Required="true"></asp:TextBox>
            </div>
            <div class="signup-form-group">
                <asp:Label ID="lblConfirmPassword" runat="server" Text="Confirmar password:" CssClass="label"></asp:Label>
                <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" CssClass="input" Required="true"></asp:TextBox>
            </div>
            <div class="signup-form-group">
                <div style="display: flex; justify-content: space-between; align-items: center;">
                    <asp:Label ID="lblRol" runat="server" Text="Rol:" CssClass="label"></asp:Label>
                </div>
                <asp:DropDownList ID="ddlRol" runat="server" CssClass="select"></asp:DropDownList>
            </div>
            <div class="signup-form-group">
                <asp:Button ID="btnRegistrar" runat="server" Text="Registrar" OnClick="btnRegistrar_Click" CssClass="button"/>
            </div>
            <div class="signup-footer">
                ¿Ya tiene una cuenta?
                <strong>
                    <a id="ingresar" class="text-primary" href="Login.aspx">Ingresar</a>
                </strong>
            </div>
            <div>
                <asp:Label ID="lblMensaje" runat="server" Text="" ForeColor="Red"></asp:Label>
            </div>
        </form>
    </div>
</body>
</html>
