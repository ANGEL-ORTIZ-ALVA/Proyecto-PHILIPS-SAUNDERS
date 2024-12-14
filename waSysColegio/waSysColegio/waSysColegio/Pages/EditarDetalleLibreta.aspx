<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditarDetalleLibreta.aspx.cs" Inherits="waSysColegio.Pages.EditarDetalleLibreta" %>


<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Editar Detalle de Libreta</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet"/>
    <style>
        body {
            background-color: #f0f2f5;
            color: #333;
        }
        .card {
            border-radius: 15px;
            box-shadow: 0 10px 30px rgba(0, 0, 0, 0.1);
        }
        .card-header {
            background-color: #4a90e2;
            color: white;
            border-radius: 15px 15px 0 0 !important;
            padding: 20px;
        }
        .btn-primary {
            background-color: #4a90e2;
            border: none;
            border-radius: 8px;
            padding: 12px 30px;
            font-weight: 600;
        }
        .btn-secondary {
            border-radius: 8px;
            padding: 12px 30px;
        }
        .btn i {
            margin-right: 5px;
        }
        .image-preview {
            max-width: 150px;
            max-height: 150px;
            display: block;
            margin: 10px 0;
        }
    </style>
</head>
<body>
    <div class="container mt-5">
        <div class="row justify-content-center">
            <div class="col-md-8">
                <div class="card">
                    <div class="card-header text-center">
                        <h2>Editar Detalle de Libreta</h2>
                    </div>
                    <div class="card-body">
                        <form id="formEditarDetalleLibreta" runat="server" novalidate>
                            <asp:HiddenField ID="hdnIDLibreta" runat="server" />
                            <asp:HiddenField ID="hdnIDPersonal" runat="server" />
                            <div class="mb-3">
                                <label for="ddlPersonal" class="form-label">Personal</label>
                                <asp:DropDownList ID="ddlPersonal" runat="server" CssClass="form-select">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqPersonal" runat="server" ControlToValidate="ddlPersonal" ErrorMessage="Seleccione un personal" CssClass="text-danger" Display="Dynamic" />
                            </div>
                            <div class="mb-3">
                                <label for="fileFirma" class="form-label">Firma</label>
                                <asp:FileUpload ID="fileFirma" runat="server" CssClass="form-control" />
                                <asp:Image ID="imgFirmaActual" runat="server" CssClass="image-preview" Visible="false" />
                                <asp:Label ID="lblFirmaActual" runat="server" CssClass="form-text"></asp:Label>
                            </div>
                            <div class="mb-3">
                                <label for="fileSello" class="form-label">Sello</label>
                                <asp:FileUpload ID="fileSello" runat="server" CssClass="form-control" />
                                <asp:Image ID="imgSelloActual" runat="server" CssClass="image-preview" Visible="false" />
                                <asp:Label ID="lblSelloActual" runat="server" CssClass="form-text"></asp:Label>
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