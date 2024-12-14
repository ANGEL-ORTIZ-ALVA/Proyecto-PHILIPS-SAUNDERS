<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditarPersonal.aspx.cs" Inherits="waSysColegio.Pages.EditarPersonal" %>
<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Editar Personal</title>
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
        .form-control, .form-select {
            border-radius: 8px;
            border: 2px solid #e0e0e0;
            padding: 10px 15px;
            transition: all 0.3s ease;
        }
        .form-control:focus, .form-select:focus {
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
        .progress-bar {
            height: 5px;
            background-color: #4a90e2;
            width: 0;
            transition: width 0.3s ease;
        }
        .input-group-text {
            background-color: #f8f9fa;
            border: 2px solid #e0e0e0;
            border-right: none;
        }
        .form-floating > .form-control, .form-floating > .form-select {
            height: calc(3.5rem + 4px);
            line-height: 1.25;
        }
        .form-floating > label {
            padding: 1rem 0.75rem;
        }
        input#btnRegresar {
            background-color: #525457;
            border: none;
            border-radius: 8px;
            padding: 12px 30px;
            font-weight: 600;
            transition: all 0.3s ease;
        }
    </style>
</head>
<body>
    <div class="container mt-5">
        <div class="row justify-content-center">
            <div class="col-md-8">
                <div class="card">
                    <div class="card-header text-center">
                        <h2>Editar Personal</h2>
                    </div>
                    <div class="progress">
                        <div class="progress-bar" role="progressbar" aria-valuenow="0" aria-valuemin="0" aria-valuemax="100"></div>
                    </div>
                    <div class="card-body">
                        <form id="form1" runat="server" novalidate>
                            <asp:HiddenField ID="hdnIDPersonal" runat="server" />
                            <div class="form-floating mb-3">
                                <asp:TextBox ID="txtNombres" runat="server" CssClass="form-control" MaxLength="50" placeholder="Nombres"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqNombres" runat="server" ControlToValidate="txtNombres" ErrorMessage="Nombres requeridos" CssClass="invalid-feedback" />
                                <label for="txtNombres">Nombres</label>
                            </div>
                            <div class="form-floating mb-3">
                                <asp:TextBox ID="txtApellidos" runat="server" CssClass="form-control" MaxLength="50" placeholder="Apellidos"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqApellidos" runat="server" ControlToValidate="txtApellidos" ErrorMessage="Apellidos requeridos" CssClass="invalid-feedback" />
                                <label for="txtApellidos">Apellidos</label>
                            </div>
                            <div class="form-floating mb-3">
                                <asp:TextBox ID="txtFechaNacimiento" runat="server" CssClass="form-control" TextMode="Date" placeholder="Fecha de Nacimiento"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqFechaNacimiento" runat="server" ControlToValidate="txtFechaNacimiento" ErrorMessage="Fecha de nacimiento requerida" CssClass="invalid-feedback" />
                                <label for="txtFechaNacimiento">Fecha de Nacimiento</label>
                            </div>
                            <div class="form-floating mb-3">
                                <asp:TextBox ID="txtDNI" runat="server" CssClass="form-control" MaxLength="8" placeholder="DNI"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="valDNI" runat="server" ControlToValidate="txtDNI" ErrorMessage="DNI inválido" ValidationExpression="^\d{8}$" CssClass="invalid-feedback" />
                                <label for="txtDNI">DNI</label>
                            </div>
                            <div class="form-floating mb-3">
                                <asp:TextBox ID="txtCorreo" runat="server" CssClass="form-control" TextMode="Email" placeholder="Correo"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqCorreo" runat="server" ControlToValidate="txtCorreo" ErrorMessage="Correo requerido" CssClass="invalid-feedback" />
                                <label for="txtCorreo">Correo</label>
                            </div>
                            <div class="form-floating mb-3">
                                <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" MaxLength="9" placeholder="Teléfono"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="valTelefono" runat="server" ControlToValidate="txtTelefono" ErrorMessage="Teléfono inválido" ValidationExpression="^\d{9}$" CssClass="invalid-feedback" />
                                <label for="txtTelefono">Teléfono</label>
                            </div>
                            <div class="form-floating mb-3">
                                <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control" MaxLength="200" placeholder="Dirección" style="height: 100px;"></asp:TextBox>
                                <label for="txtDireccion">Dirección</label>
                            </div>
                            <div class="form-floating mb-3">
                                <asp:DropDownList ID="ddlTipoPersonal" runat="server" CssClass="form-select"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqTipoPersonal" runat="server" ControlToValidate="ddlTipoPersonal" InitialValue="0" ErrorMessage="Seleccione un tipo de personal" CssClass="invalid-feedback" />
                                <label for="ddlTipoPersonal">Tipo de Personal</label>
                            </div>

                            <div class="form-floating mb-3">
                                <asp:DropDownList ID="ddlGenero" runat="server" CssClass="form-select"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqGenero" runat="server" ControlToValidate="ddlGenero" InitialValue="0" ErrorMessage="Seleccione un género" CssClass="invalid-feedback" />
                                <label for="ddlGenero">Género</label>
                            </div>

                            <div class="form-floating mb-3">
                                <asp:DropDownList ID="ddlGrado" runat="server" CssClass="form-select"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqGrado" runat="server" ControlToValidate="ddlGrado" InitialValue="0" ErrorMessage="Seleccione un grado" CssClass="invalid-feedback" />
                                <label for="ddlGrado">Grado</label>
                            </div>

                            <div class="form-floating mb-3">
                                <asp:DropDownList ID="ddlSeccion" runat="server" CssClass="form-select"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqSeccion" runat="server" ControlToValidate="ddlSeccion" InitialValue="0" ErrorMessage="Seleccione una sección" CssClass="invalid-feedback" />
                                <label for="ddlSeccion">Sección</label>
                            </div>

                            <div class="form-floating mb-3">
                                <asp:DropDownList ID="ddlUsuario" runat="server" CssClass="form-select"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqUsuario" runat="server" ControlToValidate="ddlUsuario" InitialValue="0" ErrorMessage="Seleccione un usuario" CssClass="invalid-feedback" />
                                <label for="ddlUsuario">Usuario</label>
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
                    El registro del personal ha sido actualizado correctamente.
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                </div>
            </div>
        </div>
    </div>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
    <script>
        document.addEventListener('DOMContentLoaded', function () {
            const form = document.getElementById('form1');
            const progressBar = document.querySelector('.progress-bar');
            form.addEventListener('input', updateProgressBar);
            function updateProgressBar() {
                const inputs = form.querySelectorAll('input:not([type="hidden"]), textarea');
                let filledInputs = 0;
                inputs.forEach(input => {
                    if (input.value.trim() !== '' && input.checkValidity()) {
                        filledInputs++;
                    }
                });
                const progress = (filledInputs / inputs.length) * 100;
                progressBar.style.width = `${progress}%`;
                progressBar.setAttribute('aria-valuenow', progress);
            }
        });
    </script>
</body>
</html>
