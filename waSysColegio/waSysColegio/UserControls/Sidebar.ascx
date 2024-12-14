<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Sidebar.ascx.cs" Inherits="waSysColegio.UserControls.Sidebar" %>
<nav class="sidebar close">
    <header>
        <div class="image-text" title="Colegio Philip P. Saunders">
            <span class="image">
                <img src="../Resources/logo-school.png" alt="" />
            </span>

            <div class="text logo-text">
                <span class="name">Colegio</span>
                <span class="profession">Philip P. Saunders</span>
            </div>
        </div>

        <i class='bx bx-chevron-right toggle'></i>
    </header>
    <div class="menu-bar">
    <div class="menu">
        <ul>
            <li class="search-box" title="Buscar">
                <i class='bx bx-search icon'></i>
                <input type="text" id="searchInput" placeholder="Search..." />
            </li>
        </ul>

        <ul class="menu-links">
            <li class="nav-link" runat="server" id="MenuDashboard" title="Inicio - Dashboard">
                <a href="../Pages/Dashboard.aspx">
                    <i class='bx bx-home-alt icon'></i>
                    <span class="text nav-text">Dashboard</span>
                </a>
            </li>

            <li class="nav-link" runat="server" id="MenuCalificaciones" title="Calificaciones">
                <a href="../Seccion/VerSecciones">
                    <i class='bx bxs-pencil icon'></i>
                    <span class="text nav-text">Calificaciones</span>
                </a>
            </li>

            
            <li class="nav-link" runat="server" id="MenuNotas" title="Consulta de notas">
                <a href="../Notas/Index">
                    <i class='bx bxs-check-circle icon'></i>
                    <span class="text nav-text">Notas</span>
                </a>
            </li>

            <li class="nav-link" runat="server" id="MenuEstudiantes" title="Lista de Estudiantes">
                <a href="../Estudiante/Index">
                    <i class='bx bxs-face icon'></i>
                    <span class="text nav-text">Estudiantes</span>
                </a>
            </li>

            <li class="nav-link" runat="server" id="MenuLibretas" title="Lista de Libretas de los estuadiantes">
                <a href="../Libreta/SeleccionarGradoSeccion">
                    <i class='bx bxs-notepad icon'></i>
                    <span class="text nav-text">Libretas</span>
                </a>
            </li>

            <li class="nav-link" runat="server" id="MenuCursos" title="Lista de Cursos">
                <a href="../Curso/Index">
                    <i class='bx bxs-chalkboard icon'></i>
                    <span class="text nav-text">Cursos</span>
                </a>
            </li>

            <li class="nav-link" runat="server" id="MenuSecciones" title="Lista de Secciones">
                <a href="../Seccion/Index">
                    <i class='bx bxs-grid-alt icon'></i>
                    <span class="text nav-text">Secciones</span>
                </a>
            </li>

            <li class="nav-link" runat="server" id="MenuGrado" title="Lista de Grados">
                <a href="../Grado/Index">
                    <i class='bx bxs-graduation icon'></i>
                    <span class="text nav-text">Grado</span>
                </a>
            </li>

            <li class="nav-link" runat="server" id="MenuApoderados" title="Lista de Apoderados">
                <a href="../Apoderado/Index">
                    <i class='bx bxs-user icon'></i>
                    <span class="text nav-text">Apoderados</span>
                </a>
            </li>

            <li class="nav-link" runat="server" id="MenuGeneros" title="Lista de Géneros">
                <a href="../Genero/Index">
                    <i class='bx bx-male-female icon'></i>
                    <span class="text nav-text">Géneros</span>
                </a>
            </li>
            <li class="nav-link" runat="server" id="MenuDetaApo" title="Lista de Apoderados de Estudiantes">
                <a href="../Apoderado_Estudiante/Index">
                    <i class='bx bxs-user-badge icon'></i>
                    <span class="text nav-text">Detalle Apo.</span>
                </a>
            </li>

            <li class="nav-link" runat="server" id="MenuPersonal" title="Lista del Personal">
                <a href="../Personal/Index">
                    <i class='bx bx-clipboard icon'></i>
                    <span class="text nav-text">Personal</span>
                </a>
            </li>

            <li class="nav-link" runat="server" id="MenuUsuarios" title="Lista de usuario">
                <a href="../Usuario/Index">
                    <i class='bx bxs-group icon'></i>
                    <span class="text nav-text">Usuarios</span>
                </a>
            </li>
            

            <li class="nav-link" runat="server" id="MenuTipoPer" title="Lista de Tipos de Personal">
                <a href="../Pages/VerTipoPersonal.aspx">
                    <i class='bx bxs-id-card icon'></i>
                    <span class="text nav-text">Tipo Personal</span>
                </a>
            </li>

            <li class="nav-link" runat="server" id="MenuPeriodos" title="Lista de Periodos">
                <a href="../Periodo/Index">
                    <i class='bx bxs-calendar-event icon'></i>
                    <span class="text nav-text">Periodos</span>
                </a>
            </li>
            <li class="nav-link" runat="server" id="MenuRoles" title="Lista de Roles">
                <a href="../Rol/Index">
                    <i class='bx bxs-hard-hat icon'></i>
                    <span class="text nav-text">Roles</span>
                </a>
            </li>
            <li class="nav-link" runat="server" id="MenuEstUsu" title="Lista de Estados de Usuario">
                <a href="../Estado_Usuario/Index">
                    <i class='bx bx-user-check icon'></i>
                    <span class="text nav-text">Estados de Usuario</span>
                </a>
            </li>
            <!--
<li class="nav-link">
    <a href="#">
        <i class='bx bx-heart icon'></i>
        <span class="text nav-text">Likes</span>
    </a>
</li>

<li class="nav-link">
    <a href="#">
        <i class='bx bx-wallet icon'></i>
        <span class="text nav-text">Wallets</span>
    </a>
</li>
    -->

        </ul>
    </div>

    <div class="bottom-content">
        <ul>
            <li class="" title="Cerrar Sesion">
                <a onclick="logout()" href="#">
                    <i class='bx bx-log-out icon'></i>
                    <span class="text nav-text">Logout</span>
                </a>
            </li>

            <li class="mode">
                <div class="sun-moon">
                    <i class='bx bx-moon icon moon'></i>
                    <i class='bx bx-sun icon sun'></i>
                </div>
                <span class="mode-text text">Dark mode</span>

                <div class="toggle-switch">
                    <span class="switch"></span>
                </div>
            </li>
        </ul>
    </div>
</div>
</nav>