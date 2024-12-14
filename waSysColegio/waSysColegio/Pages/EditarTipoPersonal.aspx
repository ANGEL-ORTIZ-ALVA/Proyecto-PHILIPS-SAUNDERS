<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditarTipoPersonal.aspx.cs" Inherits="waSysColegio.Pages.EditarTipoPersonal" %>

<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Editar Tipo de Personal</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet">
    <style>
        body {
            background-color: #f0f2f5;
            color: #333;
        }
        .card {
            border: none;
            border-radius: 15px;
            box-shadow: 0 10px 30px rgba(0, 0, 0, 0.1);
        }
        .card-header {
            background-color: #4a90e2;
            color: white;
            border-radius: 15px 15px 0 0 !important;
            padding: 20px;
        }
        .form-label {
            font-weight: 600;
            color: #555;
        }
        .form-control {
            border-radius: 8px;
            border: 2px solid #e0e0e0;
            padding: 10px 15px;
            transition: all 0.3s ease;
        }
        .form-control:focus {
            border-color: #4a90e2;
            box-shadow: 0 0 0 0.2rem rgba(74, 144, 226, 0.25);
        }
        .btn-primary {
            background-color: #4a90e2;
            border: none;
            border-radius: 8px;
            padding: 12px 30px;
            font-weight: 600;
            transition: all 0.3s ease;
        }
        .btn-primary:hover {
            background-color: #3a7bc8;
            transform: translateY(-2px);
        }
    </style>
</head>
<body>
    <div class="container mt-5">
        <div class="row justify-content-center">
            <div class="col-md-8">
                <div class="card">
                    <div class="card-header text-center">
                        <h2>Editar Tipo de Personal</h2>
                    </div>
                    <div class="card-body">
                        <form id="form1" runat="server" novalidate>
                            <asp:HiddenField ID="hdnIDTipoPersonal" runat="server" />
                            <div class="form-floating mb-3">
                                <asp:TextBox ID="txtNombreTipoPersonal" runat="server" CssClass="form-control" MaxLength="50" placeholder="Nombre del Tipo de Personal"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqNombreTipoPersonal" runat="server" ControlToValidate="txtNombreTipoPersonal" ErrorMessage="El nombre del tipo de personal es requerido" CssClass="invalid-feedback" Display="Dynamic" />
                                <label for="txtNombreTipoPersonal">Nombre Tipo de Personal</label>
                            </div>
                            <div class="form-floating mb-3">
                                <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" MaxLength="200" placeholder="Descripción" TextMode="MultiLine" style="height: 100px;"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqDescripcion" runat="server" ControlToValidate="txtDescripcion" ErrorMessage="La descripción es requerida" CssClass="invalid-feedback" Display="Dynamic" />
                                <label for="txtDescripcion">Descripción</label>
                            </div>
                            <div class="text-center">
                                <asp:Button ID="btnGuardar" runat="server" CssClass="btn btn-primary" Text="&#128190; Guardar" OnClick="btnGuardar_Click" />
                                <asp:Button ID="btnRegresar" runat="server" CssClass="btn btn-secondary" Text="&#11172; Regresar" OnClick="btnRegresar_Click" />
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="successModal" tabindex="-1" aria-labelledby="successModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="successModalLabel">Edición exitosa</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    El tipo de personal ha sido actualizado correctamente.
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                </div>
            </div>
        </div>
    </div>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
<script>
    // Mostrar el modal de éxito al guardar
    function showSuccessModal() {
        var modal = new bootstrap.Modal(document.getElementById('successModal'));
        modal.show();

        // Escuchar el evento de cierre del modal
        document.getElementById('successModal').addEventListener('hidden.bs.modal', function () {
            // Redirigir a la página de listado
            window.location.href = 'wfListarTipoPersonal.aspx';
        });
    }
</script>

</body>
</html>
