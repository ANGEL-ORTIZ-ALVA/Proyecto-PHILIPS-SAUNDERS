<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditarGenero.aspx.cs" Inherits="waSysColegio.Pages.EditarGenero" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Editar Género</title>
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        body {
            background-color: #f0f4f8; 
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
        .btn-actualizar, .btn-regresar {
            background-color: #28a745; 
            color: white;
            font-weight: bold; 
            transition: background-color 0.3s, transform 0.2s; 
            width: 100%; 
            margin-top: 15px;
        }
        .btn-actualizar:hover, .btn-regresar:hover {
            background-color: #218838; 
            transform: scale(1.05); 
        }
        .btn-regresar {
            background-color: #007bff; 
        }
        .btn-regresar:hover {
            background-color: #0069d9; 
        }
        .form-group {
            display: flex;
            flex-direction: column; 
            align-items: center; 
        }
        .form-group label {
            margin-bottom: 5px;
            font-weight: bold;
        }
        .form-control {
            width: 100%; 
            text-align: center; 
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2>Editar Género</h2>
            <div class="form-group">
                <label for="txtNombreGenero">Nombre del Género:</label>
                <asp:TextBox ID="txtNombreGenero" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <asp:Label ID="lblMensaje" runat="server" ForeColor="Red" /> <!-- Agregar este Label para mostrar errores -->
            <asp:Button ID="btnActualizar" runat="server" CssClass="btn btn-actualizar" Text="Actualizar" OnClick="btnActualizar_Click" />
            <asp:Button ID="btnRegresar" runat="server" CssClass="btn btn-regresar" Text="Regresar" OnClick="btnRegresar_Click" />
        </div>
    </form>
</body>
</html>