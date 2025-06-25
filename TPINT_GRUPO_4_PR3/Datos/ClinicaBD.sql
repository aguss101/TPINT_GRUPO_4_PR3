DROP DATABASE IF EXISTS ClinicaDB;
GO

CREATE DATABASE ClinicaDB;
GO

USE ClinicaDB;
GO

-- Tabla: Roles
CREATE TABLE Roles (
    idRol INT PRIMARY KEY,
    descripcion VARCHAR(25)
);

-- Tabla: Sexos
CREATE TABLE Sexos (
    idSexo INT PRIMARY KEY,
    descripcion VARCHAR(25)
);

-- Tabla: Paises
CREATE TABLE Paises (
    idPais INT PRIMARY KEY,
    nombrePais VARCHAR(25) UNIQUE,
    gentilicio VARCHAR(25) UNIQUE
);

-- Tabla: Provincias
CREATE TABLE Provincias (
    idProvincia INT PRIMARY KEY,
    nombreProvincia VARCHAR(35) UNIQUE
);

-- Tabla: Localidades
CREATE TABLE Localidades (
    idLocalidad INT PRIMARY KEY,
    nombreLocalidad VARCHAR(35),
    idProvincia INT FOREIGN KEY REFERENCES Provincias(idProvincia)
);

-- Tabla: Persona
CREATE TABLE Persona (
    DNI VARCHAR(20) PRIMARY KEY,
    nombre VARCHAR(25),
    apellido VARCHAR(25),
    sexo INT FOREIGN KEY REFERENCES Sexos(idSexo),
    direccion VARCHAR(60),
    idLocalidad INT FOREIGN KEY REFERENCES Localidades(idLocalidad),
    fechaNacimiento DATE,
    nacionalidad VARCHAR(25) FOREIGN KEY REFERENCES Paises(gentilicio),
    activo BIT NOT NULL DEFAULT 1
);

-- Tabla: Usuario
CREATE TABLE Usuario (
    DNI VARCHAR(20) PRIMARY KEY,
    idRol INT,
    nombreUsuario VARCHAR(25),
    contrasenia VARCHAR(25),
    ultimoIngreso DATETIME,
    alta DATETIME,
    FOREIGN KEY (idRol) REFERENCES Roles(idRol),
    FOREIGN KEY (DNI) REFERENCES Persona(DNI) ON UPDATE CASCADE
);

-- Tabla: Telefonos
CREATE TABLE Telefonos (
    idPersona VARCHAR(20),
    telefono VARCHAR(25),
    PRIMARY KEY (idPersona, telefono),
    FOREIGN KEY (idPersona) REFERENCES Persona(DNI) ON UPDATE CASCADE
);

-- Tabla: Correos
CREATE TABLE Correos (
    idPersona VARCHAR(20),
    correo VARCHAR(40),
    PRIMARY KEY (idPersona, correo),
    FOREIGN KEY (idPersona) REFERENCES Persona(DNI) ON UPDATE CASCADE
);

-- Tabla: Especialidades
CREATE TABLE Especialidades (
    idEspecialidad INT PRIMARY KEY,
    descripcion VARCHAR(25)
);

-- Tabla: Medico
CREATE TABLE Medico (
    Legajo VARCHAR(20),
    DNI VARCHAR(20),
    idEspecialidad INT,
    PRIMARY KEY (DNI, Legajo),
    UNIQUE (DNI),
    UNIQUE (Legajo),
    FOREIGN KEY (DNI) REFERENCES Persona(DNI) ON UPDATE CASCADE,
    FOREIGN KEY (idEspecialidad) REFERENCES Especialidades(idEspecialidad)
);

-- Tabla: Jornadas
CREATE TABLE Jornadas (
    Legajo VARCHAR(20),
    DiaSemana VARCHAR(20),
    rangoHorario TIME(0),
    PRIMARY KEY (Legajo, DiaSemana, rangoHorario),
    FOREIGN KEY (Legajo) REFERENCES Medico(Legajo) ON UPDATE CASCADE
);

-- Tabla: EstadoTurnos
CREATE TABLE EstadoTurnos (
    idEstado INT PRIMARY KEY,
    valorEstado BIT,
    descripcion VARCHAR(25)
);

-- Tabla: ObraSocial
CREATE TABLE ObraSocial (
    idObraSocial INT PRIMARY KEY,
    nombre VARCHAR(25)
);

-- Tabla: Paciente
CREATE TABLE Paciente (
    DNI VARCHAR(20) PRIMARY KEY,
    ObraSocial INT,
    ultimaAtencion DATETIME,
    alta DATETIME,
    FOREIGN KEY (DNI) REFERENCES Persona(DNI) ON UPDATE CASCADE,
    FOREIGN KEY (ObraSocial) REFERENCES ObraSocial(idObraSocial)
);

-- Tabla: Turnos
CREATE TABLE Turnos(
    Legajo VARCHAR(20),
    DNIPaciente VARCHAR(20),
    fechaPactada DATETIME,
    estado INT,
    observacion VARCHAR(200),
    diagnostico VARCHAR(50),
    PRIMARY KEY (Legajo, fechaPactada),
    FOREIGN KEY (Legajo) REFERENCES Medico(Legajo) ON UPDATE CASCADE,
    FOREIGN KEY (DNIPaciente) REFERENCES Paciente(DNI) ON UPDATE NO ACTION,
    FOREIGN KEY (estado) REFERENCES EstadoTurnos(idEstado)
);
