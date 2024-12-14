-- Creacion de la base de datos
CREATE DATABASE ColegioBDv2;
GO
USE ColegioBDv2;
GO

-- Tablas
CREATE TABLE Estado_Usuario (
    ID_Estado_Usuario INT IDENTITY(1,1) PRIMARY KEY,
    Nombre_Estado_Usuario VARCHAR(50) NOT NULL,
    Estado_Registro VARCHAR(15) DEFAULT 'Registrado' CHECK(Estado_Registro IN('Registrado', 'Eliminado')) NOT NULL
);

CREATE TABLE Rol (
    ID_Rol INT IDENTITY(1,1) PRIMARY KEY,
    Nombre_Rol VARCHAR(50) NOT NULL,
    Permiso TEXT,
    Estado_Registro VARCHAR(15) DEFAULT 'Registrado' CHECK(Estado_Registro IN('Registrado', 'Eliminado')) NOT NULL
);

CREATE TABLE Usuario (
    ID_Usuario INT IDENTITY(1,1) PRIMARY KEY,
    Nombre_Usuario VARCHAR(60) UNIQUE NOT NULL,
    Password VARCHAR(255) NOT NULL,
    Fecha_Creacion DATETIME NOT NULL,
    Ultimo_Acceso DATETIME,-- Crear trigger para validar ultimo acceso sea mayo a la fecha de creacion
    Estado_Registro VARCHAR(15) DEFAULT 'Registrado' CHECK(Estado_Registro IN('Registrado', 'Eliminado')) NOT NULL,
    ID_Estado_Usuario INT,
    ID_Rol INT,
    FOREIGN KEY (ID_Estado_Usuario) REFERENCES Estado_Usuario(ID_Estado_Usuario),
    FOREIGN KEY (ID_Rol) REFERENCES Rol(ID_Rol)
);

CREATE TABLE Tipo_Personal (
    ID_Tipo_Personal INT IDENTITY(1,1) PRIMARY KEY,
    Nombre_Tipo_Personal VARCHAR(30) NOT NULL,
    Descripcion TEXT,
    Estado_Registro VARCHAR(15) DEFAULT 'Registrado' CHECK(Estado_Registro IN('Registrado', 'Eliminado')) NOT NULL
);

CREATE TABLE Genero (
    ID_Genero INT IDENTITY(1,1) PRIMARY KEY,
    Nombre_Genero VARCHAR(9) CHECK(Nombre_Genero IN('Masculino', 'Femenino')) NOT NULL,
    Estado_Registro VARCHAR(15) DEFAULT 'Registrado' CHECK(Estado_Registro IN('Registrado', 'Eliminado')) NOT NULL
);

CREATE TABLE Seccion (
    ID_Seccion INT IDENTITY(1,1) PRIMARY KEY,
    Nombre_Seccion VARCHAR(30) NOT NULL,
    Aforo INT CHECK(Aforo >= 15 and Aforo <=30 ) NOT NULL,
    Estado_Registro VARCHAR(15) DEFAULT 'Registrado' CHECK(Estado_Registro IN('Registrado', 'Eliminado')) NOT NULL
);

CREATE TABLE Grado (
    ID_Grado INT IDENTITY(1,1) PRIMARY KEY,
    Numero_Grado VARCHAR(7) NOT NULL,
    Estado_Registro VARCHAR(15) DEFAULT 'Registrado' CHECK(Estado_Registro IN('Registrado', 'Eliminado')) NOT NULL
);

CREATE TABLE Personal (
    ID_Personal INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(50) NOT NULL,
    Apellido VARCHAR(50) NOT NULL,
    Fecha_Nacimiento DATE CHECK(Fecha_Nacimiento < GETDATE())NOT NULL,-- Crear una Funcion que verifique que el personal sea mayor de edad
    DNI CHAR(8) UNIQUE CHECK(LEN(DNI) = 8) NOT NULL,
    Correo VARCHAR(100) CHECK(Correo LIKE '%@%'),
	Telefono VARCHAR(15) not null,
	Direccion VARCHAR(125) not null,
    Firma VARBINARY(MAX),
    Sello VARBINARY(MAX),
    Estado_Registro VARCHAR(15) DEFAULT 'Registrado' CHECK(Estado_Registro IN('Registrado', 'Eliminado')) NOT NULL,
    ID_Tipo_Personal INT,
    ID_Genero INT,
	ID_Grado INT,
	ID_Seccion INT,
    ID_Usuario INT,
    FOREIGN KEY (ID_Tipo_Personal) REFERENCES Tipo_Personal(ID_Tipo_Personal),
    FOREIGN KEY (ID_Genero) REFERENCES Genero(ID_Genero),
	FOREIGN KEY (ID_Grado) REFERENCES Grado(ID_Grado),
	FOREIGN KEY (ID_Seccion) REFERENCES Seccion(ID_Seccion),
    FOREIGN KEY (ID_Usuario) REFERENCES Usuario(ID_Usuario)
);

CREATE TABLE Estudiante (
    ID_Estudiante INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(50) NOT NULL,
    Apellido VARCHAR(50) NOT NULL,
    Fecha_Nacimiento DATE CHECK(Fecha_Nacimiento < GETDATE())NOT NULL,-- crear una funcion que permita verificar que el estudiante no tenga menos de 6 anios y que no sea posterior a la fecha actual
    DNI CHAR(8) UNIQUE CHECK(LEN(DNI) = 8) NOT NULL,
	Direccion VARCHAR(125) not null,
    Estado_Registro VARCHAR(15) DEFAULT 'Registrado' CHECK(Estado_Registro IN('Registrado', 'Eliminado')) NOT NULL,
    ID_Genero INT,
    ID_Grado INT,
    ID_Seccion INT,
    ID_Usuario INT,
    FOREIGN KEY (ID_Genero) REFERENCES Genero(ID_Genero),
    FOREIGN KEY (ID_Grado) REFERENCES Grado(ID_Grado),
    FOREIGN KEY (ID_Seccion) REFERENCES Seccion(ID_Seccion),
    FOREIGN KEY (ID_Usuario) REFERENCES Usuario(ID_Usuario)
);

CREATE TABLE Apoderado (
    ID_Apoderado INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(50) NOT NULL,
    Apellido VARCHAR(50) NOT NULL,
    DNI CHAR(8) UNIQUE CHECK(LEN(DNI) = 8) NOT NULL,
    Correo VARCHAR(100) CHECK(Correo LIKE '%@%'),
	Telefono VARCHAR(15) not null,
	Direccion VARCHAR(125) not null,
    Estado_Registro VARCHAR(15) DEFAULT 'Registrado' CHECK(Estado_Registro IN('Registrado', 'Eliminado')) NOT NULL,
    ID_Genero INT,
    ID_Usuario INT,
    FOREIGN KEY (ID_Genero) REFERENCES Genero(ID_Genero),
    FOREIGN KEY (ID_Usuario) REFERENCES Usuario(ID_Usuario)
);


CREATE TABLE Curso (
    ID_Curso INT IDENTITY(1,1) PRIMARY KEY,
    Nombre_Curso VARCHAR(50) NOT NULL,
    Descripcion TEXT,
    Estado_Registro VARCHAR(15) DEFAULT 'Registrado' CHECK(Estado_Registro IN('Registrado', 'Eliminado')) NOT NULL,
	ID_Personal int,
	foreign key (ID_Personal) references Personal(ID_Personal)
);


CREATE TABLE Periodo (
    ID_Periodo INT IDENTITY(1,1) PRIMARY KEY,
    Nombre_Periodo VARCHAR(50) NOT NULL,
    Descripcion TEXT,
    Estado_Registro VARCHAR(15) DEFAULT 'Registrado' CHECK(Estado_Registro IN('Registrado', 'Eliminado')) NOT NULL
);

CREATE TABLE Detalle_Curso (
	ID_Estudiante INT not null,
    ID_Curso INT NOT NULL,
    ID_Periodo INT NOT NULL,
    Competencia1 DECIMAL(5,2) DEFAULT(0.0) CHECK(Competencia1 >= 0.0 and Competencia1 <=20.0),
    Competencia2 DECIMAL(5,2) DEFAULT(0.0) CHECK(Competencia2 >= 0.0 and Competencia2 <=20.0),
    Competencia3 DECIMAL(5,2) DEFAULT(0.0) CHECK(Competencia3 >= 0.0 and Competencia3 <=20.0),
    Competencia4 DECIMAL(5,2) DEFAULT(0.0) CHECK(Competencia4 >= 0.0 and Competencia4 <=20.0),
    Proyecto DECIMAL(5,2) DEFAULT(0.0) CHECK(Proyecto >= 0.0 and Proyecto <=20.0),
    ExamenFinal DECIMAL(5,2) DEFAULT(0.0) CHECK(ExamenFinal >= 0.0 and ExamenFinal <=20.0),
    Estado_Registro VARCHAR(15) DEFAULT 'Registrado' CHECK(Estado_Registro IN('Registrado', 'Eliminado')) NOT NULL,
    PRIMARY KEY (ID_Estudiante, ID_Curso, ID_Periodo),
	FOREIGN KEY (ID_Estudiante) references Estudiante(ID_Estudiante),
    FOREIGN KEY (ID_Curso) REFERENCES Curso(ID_Curso),
    FOREIGN KEY (ID_Periodo) REFERENCES Periodo(ID_Periodo)
);

CREATE TABLE Apoderado_Estudiante (
    ID_Apoderado INT NOT NULL,
    ID_Estudiante INT NOT NULL,
    Parentesco VARCHAR(20) NOT NULL,
    Estado_Registro VARCHAR(15) DEFAULT 'Registrado' CHECK(Estado_Registro IN('Registrado', 'Eliminado')) NOT NULL,
    PRIMARY KEY (ID_Apoderado, ID_Estudiante),
    FOREIGN KEY (ID_Apoderado) REFERENCES Apoderado(ID_Apoderado),
    FOREIGN KEY (ID_Estudiante) REFERENCES Estudiante(ID_Estudiante)
);


/* eliminar base de datos
Primero ejecutar esto

USE master;
GO
SELECT session_id, login_name
FROM sys.dm_exec_sessions
WHERE database_id = DB_ID('ColegioBDv2');



USE master;
GO
ALTER DATABASE ColegioBDv2
SET SINGLE_USER
WITH ROLLBACK IMMEDIATE;
GO

DROP DATABASE ColegioBDv2;
GO
*/

select * from Seccion;