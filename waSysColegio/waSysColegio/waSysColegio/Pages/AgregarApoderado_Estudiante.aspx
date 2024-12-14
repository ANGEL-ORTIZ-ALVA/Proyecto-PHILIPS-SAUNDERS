<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AgregarApoderado_Estudiante.aspx.cs" Inherits="waSysColegio.Pages.AgregarApoderado_Estudiante" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Agregar Relación Apoderado - Estudiante</title>
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        body {
            background-color: #e2f3ef;
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
            margin: 0;
        }

        .container {
            border-radius: 10px;
            box-shadow: 0 4px 20px rgba(0, 0, 0, 0.1);
            padding: 30px;
            background-color: #ffffff;
            width: 400px;
        }

        h2 {
            color: #02666a;
            margin-bottom: 30px;
            text-align: center;
        }

        .btn-agregar {
            background-color: #1de356;
            color: white;
            font-weight: bold;
            transition: background-color 0.3s, transform 0.2s;
            width: 100%;
        }

        .btn-agregar:hover {
            background-color: #68d19d;
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
            margin-bottom: 15px;
        }

        .form-group label {
            font-weight: bold;
            text-align: center;
            width: 100%;
        }

        .form-control {
            width: 100%;
            text-align: center;
        }

        .dropdown {
            width: 100%;
        }

    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2>Agregar Apoderado - Estudiante</h2>

            <div class="form-group">
                <label for="ddlApoderado">Seleccione Apoderado:</label>
                <asp:DropDownList ID="ddlApoderado" runat="server" CssClass="form-control"></asp:DropDownList>
            </div>

            <div class="form-group">
                <label for="ddlEstudiante">Seleccione Estudiante:</label>
                <asp:DropDownList ID="ddlEstudiante" runat="server" CssClass="form-control"></asp:DropDownList>
            </div>

            <div class="form-group">
                <label for="ddlParentesco">Parentesco:</label>
                <asp:DropDownList ID="ddlParentesco" runat="server" CssClass="form-control">
                    <asp:ListItem Text="Seleccione Parentesco" Value="" />
                    <asp:ListItem Text="Madre" Value="Madre" />
                    <asp:ListItem Text="Padre" Value="Padre" />
                    <asp:ListItem Text="Hermano" Value="Hermano" />
                    <asp:ListItem Text="Hermano" Value="Hermana" />
                    <asp:ListItem Text="Abuelo" Value="Abuelo" />
                    <asp:ListItem Text="Abuelo" Value="Abuela" />
                    <asp:ListItem Text="Otro" Value="Otro" />
                </asp:DropDownList>
            </div>

            <div class="form-group">
                <label for="ddlEstadoRegistro">Estado de Registro:</label>
                <asp:DropDownList ID="ddlEstadoRegistro" runat="server" CssClass="form-control">
                    <asp:ListItem Text="Seleccione Estado" Value="" />
                    <asp:ListItem Text="Activo" Value="Activo" />
                    <asp:ListItem Text="Inactivo" Value="Inactivo" />
                </asp:DropDownList>
            </div>

            <asp:Button ID="btnAgregar" runat="server" CssClass="btn btn-agregar" Text="Agregar" OnClick="btnAgregar_Click" />
            <asp:Button ID="btnRegresar" runat="server" CssClass="btn btn-regresar" Text="Regresar" OnClick="btnRegresar_Click" />

            <asp:Label ID="lblMensaje" runat="server" ForeColor="Green" Visible="False"></asp:Label>
        </div>
    </form>
</body>
</html>
