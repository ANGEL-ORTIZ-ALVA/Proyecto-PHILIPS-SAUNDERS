<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="waSysColegio.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="shortcut icon" href="Resources/logo-school.png"/>
    <link rel="stylesheet" href="Styles/Login.css" />
    <title>Log in</title>
</head>
<body>
    <div class="login-container">
        <div class="login-header">
            <div>
                <img src="Resources/logo-school.png" width="60" height="60" />
            </div>
            <h1 style="text-align: center;">Colegio Philip P. Saunders</h1>
        </div>
        <form id="form1" runat="server">
            <div class="login-form-group">
                <asp:Label ID="lblUsuario" runat="server" Text="Usuario:" CssClass="label"></asp:Label>
                <asp:TextBox ID="txtUsuario" runat="server" CssClass="input" Required="true"></asp:TextBox>
            </div>
            <div class="login-form-group">
                <div style="display: flex; justify-content: space-between; align-items: center;">
                    <asp:Label ID="lblPassword" runat="server" Text="Password:"></asp:Label>
                    <a href="#">¿Olvidó su contraseña?</a>
                </div>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="input" Required="true"></asp:TextBox>
            </div>
            <div class="login-form-group">
                <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" CssClass="button" />
            </div>
            <div>
                <asp:Label ID="lblMensaje" runat="server" Text="" ForeColor="Red"></asp:Label>
            </div>
        </form>
        <div class="login-footer">
            ¿No tiene una cuenta?
          <strong>
              <a id="crear-cuenta" class="text-primary" href="Signup.aspx">Crear cuenta</a>
          </strong>
        </div>
    </div>
</body>
</html>
