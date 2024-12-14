<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="waPlantillaVerDash.aspx.cs" Inherits="waSysColegio.Pages.waPlantillaVer" %>

<!--FRACCION DEL SIDEBAR y FOOTER-->
<%@ Register Src="~/UserControls/Sidebar.ascx" TagName="Sidebar" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/Footer.ascx" TagName="Footer" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/Head_Content.ascx" TagName="Head_Content" TagPrefix="uc" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <!----======== CSS ======== -->
    <link rel="stylesheet" href="../Styles/dash.css" />
    <!--HEAD CONTENT-->
<uc:Head_Content runat="server" />
    <title></title>
</head>
<body>
    <!--===== SIDEBAR =====-->
    <uc:Sidebar runat="server" />
    <!--FIN SIDEBAR-->

    <section class="home">
        <div class="text">
            <img src="../Resources/logo-school.png" width="50" height="50" />
            <h2>Colegio 'Philip P. Saunders'</h2>
        </div>
        <!--===== TABLA DE GESTION DE PERSONAL =====-->
        <div class="text">
           <!-- <form id="form1" runat="server"> -->
                <div class="form-container">
                    <div class="table-responsive">
                        <div style="margin: 10px 0px;">
                           <figure>
                                <img src="../Resources/colegiop.jpg" class="image-resize" width="40%" />
                            </figure>
                        </div>
                         <p class="content-dash">
                             El Centro Educativo 3519 Philip P. Saunders está ubicado en el distrito de Carabayllo,
                             en la ciudad de Lima, Perú. Fundado con el propósito de ofrecer educación de calidad,
                             el centro tiene como misión contribuir al desarrollo integral de los estudiantes,
                             basándose en una formación académica sólida y en valores éticos y humanos.
                         </p>
                        <!-- Sección Apafa -->
                        <section class="apafa">
                            <h3>Asociación de Padres de Familia (APFA)</h3>
                            <p  class="content-dash">La APFA trabaja en conjunto con el colegio para organizar eventos, apoyar el desarrollo de proyectos educativos y fomentar la participación activa de los padres en la vida escolar de los estudiantes.</p>
                        </section>
                         <!-- Sección Dirección -->
                         <section class="direccion">
                             <h3>Dirección del Colegio</h3>
                             <p  class="content-dash">
                                 Centro Educativo 3519 Philip P. Saunders<br />
                                 Av. Los Laureles 123, Carabayllo, Lima, Perú
                             </p>
                             <!-- Opcional: puedes agregar un mapa de Google aquí -->
                             <iframe src="https://www.google.com/maps/embed?pb=!1m14!1m8!1m3!1d15619.533546265486!2d-77.0286622!3d-11.8434357!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x9105d7cc363ee9e5%3A0xf752337134538f72!2sInstituci%C3%B3n%20Educativa%20Philip%20P.%20Saunders!5e0!3m2!1ses-419!2spe!4v1732416129844!5m2!1ses-419!2spe" 
                                 width="700" height="400" style="border:0;" allowfullscreen="" loading="lazy" referrerpolicy="no-referrer-when-downgrade"></iframe>
                         </section>

                        <section class="cursos">
                            <h2 style="text-align:center;">Cursos</h2>
                             <!-- Cards Container -->
                             <div class="container-card">
                                 <div class="card">
                                     <figure>
                                         <img src="../Resources/computacion.jpg" width="40%"/>
                                     </figure>
                                     <div class="contenido-card">
                                         <h3>COMPUTACION</h3>
                                         <p>
                                             "El curso de Computación en el colegio brinda a los estudiantes conocimientos fundamentales en programación, manejo de sistemas operativos, diseño web y redes.
                                             Fomenta habilidades tecnológicas que preparan a los alumnos para enfrentar los desafíos digitales del futuro."
                                         </p>
                                         <a href="#">Leer Más</a>
                                     </div>
                                 </div>

                                 <div class="card">
                                     <figure>
                                         <img src="../Resources/comunicacion.jpg" width="40%"/>
                                     </figure>
                                     <div class="contenido-card">
                                         <h3>COMUNICACION</h3>
                                         <p>
                                             "El curso de Comunicación en el colegio desarrolla habilidades lingüísticas y expresivas, tanto orales como escritas. Los estudiantes aprenden a organizar sus ideas,
                                             mejorar su capacidad de argumentación y crear contenidos para diversos medios, promoviendo una comunicación efectiva y ética."
                                         </p>
                                         <a href="#">Leer Más</a>
                                     </div>
                                 </div>

                                 <div class="card">
                                     <figure>
                                         <img src="../Resources/fisica.jpeg" width="40%"/>
                                     </figure>
                                     <div class="contenido-card">
                                         <h3>EDUCACION FISICA</h3>
                                         <p>
                                             "El curso de Educación Física promueve el desarrollo físico y mental de los estudiantes a través de actividades deportivas y ejercicios.
                                             Fomenta el trabajo en equipo, la disciplina, la salud y el bienestar, contribuyendo a una vida activa y equilibrada."

                                         </p>
                                         <a href="#">Leer Más</a>
                                     </div>
                                 </div>

                                 <div class="card">
                                     <figure>
                                         <img src="../Resources/ingles.jpg" width="40%"/>
                                     </figure>
                                     <div class="contenido-card">
                                         <h3>INGLES</h3>
                                         <p>
                                             "El curso de Inglés en el colegio enseña a los estudiantes a comprender, hablar, leer y escribir en inglés. A través de actividades prácticas, se fomenta la fluidez verbal, 
                                             la comprensión lectora y la capacidad de comunicación en un idioma global."
                                         </p>
                                         <a href="#">Leer Más</a>
                                     </div>
                                 </div>

                                 <div class="card">
                                     <figure>
                                         <img src="../Resources/matematica.png" width="40%"/>
                                     </figure>
                                     <div class="contenido-card">
                                         <h3>MATEMATICA</h3>
                                         <p>
                                             "El curso de Matemática desarrolla habilidades en resolución de problemas, álgebra, cálculo y estadísticas. Los estudiantes aprenden a aplicar conceptos matemáticos en situaciones reales,
                                             fortaleciendo su pensamiento lógico, analítico y su capacidad para tomar decisiones informadas."
                                         </p>
                                         <a href="#">Leer Más</a>
                                     </div>
                                 </div>
                                 <div class="card">
                                     <figure>
                                         <img src="../Resources/geometria.jpeg" width="40%"/>
                                     </figure>
                                     <div class="contenido-card">
                                         <h3>GEOMETRIA</h3>
                                         <p>
                                             "El curso de Geometría enseña a los estudiantes a comprender y analizar formas, tamaños y espacios. A través de figuras y construcciones geométricas, desarrollan habilidades en razonamiento espacial,
                             cálculo de áreas y volúmenes, y resolución de problemas visuales y matemáticos."
                                         </p>
                                         <a href="#">Leer Más</a>
                                     </div>
                                 </div>
                                 <div class="card">
                                     <figure>
                                         <img src="../Resources/ciencias.jpeg" width="40%"/>
                                     </figure>
                                     <div class="contenido-card">
                                         <h3>CIENCIAS</h3>
                                         <p>
                                             "El curso de Ciencias fomenta el interés por el mundo natural, enseñando conceptos de biología, física, química y ecología. Los estudiantes desarrollan habilidades en observación, experimentación y análisis,
                             promoviendo una comprensión profunda de los fenómenos naturales y su aplicación."
                                         </p>
                                         <a href="#">Leer Más</a>
                                     </div>
                                 </div>
                                 <div class="card">
                                     <figure>
                                         <img src="../Resources/religion.jpeg" width="40%"/>
                                     </figure>
                                     <div class="contenido-card">
                                         <h3>RELIGION</h3>
                                         <p>
                                             "El curso de Religión en el colegio ofrece una comprensión profunda de los principios y valores espirituales. Los estudiantes exploran enseñanzas religiosas, ética y moral, promoviendo el respeto, 
                             la convivencia armoniosa y el desarrollo de una conciencia responsable y solidaria."
                                         </p>
                                         <a href="#">Leer Más</a>
                                     </div>
                                 </div>
                             </div>
                             <!-- Fin Cards Container -->
                        </section>
                    </div>
                </div>
            <!--</form>-->
        </div>
    </section>

    <!--====== FOOTER ======-->
    <uc:Footer runat="server" />
    <!--====== FIN FOOTER ======-->
<style>
    body.dark .content-dash {
        color: #adadad;
    }
</style>
</body>
</html>

