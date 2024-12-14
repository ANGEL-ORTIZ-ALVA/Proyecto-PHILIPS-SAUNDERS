document.addEventListener('DOMContentLoaded', function () {
    const form = document.getElementById('registroForm');
    const fechaNacimiento = document.getElementById('fechaNacimiento');
    const progressBar = document.querySelector('.progress-bar');
    const cargoSelect = document.getElementById('cargo');
    const tutorFields = document.getElementById('tutorFields');
    const successModal = new bootstrap.Modal(document.getElementById('successModal'));

    // Fecha maxima de nacimiento
    const today = new Date().toISOString().split('T')[0];
    fechaNacimiento.setAttribute('max', today);

    // Vlidatcion de nacimiento
    fechaNacimiento.addEventListener('input', function () {
        const selectedDate = new Date(this.value);
        const minDate = new Date();
        minDate.setFullYear(minDate.getFullYear() - 100); // edad maxima como 100 anios

        if (selectedDate > new Date() || selectedDate < minDate) {
            this.setCustomValidity('Por favor, seleccione una fecha de nacimiento válida (no mayor que hoy y no más de 100 años atrás).');
        } else {
            this.setCustomValidity('');
        }
    });

    // Validation para nombres and apellidos
    const nombreApellidoInputs = [document.getElementById('nombres'), document.getElementById('apellidos')];
    nombreApellidoInputs.forEach(input => {
        input.addEventListener('input', function () {
            if (this.value.trim().length < 2) {
                this.setCustomValidity('Debe tener al menos 2 caracteres.');
            } else if (!/^[A-Za-zÁáÉéÍíÓóÚúÑñ\s]+$/.test(this.value)) {
                this.setCustomValidity('Solo se permiten letras y espacios.');
            } else {
                this.setCustomValidity('');
            }
        });
    });

    // Validation para DNI
    const dniInput = document.getElementById('dni');
    dniInput.addEventListener('input', function () {
        if (!/^[0-9]{8}$/.test(this.value)) {
            this.setCustomValidity('El DNI debe tener exactamente 8 dígitos numéricos.');
        } else {
            this.setCustomValidity('');
        }
    });

    // Validation para teléfono
    const telefonoInput = document.getElementById('telefono');
    telefonoInput.addEventListener('input', function () {
        if (!/^[0-9]{9}$/.test(this.value)) {
            this.setCustomValidity('El número de teléfono debe tener exactamente 9 dígitos numéricos.');
        } else {
            this.setCustomValidity('');
        }
    });
    //Mostrar si es tutor
    cargoSelect.addEventListener('change', function () {
        if (this.value === 'Tutor') {
            tutorFields.style.display = 'block';
        } else {
            tutorFields.style.display = 'none';
        }
        updateProgressBar();
    });

    // Update progress bar
    function updateProgressBar() {
        const inputs = form.querySelectorAll('input:not([type="hidden"]), select, textarea');
        let totalInputs = inputs.length;
        let filledInputs = 0;

        if (cargoSelect.value !== 'Tutor') {
            totalInputs -= 2; // Subtract seccion and grado fields
        }

        inputs.forEach(input => {
            if (input.value.trim() !== '' &&
                (cargoSelect.value === 'Tutor' || (input.id !== 'seccion' && input.id !== 'grado'))) {
                filledInputs++;
            }
        });

        const progress = (filledInputs / totalInputs) * 100;
        progressBar.style.width = `${progress}%`;
        progressBar.setAttribute('aria-valuenow', progress);
    }

    form.addEventListener('input', updateProgressBar);

    form.addEventListener('submit', function (event) {
        if (!form.checkValidity()) {
            event.preventDefault();
            event.stopPropagation();
        } else {
            // Here you would typically send the form data to your backend
            console.log('Formulario enviado');
            // Prevent the form from actually submitting for this example
            event.preventDefault();

            // Show success modal
            successModal.show();

            // Reset form after modal is closed
            document.getElementById('successModal').addEventListener('hidden.bs.modal', function () {
                form.reset();
                form.classList.remove('was-validated');
                tutorFields.style.display = 'none';
                updateProgressBar();
            });
        }

        form.classList.add('was-validated');
    }, false);

    // Initial progress bar update
    updateProgressBar();
});