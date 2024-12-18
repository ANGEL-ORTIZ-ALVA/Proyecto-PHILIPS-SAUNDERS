﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Sidebar.ascx.cs" Inherits="waSysColegio.UserControls.Sidebar" %>
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
                <li class="nav-link" title="Inicio - Dashboard">
                    <a href="../Pages/Dashboard.aspx">
                        <i class='bx bx-home-alt icon'></i>
                        <span class="text nav-text">Dashboard</span>
                    </a>
                </li>

                <li class="nav-link" title="Lista de Detalle cursos">
                    <a href="../Pages/VerDetalleCurso.aspx">
                        <i class='bx bxs-book-content icon'></i>
                        <span class="text nav-text">Detalle cursos</span>
                    </a>
                </li>

                <li class="nav-link" title="Lista de Estudiantes">
                    <a href="../Estudiante/VerEstudiante">
                        <i class='bx bxs-face icon'></i>
                        <span class="text nav-text">Estudiante</span>
                    </a>
                </li>
                
                <li class="nav-link" title="Lista de Libretas">
                    <a href="../Pages/VerLibreta.aspx">
                        <i class='bx bxs-notepad icon'></i>
                        <span class="text nav-text">Libretas</span>
                    </a>
                </li>

                <li class="nav-link" title="Lista de Apoderados">
                    <a href="../Pages/VerApoderados.aspx">
                        <i class='bx bxs-group icon'></i>
                        <span class="text nav-text">Apoderados</span>
                    </a>
                </li>
                <li class="nav-link" title="Listta del Personal">
                    <a href="../Pages/VerPersonal.aspx">
                        <i class='bx bx-clipboard icon'></i>
                        <span class="text nav-text">Personal</span>
                    </a>
                </li>
                <li class="nav-link" title="Lista de Géneros">
                    <a href="../Pages/VerGenero.aspx">
                        <i class='bx bx-male-female icon'></i>
                        <span class="text nav-text">Géneros</span>
                    </a>
                </li>
                <li class="nav-link" title="Lista de Apoderados - Estudiantes">
                    <a href="../Pages/VerApoderado_Estudiante.aspx">
                        <i class='bx bxs-user-badge icon'></i>
                        <span class="text nav-text">Detalle Apo.</span>
                    </a>
                </li>
                <li class="nav-link" title="Lista de Tipos de Personal">
                    <a href="../Pages/VerTipoPersonal.aspx">
                        <i class='bx bxs-collection icon'></i>
                        <span class="text nav-text">Tipo Personal</span>
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