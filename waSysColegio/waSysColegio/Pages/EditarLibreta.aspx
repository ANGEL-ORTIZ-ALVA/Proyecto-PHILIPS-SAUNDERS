<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditarLibreta.aspx.cs" Inherits="waSysColegio.Pages.EditarLibreta" %>
<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Editar Libreta</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet">
    <style>
        body {
            background-color: #f7f9fc;
            color: #333;
        }
        .card {
            border-radius: 15px;
            box-shadow: 0 10px 30px rgba(0, 0, 0, 0.1);
        }
        .card-header {
            background-color: #4a90e2;
            color: white;
            padding: 20px;
            border-radius: 15px 15px 0 0 !important;
        }
        .form-control {
            border-radius: 8px;
            padding: 10px 15px;
            border: 2px solid #e0e0e0;
        }
        .form-control:focus {
            border-color: #4a90e2;
            box-shadow: 0 0 0 0.2rem rgba(74, 144, 226, 0.25);
        }
        .btn-primary {
            background-color: #4a90e2;
            border: none;
            padding: 12px 30px;
            border-radius: 8px;
        }
        .btn-secondary {
            border-radius: 8px;
            padding: 12px 30px;
        }
        .btn i {
            margin-right: 5px;
        }
    </style>
</head>
<body>
    <div class="container mt-5 mb-5">
        <div class="row justify-content-center">
            <div class="col-md-6">
                <div class="card">
                    <div class="card-header text-center">
                        <h3 class="mb-0">Editar Libreta</h3>
                    </div>
                    <div class="card-body">
                        <form id="form1" runat="server">
                            <asp:HiddenField ID="hdnIDLibreta" runat="server" />
                            <div class="form-floating mb-3">
                                <asp:TextBox ID="txtAnioEscolar" runat="server" CssClass="form-control" TextMode="Date" placeholder="Año Escolar"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqAnioEscolar" runat="server" ControlToValidate="txtAnioEscolar" ErrorMessage="El año escolar es requerido" CssClass="text-danger" Display="Dynamic" />
                                <label for="txtAnioEscolar">Año Escolar</label>
                            </div>
                            <div class="form-floating mb-3">
                                <asp:DropDownList ID="ddlEstudiantes" runat="server" CssClass="form-select" placeholder="Estudiante"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqEstudiante" runat="server" ControlToValidate="ddlEstudiantes" InitialValue="" ErrorMessage="Debe seleccionar un estudiante" CssClass="text-danger" Display="Dynamic" />
                                <label for="ddlEstudiantes">Estudiante</label>
                            </div>
                            <div class="text-center">
                                <asp:Button ID="btnGuardar" runat="server" CssClass="btn btn-primary" Text="&#128190; Guardar" OnClick="btnGuardar_Click" />
                                <asp:Button ID="btnRegresar" runat="server" CssClass="btn btn-secondary ms-2" Text="&#11172; Regresar" OnClick="btnRegresar_Click" />
                            </div>
                            <div class="text-center mt-3">
                                <asp:Label ID="lblMensaje" runat="server" CssClass="text-success"></asp:Label>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>
