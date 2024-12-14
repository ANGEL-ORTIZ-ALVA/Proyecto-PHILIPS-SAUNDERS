<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AgregarDetalleCurso.aspx.cs" Inherits="waSysColegio.Pages.AgregarDetalleCurso" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Registrar Detalle Curso</title>
    <link href="../Styles/AgregarDetalle.css" rel="stylesheet" type="text/css" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet"/>
<link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css" rel="stylesheet"/>
    
</head>
<body>

    <div class="container mt-5 mb-5">
        <div class="row justify-content-center">
            <div class="col-md-8">
                <div class="card">
                <div class="card-header text-center">
                    <h2 class="mb-0">Registrar Nota</h2>
                </div>
                <div class="progress">
                    <div class="progress-bar" role="progressbar" aria-valuenow="0" aria-valuemin="0" aria-valuemax="100"></div>
                </div>
                <div class="card-body">
                <form id="form1" runat="server">

                    <div class="row mb-3">
                        <div class="col-md-12">
                            <div class="form-floating mb-12">
                                <asp:DropDownList ID="ddlEstudiante" runat="server" class="form-select" required aria-label="Género" />
                                <label for="Alumno">Seleccione Estudiante a asignar actividad</label>
                                <div class="invalid-feedback">
                                    Por favor, seleccione una actividad evaluativa.
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="row mb-3">
                        <div class="col-md-6">
                            <div class="form-floating mb-3">
                                <asp:DropDownList ID="ddlEvaluacion" runat="server" class="form-select" required aria-label="Género" />
                                <label for="Evaluacion">Seleccione Evaluacion</label>
                                <div class="invalid-feedback">
                                    Por favor, seleccione una actividad evaluativa.
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-floating mb-3">
                                <asp:DropDownList ID="ddlCurso" runat="server" class="form-select" required aria-label="Género" />
                                <label for="Evaluacion">Seleccione Evaluacion</label>
                                <div class="invalid-feedback">
                                    Por favor, seleccione una actividad evaluativa.
                                </div>
                            </div>
                        </div>
                    </div>


                    <div class="row mb-3">
                        <div class="col-md-6">
                            <div class="form-floating mb-3">
                                <asp:DropDownList ID="ddlAsistencia" runat="server" class="form-select" required aria-label="Género" />
                                <label for="Evaluacion">Seleccione Evaluacion</label>
                                <div class="invalid-feedback">
                                    Por favor, seleccione una actividad evaluativa.
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-floating mb-3">
                                <asp:DropDownList ID="ddlPeriodo" runat="server" class="form-select" required aria-label="Género" />
                                <label for="Evaluacion">Seleccione Evaluacion</label>
                                <div class="invalid-feedback">
                                    Por favor, seleccione una actividad evaluativa.
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="row mb-3">
                        <div class="col-md-6">
                            <div class="form-floating mb-3">
                                <asp:TextBox ID="txtNota" runat="server" TextMode="Number"
                                    placeholder="Nota" class="form-control" required ></asp:TextBox>
                                <label for="Nota">Nota</label>
                                <div class="invalid-feedback">
                                    Por favor, ingrese una noat valida del 1 al 20
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-floating mb-3">
                                <asp:DropDownList ID="ddlEstado" runat="server" class="form-select" required aria-label="Género" >
                                <asp:ListItem Value="Registrado">Registrado</asp:ListItem>
                                <asp:ListItem Value="Eliminado">Eliminado</asp:ListItem>
                                </asp:DropDownList>
                                <label for="Estado">Seleccione Estado del registro</label>
                                <div class="invalid-feedback">
                                    Por favor, seleccione una actividad evaluativa.
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="text-center">
                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" class="btn btn-primary btn-lg" />
                    </div>
                   </form>
                 </div>
              </div>
           </div>
       </div>
   </div>


</body>
    <script src="../Scripts/Forms(AgDet).js"></script>
     <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
 <script src="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/js/all.min.js"></script>
</html>

