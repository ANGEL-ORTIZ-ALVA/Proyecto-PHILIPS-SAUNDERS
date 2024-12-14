<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AgregarApoderado.aspx.cs" Inherits="waSysColegio.Pages.AgregarApoderado" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Agregar Apoderado</title>
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        body {
            background-color: #e0e7ff;
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
            margin: 0;
            font-family: 'Arial', sans-serif;
        }

        .container {
            border-radius: 10px;
            box-shadow: 0 6px 15px rgba(0, 0, 0, 0.1);
            padding: 40px;
            background-color: #ffffff;
            width: 100%;
            max-width: 600px;
            text-align: center;
        }

        h2 {
            color: #1e40af;
            margin-bottom: 25px;
        }

        .btn {
            background-color: #2563eb;
            color: #ffffff;
            font-weight: bold;
            padding: 10px;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            transition: background-color 0.3s, transform 0.2s;
            width: 100%;
            margin-top: 10px;
        }

            .btn:hover {
                background-color: #1d4ed8;
                transform: scale(1.05);
            }

        .btn-regresar {
            background-color: #d1d5db;
            color: #111827;
        }

            .btn-regresar:hover {
                background-color: #9ca3af;
            }

        .form-group {
            margin-bottom: 20px;
            text-align: left;
        }

            .form-group label {
                font-weight: bold;
                margin-bottom: 5px;
                display: block;
            }

        .form-control {
            width: 100%;
            padding: 8px;
            border: 1px solid #d1d5db;
            border-radius: 5px;
            font-size: 14px;
        }

            .form-control:focus {
                outline: none;
                border-color: #2563eb;
            }

        .error-message {
            color: red;
            font-size: 12px;
        }

        .form-row {
            display: flex;
            flex-wrap: wrap;
            justify-content: space-between;
        }

            .form-row .form-group {
                width: 48%;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2>Agregar Apoderado</h2>
            <div class="form-row">
                <div class="form-group">
                    <label for="txtNombre">Nombre:</label>
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" required="required"></asp:TextBox>
                </div>

                <div class="form-group">
                    <label for="txtApellido">Apellido:</label>
                    <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" required="required"></asp:TextBox>
                </div>
            </div>

            <div class="form-row">
                <div class="form-group">
                    <label for="txtDNI">DNI:</label>
                    <asp:TextBox ID="txtDNI" runat="server" CssClass="form-control" MaxLength="8"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="regexDNI" runat="server"
                        ControlToValidate="txtDNI"
                        ValidationExpression="^\d{8}$"
                        ErrorMessage="El DNI debe tener exactamente 8 dígitos."
                        ForeColor="Red" />
                    <asp:RequiredFieldValidator ID="reqDNI" runat="server"
                        ControlToValidate="txtDNI"
                        ErrorMessage="El DNI es obligatorio."
                        ForeColor="Red" />
                </div>

                <div class="form-group">
                    <label for="txtCorreo">Correo:</label>
                    <asp:TextBox ID="txtCorreo" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>

            <div class="form-row">
                <div class="form-group">
                    <label for="txtTelefono">Teléfono:</label>
                    <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="form-group">
                    <label for="txtDireccion">Dirección:</label>
                    <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>

            <div class="form-row">
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
            </div>

            <div class="form-group">
                <label for="ddlUsuario">Nombre de Usuario:</label>
                <asp:DropDownList ID="ddlUsuario" runat="server" CssClass="form-control"></asp:DropDownList>
            </div>

            <asp:Button ID="btnAgregar" runat="server" CssClass="btn btn-agregar" Text="Agregar" OnClick="btnAgregar_Click" />
            <asp:Button ID="btnRegresar" runat="server" CssClass="btn btn-regresar" Text="Regresar" OnClick="btnRegresar_Click" CausesValidation="false" />

            <asp:Label ID="lblMensaje" runat="server" ForeColor="Green" Visible="False"></asp:Label>
        </div>
    </form>
</body>
</html>


