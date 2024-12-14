const body = document.querySelector('body'),
    sidebar = body.querySelector('nav'),
    toggle = body.querySelector(".toggle"),
    searchBtn = body.querySelector(".search-box"),
    modeSwitch = body.querySelector(".toggle-switch"),
    modeText = body.querySelector(".mode-text");

// Función para aplicar o quitar el modo oscuro basado en el LocalStorage
function applyDarkMode() {
    const darkMode = localStorage.getItem('darkMode');  // Leeemos el LocalStorage
    if (darkMode === 'enabled') {
        body.classList.add('dark');
        modeText.innerText = "Light mode";
    } else {
        body.classList.remove('dark');
        modeText.innerText = "Dark mode";
    }
}

// Función cuando la página carga para aplicar el estado correcto
applyDarkMode();
// Toggle del sidebar
toggle.addEventListener("click", () => {
    sidebar.classList.toggle("close");
})
// Abrir el sidebar al buscar
searchBtn.addEventListener("click", () => {
    sidebar.classList.remove("close");
})

//Funcion para cambiar entre el modo oscuro y claro y guardarlo en LocalStorage
modeSwitch.addEventListener("click", () => {
    const darkMode = localStorage.getItem('darkMode'); // Obtenemos el estado actual
    if (darkMode !== 'enabled') {
        body.classList.add('dark');
        localStorage.setItem('darkMode', 'enabled');  // Guardamos el estado como activado
        modeText.innerText = "Light mode";
    } else {
        body.classList.remove('dark');
        localStorage.setItem('darkMode', null);  // Guardamos el estado como desactivado
        modeText.innerText = "Dark mode";
    }
});


// Funcionalidad al sidebar:
// Selecciona todos los enlaces del sidebar
const sidebarLinks = document.querySelectorAll('.sidebar ul li a');
// Obtener el pathname de la URL actual como "../Pages/VerDetalleCurso.aspx")
const currentPath = window.location.pathname;
// Recorre los enlaces y verifica si el href coincide con la URL actual
sidebarLinks.forEach((link) => {
    // Si href del enlace incluye la ruta actual
    if (link.getAttribute('href').includes(currentPath)) {
        link.classList.add('active-link');// Agrega la clase active-link
    }
    // Agrega evento de click a cada enlace para manejar cambios en el click
    link.addEventListener('mousedown', () => {
        // Elimina la clase active-link de todos los enlaces
        sidebarLinks.forEach((otherLink) => {
            otherLink.classList.remove('active-link');
        });
        // Agrega la clase active-link al enlace seleccionado
        link.classList.add('active-link');
    });
});


// Agrega un evento de búsqueda al input
document.getElementById('searchInput').addEventListener('input', function () {
    const filter = this.value.toLowerCase();  // Obtienes el texto de búsqueda en minúsculas
    const menuItems = document.querySelectorAll('.menu-links .nav-link');  // Seleccionas todos los elementos del sidebar

    menuItems.forEach(function (item) {
        const text = item.querySelector('.nav-text').textContent.toLowerCase();  // Obtienes el texto del enlace en minúsculas

        // Si el texto del enlace incluye el texto de búsqueda, muestra, sino se oculta
        if (text.includes(filter)) {
            item.style.display = 'block';  // Mostramos el elemento si coincide
        } else {
            item.style.display = 'none';  // Ocultamos el elemento si no 
        }
    });
});