<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AgregarApoderado.aspx.cs" Inherits="waSysColegio.Pages.AgregarApoderado" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Agregar Apoderado</title>
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
    <style>
    body {
        background-color: #f0f4f8; 
        display: flex;
        justify-content: center;
        align-items: center; 
        transform: scale(0.9);
    }
    .container {
        border-radius: 10px;
        box-shadow: 0 4px 20px rgba(0, 0, 0, 0.1);
        padding: 20px; 
        background-color: #ffffff; 
        width: 400px; 
    }
    h2 {
        color: #02666a; 
        margin-bottom: 20px; 
        text-align: center; 
    }
    .btn-agregar {
        background-color: #28a745;
        color: white;
        font-weight: bold; 
        transition: background-color 0.3s, transform 0.2s; 
        width: 100%; 
    }
    .btn-agregar:hover {
        background-color: #218838;
        transform: scale(1.05); 
    }
    .btn-regresar {
        background-color: #007bff; 
        color: white;
        font-weight: bold;
        transition: background-color 0.3s, transform 0.2s;
        width: 100%; 
        margin-top: 10px; 
    }
    .btn-regresar:hover {
        background-color: #0056b3; 
        transform: scale(1.05); 
    }
    .form-group {
        display: flex;
        flex-direction: column; 
        align-items: center; 
        margin-bottom: 5px; 
    }
    .form-group label {
        font-weight: bold;
        text-align: center;
        width: 100%;  
        margin-bottom: 2px; 
    }
    .form-control {
        width: 100%; 
        text-align: center; 
        padding: 5px; 
        font-size: 14px; 
    }
</style>

</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2>Agregar Apoderado</h2>


            <div class="form-group">
                <label for="txtNombre">Nombre:</label>
                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" required="required"></asp:TextBox>
                <asp:RequiredFieldValidator ID="reqNombre" runat="server" 
                    ControlToValidate="txtNombre" 
                    ErrorMessage="El nombre es obligatorio." 
                    ForeColor="Red" />
            </div>


            <div class="form-group">
                <label for="txtApellido">Apellido:</label>
                <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" required="required"></asp:TextBox>
                <asp:RequiredFieldValidator ID="reqApellido" runat="server" 
                    ControlToValidate="txtApellido" 
                    ErrorMessage="El apellido es obligatorio." 
                    ForeColor="Red" />
            </div>



                <div class="form-group">
                    <label for="txtDNI">DNI:</label>
                    <asp:TextBox ID="txtDNI" runat="server" CssClass="form-control" MaxLength="8"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqDNI" runat="server" 
                        ControlToValidate="txtDNI" 
                        ErrorMessage="El DNI es obligatorio." 
                        ForeColor="Red" />
                    <asp:RegularExpressionValidator ID="regexDNI" runat="server" 
                        ControlToValidate="txtDNI" 
                        ValidationExpression="^\d{8}$" 
                        ErrorMessage="El DNI debe tener exactamente 8 dígitos." 
                        ForeColor="Red" />
                </div>


                <div class="form-group">
                    <label for="txtCorreo">Correo:</label>
                    <asp:TextBox ID="txtCorreo" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqCorreo" runat="server" 
                        ControlToValidate="txtCorreo" 
                        ErrorMessage="El correo es obligatorio." 
                        ForeColor="Red" />
                    <asp:RegularExpressionValidator ID="regexCorreo" runat="server" 
                        ControlToValidate="txtCorreo" 
                        ValidationExpression="^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$" 
                        ErrorMessage="Ingrese un correo válido." 
                        ForeColor="Red" />
                </div>
    


            <div class="form-group">
                <label for="txtTelefono">Teléfono:</label>
                <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="reqTelefono" runat="server" 
                    ControlToValidate="txtTelefono" 
                    ErrorMessage="El teléfono es obligatorio." 
                    ForeColor="Red" />
                <asp:RegularExpressionValidator ID="regexTelefono" runat="server" 
                    ControlToValidate="txtTelefono" 
                    ValidationExpression="^\d{9}$" 
                    ErrorMessage="El teléfono debe tener exactamente 9 dígitos." 
                    ForeColor="Red" />
            </div>


            <div class="form-group">
                <label for="txtDireccion">Dirección:</label>
                <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="reqDireccion" runat="server" 
                    ControlToValidate="txtDireccion" 
                    ErrorMessage="La dirección es obligatoria." 
                    ForeColor="Red" />
            </div>


            <div class="form-group">
                <label for="ddlGenero">Género:</label>
                <asp:DropDownList ID="ddlGenero" runat="server" CssClass="form-control">
                    <asp:ListItem Value="1">Masculino</asp:ListItem>
                    <asp:ListItem Value="2">Femenino</asp:ListItem>
                </asp:DropDownList>
            </div>


            <div class="form-group">
                <label for="ddlEstadoRegistro">Estado de Registro:</label>
                <asp:DropDownList ID="ddlEstadoRegistro" runat="server" CssClass="form-control">
                    <asp:ListItem Value="Registrado">Registrado</asp:ListItem>
                    <asp:ListItem Value="Eliminado">Eliminado</asp:ListItem>
                </asp:DropDownList>
            </div>


            <div class="form-group">
                <label for="ddlUsuario">Nombre de Usuario:</label>
                <asp:DropDownList ID="ddlUsuario" runat="server" CssClass="form-control"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="reqUsuario" runat="server" 
                    ControlToValidate="ddlUsuario" 
                    InitialValue="" 
                    ErrorMessage="Seleccione un usuario." 
                    ForeColor="Red" />
            </div>


            <asp:Button ID="btnAgregar" runat="server" CssClass="btn btn-agregar" Text="Agregar" OnClick="btnAgregar_Click" />
            <asp:Button ID="btnRegresar" runat="server" CssClass="btn btn-regresar" Text="Regresar" OnClick="btnRegresar_Click" />
            <asp:Label ID="lblMensaje" runat="server" ForeColor="Green" Visible="False"></asp:Label>
        </div>
    </form>
</body>
</html>


