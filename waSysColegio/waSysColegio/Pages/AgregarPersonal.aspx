<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AgregarPersonal.aspx.cs" Inherits="waSysColegio.Pages.AgregarPersonal" %>

<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Registro Trabajador</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet">
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css" rel="stylesheet">
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
    <div class="container mt-5 mb-5">
        <div class="row justify-content-center">
            <div class="col-md-8">
                <div class="card">
                    <div class="card-header text-center">
                        <h2 class="mb-0">Registro de Personal</h2>
                    </div>
                    <div class="progress">
                        <div class="progress-bar" role="progressbar" aria-valuenow="0" aria-valuemin="0" aria-valuemax="100"></div>
                    </div>
                    
                    <div class="card-body">
                        <form id="registroForm" runat="server" novalidate>
                            <div class="row mb-3">
                                <div class="col-md-6">
                                    <div class="form-floating mb-3">
                                        <asp:TextBox ID="txtNombres" runat="server" CssClass="form-control" MaxLength="50" placeholder="Nombres"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="reqNombres" runat="server" ControlToValidate="txtNombres" ErrorMessage="Nombres requeridos" Display="Dynamic" CssClass="invalid-feedback" />
                                        <label for="txtNombres">Nombres</label>
                                        <div class="invalid-feedback">
                                            Por favor, ingrese nombres válidos (solo letras y espacios, 2-50 caracteres).
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-floating mb-3">
                                        <asp:TextBox ID="txtApellidos" runat="server" CssClass="form-control" MaxLength="50" placeholder="Apellidos" ></asp:TextBox>
                                        <label for="txtApellidos">Apellidos</label>
                                        <div class="invalid-feedback">
                                            Por favor, ingrese apellidos válidos (solo letras y espacios, 2-50 caracteres).
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row mb-3">
                                <div class="col-md-6">
                                    <div class="form-floating mb-3">
                                    <asp:DropDownList ID="ddlGenero" runat="server" CssClass="form-select" >
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" InitialValue="0" ControlToValidate="ddlGenero" ErrorMessage="Seleccione un género" CssClass="invalid-feedback" Display="Dynamic" />
                                    <label for="ddlGenero">Género</label>
                                    <div class="invalid-feedback">
                                        Por favor, seleccione un género.
                                    </div>
                                </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-floating mb-3">
                                        <asp:TextBox ID="txtFechaNacimiento" runat="server" CssClass="form-control" placeholder="Fecha de Nacimiento" TextMode="Date" />
                                        
                                        <label for="txtFechaNacimiento">Fecha de Nacimiento</label>
                                        <div class="invalid-feedback">
                                            Por favor, seleccione una fecha de nacimiento válida.
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row mb-3">
                                <div class="col-md-6">
                                     <div class="form-floating mb-3">
                                         <asp:TextBox ID="txtDNI" runat="server" class="form-control"  pattern="[0-9]{8}" placeholder="DNI"></asp:TextBox>
                                         <asp:RegularExpressionValidator ID="valDNI" runat="server" ControlToValidate="txtDNI" ErrorMessage="DNI inválido" ValidationExpression="^\d{8}$" Display="Dynamic" CssClass="invalid-feedback" />
                                         <label for="txtDNI">DNI</label>
                                     </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-floating mb-3">
                                        <asp:TextBox ID="txtTelefono" runat="server" class="form-control"  pattern="[0-9]{9}" placeholder="Teléfono" TextMode="Phone"></asp:TextBox>
                                        <label for="txtTelefono">Teléfono</label>
                                        <div class="invalid-feedback">
                                            Por favor, ingrese un número de teléfono válido (9 dígitos numéricos).
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-floating mb-3">
                                <asp:TextBox ID="txtCorreo" runat="server" class="form-control"  placeholder="Correo" TextMode="Email"></asp:TextBox>
                                <label for="txtCorreo">Correo</label>
                                <div class="invalid-feedback">
                                    Por favor, ingrese una dirección de correo electrónico válida.
                                </div>
                            </div>
                            <div class="form-floating mb-3">
                                <asp:TextBox ID="txtDireccion" runat="server" class="form-control"  minlength="10" maxlength="200" style="height: 100px" placeholder="Dirección" ></asp:TextBox>
                                <label for="txtDireccion">Dirección</label>
                                <div class="invalid-feedback">
                                    Por favor, ingrese una dirección válida (10-200 caracteres).
                                </div>
                            </div>
                            <div class="form-floating mb-3">
                                <asp:DropDownList ID="ddlTipopersonal" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlTipopersonal_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" InitialValue="0" ControlToValidate="ddlTipopersonal" ErrorMessage="Seleccione un tipo de personal" CssClass="invalid-feedback" Display="Dynamic" />
                                <label for="ddlTipopersonal">Tipo de Personal</label>
                            </div>

                            <div id="tutorFields" runat="server" style="display: none;">
                                <div class="row mb-3">
                                    <div class="col-md-6">
                                        <div class="form-floating mb-3">
                                            <asp:DropDownList ID="ddlSeccion" runat="server" CssClass="form-select" ></asp:DropDownList>
                                            <label for="ddlSeccion">Seccion</label>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-floating mb-3">
                                            <asp:DropDownList ID="ddlGrado" runat="server" CssClass="form-select" ></asp:DropDownList>
                                            <label for="ddlGrado">Grado</label>
                                        </div>
                                    </div>
                                </div>
                            </div>


                            <div class="form-floating mb-3">
                                <asp:DropDownList ID="ddlUsuario" runat="server" CssClass="form-select" AutoPostBack="true">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" InitialValue="0" ControlToValidate="ddlUsuario" ErrorMessage="Seleccione un usuario" CssClass="invalid-feedback" Display="Dynamic" />
                                <label for="ddlUsuario">Usuario</label>
                            </div>
                            <div class="text-center">
                                <asp:Button ID="btnGuardar" runat="server" CssClass="btn btn-primary" Text="&#128190; Registrar" OnClick="btnGuardar_Click" />

                                <asp:Button ID="btnRegresar" runat="server" CssClass="btn btn-secondary" Text="&#11172; Regresar" OnClick="btnRegresar_Click" />

                            </div>
                            <div class="text-center">
                                <asp:Label ID="lblmensaje" runat="server" Text=""></asp:Label>
                            </div>
                        </form>
                    </div>
                    </div>
                </div>
            </div>
        </div>

    <div class="modal fade" id="errorModal" tabindex="-1" aria-labelledby="errorModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="errorModalLabel">Error en el registro</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    Ha ocurrido un error al intentar registrar el personal.
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                </div>
            </div>
        </div>
    </div>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/js/all.min.js"></script>
    <script>
        document.addEventListener('DOMContentLoaded', function () {
            const form = document.getElementById('registroForm');
            const fechaNacimiento = document.getElementById('txtFechaNacimiento'); // Asegúrate de que el ID esté correcto
            const progressBar = document.querySelector('.progress-bar');
            const ddlTipoPersonal = document.getElementById('ddlTipopersonal'); // Asegúrate de que el ID esté correcto
            const tutorFields = document.getElementById('tutorFields');
            const successModal = new bootstrap.Modal(document.getElementById('successModal'));
            const errorModal = new bootstrap.Modal(document.getElementById('errorModal'));

            // Establecer la fecha máxima para que solo se puedan seleccionar personas mayores de 18 años
            const today = new Date();
            today.setFullYear(today.getFullYear() - 18); // Resta 18 años a la fecha actual
            const maxDate = today.toISOString().split('T')[0]; // Convierte la fecha a formato yyyy-mm-dd
            fechaNacimiento.setAttribute('max', maxDate); // Establece la fecha máxima en el input de fecha

            // Evento adicional que muestra error si se intenta poner una fecha no permitida
            fechaNacimiento.addEventListener('change', function () {
                const selectedDate = new Date(this.value);
                if (selectedDate > new Date(maxDate)) {
                    this.value = ''; // Limpia el input si la fecha es inválida
                    alert('Fecha no permitida. Debe ser mayor de 18 años.');
                }
            });

            ddlTipoPersonal.addEventListener('change', function () {
                tutorFields.style.display = this.value === '3' ? 'block' : 'none';
            });

            // Validaciones adicionales aquí

            form.addEventListener('input', updateProgressBar);
            form.addEventListener('submit', function (event) {
                if (!form.checkValidity()) {
                    event.preventDefault();
                    event.stopPropagation();
                    errorModal.show();
                } else {
                    console.log('Formulario enviado');
                    event.preventDefault(); // Evita el envío real para demostración
                    successModal.show();
                }

                form.classList.add('was-validated');
            });

            function updateProgressBar() {
                const inputs = form.querySelectorAll('input:not([type="hidden"]), select, textarea');
                let filledInputs = 0;
                inputs.forEach(input => {
                    if (input.value.trim() !== '' && input.checkValidity()) {
                        filledInputs++;
                    }
                });
                const progress = (filledInputs / inputs.length) * 100;
                progressBar.style.width = ${ progress }%;
                progressBar.setAttribute('aria-valuenow', progress);
            }
        });
    </script>


</body>
</html>