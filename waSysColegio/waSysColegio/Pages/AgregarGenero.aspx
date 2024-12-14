<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AgregarGenero.aspx.cs" Inherits="waSysColegio.Pages.AgregarGenero" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Agregar Género</title>
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
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2>Agregar Género</h2>

            <div class="form-group">
                <label for="txtNombreGenero">Nombre del Género:</label>
                <asp:TextBox ID="txtNombreGenero" runat="server" CssClass="form-control" required="required"></asp:TextBox>
            </div>

            <asp:Button ID="btnAgregar" runat="server" CssClass="btn btn-agregar" Text="Agregar" OnClick="btnAgregar_Click" />
            <asp:Button ID="btnRegresar" runat="server" CssClass="btn btn-regresar" Text="Regresar" OnClick="btnRegresar_Click" />
            
            <asp:Label ID="lblMensaje" runat="server" ForeColor="Green" Visible="False"></asp:Label>
        </div>
    </form>
</body>
</html>


