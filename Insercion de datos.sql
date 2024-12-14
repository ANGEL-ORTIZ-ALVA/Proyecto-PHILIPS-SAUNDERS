use ColegioBDv2;
INSERT INTO Seccion (Nombre_Seccion, Aforo)
VALUES 
    ('A', 25),
    ('B', 30),
    ('C', 20),
    ('D', 18),
    ('E', 22);

INSERT INTO Estado_Usuario (Nombre_Estado_Usuario)
VALUES 
    ('Activo'),
    ('Inactivo'),
    ('Suspendido'),
    ('Pendiente'),
    ('Deshabilitado');

INSERT INTO Rol (Nombre_Rol, Permiso)
VALUES 
    ('Administrador', 'Permiso total'),
    ('Docente', 'Acceso a cursos y calificaciones'),
    ('Estudiante', 'Acceso a sus notas y asistencia'),
    ('Apoderado', 'Acceso a notas y asistencias de estudiantes a cargo'),
    ('Secretaria', 'Gestión de asistencias y matrícula');

INSERT INTO Usuario (Nombre_Usuario, Password, Fecha_Creacion, Ultimo_Acceso, ID_Estado_Usuario, ID_Rol)
VALUES 
    ('admin_user', 'hashed_password1', GETDATE(), GETDATE(), 1, 1),
    ('profesor_jose', 'hashed_password2', GETDATE(), GETDATE(), 1, 2),
    ('estudiante_juan', 'hashed_password3', GETDATE(), NULL, 1, 3),
    ('apoderado_maria', 'hashed_password4', GETDATE(), NULL, 1, 4),
    ('secretaria_ana', 'hashed_password5', GETDATE(), GETDATE(), 1, 5);

INSERT INTO Genero (Nombre_Genero)
VALUES 
    ('Masculino'),
    ('Femenino');

INSERT INTO Grado (Numero_Grado)
VALUES 
    ('Primero'),
    ('Segundo'),
    ('Tercero'),
    ('Cuarto'),
    ('Quinto');

INSERT INTO Estudiante (Nombre, Apellido, Fecha_Nacimiento, DNI, Direccion, ID_Genero, ID_Grado, ID_Seccion, ID_Usuario)
VALUES 
    ('Juan', 'Perez', '2010-05-10', '12345678', 'Av. Lima 123', 1, 1, 1, 1),
    ('Maria', 'Lopez', '2011-08-20', '87654321', 'Jr. Arequipa 456', 2, 2, 2, 2),
    ('Carlos', 'Garcia', '2010-03-15', '13579246', 'Calle Sol 789', 1, 3, 3, 3),
    ('Lucia', 'Torres', '2012-01-25', '24681357', 'Av. Pardo 321', 2, 1, 1, 4),
    ('Ana', 'Gutierrez', '2011-11-02', '11223344', 'Jr. Prado 654', 2, 2, 2, 5);

INSERT INTO Periodo (Nombre_Periodo, Descripcion)
VALUES 
    ('Primer Trimestre', 'Periodo de evaluación inicial del año académico'),
    ('Segundo Trimestre', 'Periodo intermedio del año académico'),
    ('Tercer Trimestre', 'Periodo final del año académico'),
    ('Verano', 'Periodo de cursos de verano'),
    ('Invierno', 'Periodo de cursos de invierno');

INSERT INTO Tipo_Personal (Nombre_Tipo_Personal, Descripcion)
VALUES 
    ('Docente', 'Encargado de la enseñanza de materias'),
    ('Administrativo', 'Personal administrativo del colegio'),
    ('Psicólogo', 'Encargado del bienestar emocional de los estudiantes'),
    ('Director', 'Líder administrativo del colegio'),
    ('Bibliotecario', 'Encargado de la biblioteca');

INSERT INTO Personal (Nombre, Apellido, Fecha_Nacimiento, DNI, Correo, Telefono, Direccion, ID_Tipo_Personal, ID_Genero, ID_Grado, ID_Seccion, ID_Usuario)
VALUES 
    ('Jose', 'Martinez', '1980-05-15', '12345678', 'jose.martinez@colegio.edu', '987654321', 'Av. Libertad 123', 1, 1, 1, 1, 1),
    ('Ana', 'Gomez', '1985-07-20', '87654321', 'ana.gomez@colegio.edu', '987654322', 'Av. Paz 456', 2, 2, 2, 2, 2),
    ('Luis', 'Rojas', '1978-08-30', '13579246', 'luis.rojas@colegio.edu', '987654323', 'Av. Progreso 789', 3, 1, 3, 3, 3),
    ('Maria', 'Flores', '1975-09-25', '24681357', 'maria.flores@colegio.edu', '987654324', 'Av. Unión 321', 4, 2, 4, 4, 4),
    ('Carlos', 'Castro', '1990-10-10', '11223344', 'carlos.castro@colegio.edu', '987654325', 'Av. Victoria 654', 5, 1, 5, 5, 5);

INSERT INTO Curso (Nombre_Curso, Descripcion, ID_Personal)
VALUES 
    ('Matemáticas', 'Curso de matemáticas básica y avanzada', 1),
    ('Lenguaje', 'Curso de comprensión lectora y redacción', 2),
    ('Ciencia', 'Curso de ciencias naturales', 3),
    ('Historia', 'Curso de historia del Perú y del mundo', 4),
    ('Educación Física', 'Curso de actividad física y deportes', 5);

select * from Curso;

select * from Detalle_Curso;

Select * from Estudiante;

SELECT 
    e.Nombre AS NombreEstudiante,
    e.Apellido AS ApellidoEstudiante,
    c.Nombre_Curso AS NombreCurso,
    p.Nombre_Periodo AS NombrePeriodo,
    dc.Competencia1,
    dc.Competencia2,
    dc.Competencia3,
    dc.Competencia4,
    dc.Proyecto,
    dc.ExamenFinal,
    dc.Estado_Registro
FROM 
    Detalle_Curso dc
JOIN 
    Estudiante e ON dc.ID_Estudiante = e.ID_Estudiante
JOIN 
    Curso c ON dc.ID_Curso = c.ID_Curso
JOIN 
    Periodo p ON dc.ID_Periodo = p.ID_Periodo
ORDER BY 
    e.Nombre, c.Nombre_Curso, p.Nombre_Periodo;




SELECT 
    e.ID_Estudiante,
    ISNULL(dc.Competencia1, 0) AS Competencia1,
    ISNULL(dc.Competencia2, 0) AS Competencia2,
    ISNULL(dc.Competencia3, 0) AS Competencia3,
    ISNULL(dc.Competencia4, 0) AS Competencia4,
    ISNULL(dc.Proyecto, 0) AS Proyecto,
    ISNULL(dc.ExamenFinal, 0) AS ExamenFinal,
    ISNULL(dc.Estado_Registro, 'Registrado') AS Estado_Registro
FROM Estudiante e
LEFT JOIN Detalle_Curso dc ON e.ID_Estudiante = dc.ID_Estudiante 
                            AND dc.ID_Curso = 2 
                            AND dc.ID_Periodo = 1
WHERE e.ID_Seccion = 1
ORDER BY e.Apellido, e.Nombre
INSERT INTO Detalle_Curso (ID_Estudiante, ID_Curso, ID_Periodo, Competencia1, Competencia2, Competencia3, Competencia4, Proyecto, ExamenFinal, Estado_Registro)
VALUES
    (1, 1, 1, 15.5, 16.0, 14.0, 18.0, 17.0, 16.5, 'Registrado'), -- Juan - Matemáticas - Primer Trimestre
    (2, 2, 2, 13.0, 14.5, 16.0, 14.0, 15.0, 13.5, 'Registrado'), -- María - Lenguaje - Segundo Trimestre
    (3, 3, 3, 12.0, 13.5, 14.0, 13.0, 14.5, 15.0, 'Registrado'), -- Carlos - Ciencia - Tercer Trimestre
    (4, 4, 4, 16.0, 15.5, 17.0, 16.5, 18.0, 17.5, 'Registrado'), -- Lucía - Historia - Verano
    (5, 5, 5, 18.0, 19.0, 17.5, 18.5, 16.5, 17.0, 'Registrado'); -- Ana - Educación Física - Invierno

INSERT INTO Apoderado (Nombre, Apellido, DNI, Correo, Telefono, Direccion, ID_Genero, ID_Usuario)
VALUES 
    ('Pedro', 'Perez', '12345679', 'pedro.perez@colegio.edu', '987654326', 'Av. Lima 123', 1, 1),
    ('Elena', 'Castillo', '87654322', 'elena.castillo@colegio.edu', '987654327', 'Jr. Arequipa 456', 2, 2),
    ('Raul', 'Garcia', '11223355', 'raul.garcia@colegio.edu', '987654328', 'Calle Sol 789', 1, 3),
    ('Sofia', 'Mendez', '44556677', 'sofia.mendez@colegio.edu', '987654329', 'Av. Pardo 321', 2, 4),
    ('Laura', 'Alvarez', '99887766', 'laura.alvarez@colegio.edu', '987654330', 'Jr. Prado 654', 2, 5);

INSERT INTO Apoderado_Estudiante (ID_Apoderado, ID_Estudiante, Parentesco, Estado_Registro)
VALUES 
    (1, 1, 'Padre', 'Registrado'), -- Pedro es el padre de Juan
    (2, 2, 'Madre', 'Registrado'), -- Elena es la madre de María
    (3, 3, 'Padre', 'Registrado'), -- Raul es el padre de Carlos
    (4, 4, 'Madre', 'Registrado'), -- Sofia es la madre de Lucia
    (5, 5, 'Madre', 'Registrado'); -- Laura es la madre de Ana

	select * from Detalle_Curso
	select * from Estudiante
	select * from Apoderado
	select * from Apoderado_Estudiante
	select * from Grado
	select * from Seccion
	select * from Usuario
	
	