CREATE DATABASE ClinicaDB

USE ClinicaDB
-- Tabla Roles
CREATE TABLE Roles (
  idRol INT PRIMARY KEY,
  descripcion VARCHAR(25)
);

-- Tabla Sexos
CREATE TABLE Sexos (
  idSexo INT PRIMARY KEY,
  descripcion VARCHAR(25)
);

-- Tabla Paises
CREATE TABLE Paises (
  idPais INT PRIMARY KEY,
  nombrePais VARCHAR(25),
  gentilicio VARCHAR(25)
);

-- Tabla Provincias
CREATE TABLE Provincias (
  idProvincia INT PRIMARY KEY,
  nombreProvincia NVARCHAR(255) UNIQUE
);

-- Tabla Localidades
CREATE TABLE Localidades (
  idLocalidad INT PRIMARY KEY,
  nombreLocalidad NVARCHAR(255),
  idProvincia INT,
  FOREIGN KEY (idProvincia) REFERENCES Provincias(idProvincia)
);

-- Tabla Persona
CREATE TABLE Persona (
  DNI NVARCHAR(255) PRIMARY KEY,
  nombre VARCHAR(25),
  apellido VARCHAR(25),
  sexo INT,
  direccion VARCHAR(25),
  idLocalidad INT,
  fechaNacimiento DATE,
  idPais INT,
  FOREIGN KEY (sexo) REFERENCES Sexos(idSexo),
  FOREIGN KEY (idLocalidad) REFERENCES Localidades(idLocalidad),
  FOREIGN KEY (idPais) REFERENCES Paises(idPais)
);

-- Tabla Usuario (Admins y Médicos)
CREATE TABLE Usuario (
  DNI NVARCHAR(255) PRIMARY KEY,
  idRol INT,
  nombreUsuario VARCHAR(25),
  contrasenia VARCHAR(25),
  ultimoIngreso DATETIME,
  alta DATETIME,
  FOREIGN KEY (DNI) REFERENCES Persona(DNI),
  FOREIGN KEY (idRol) REFERENCES Roles(idRol)
);


-- Tabla Especialidades
CREATE TABLE Especialidades (
  idEspecialidad INT PRIMARY KEY,
  descripcion VARCHAR(25)
);

-- Tabla Medico
CREATE TABLE Medico (
  DNI NVARCHAR(255) PRIMARY KEY,
  Legajo NVARCHAR(255) UNIQUE,
  idEspecialidad INT,
  FOREIGN KEY (DNI) REFERENCES Usuario(DNI),
  FOREIGN KEY (idEspecialidad) REFERENCES Especialidades(idEspecialidad)
);

-- Tabla Telefonos
CREATE TABLE Telefonos (
  DNI NVARCHAR(255),
  telefono VARCHAR(25) UNIQUE,
  PRIMARY KEY (DNI, telefono),
  FOREIGN KEY (DNI) REFERENCES Persona(DNI)
);

-- Tabla Correos
CREATE TABLE Correos (
  DNI NVARCHAR(255),
  correo VARCHAR(25) UNIQUE,
  PRIMARY KEY (DNI, correo),
  FOREIGN KEY (DNI) REFERENCES Persona(DNI)
);

-- Tabla Jornadas
CREATE TABLE Jornadas (
  Legajo NVARCHAR(255),
  DiaSemana NVARCHAR(255),
  rangoHorario TIME,
  PRIMARY KEY (Legajo, DiaSemana, rangoHorario),
  FOREIGN KEY (Legajo) REFERENCES Medico(Legajo)
);

-- Tabla EstadoTurnos
CREATE TABLE EstadoTurnos (
  idEstado INT PRIMARY KEY,
  descripcion VARCHAR(25)
);

-- Tabla Turnos
 CREATE TABLE Turnos (
  idTurno INT IDENTITY PRIMARY KEY,
  Legajo NVARCHAR(255),
  DNIPaciente NVARCHAR(255),
  fechaPactada DATE,
  hora TIME,
  estado INT,
  observacion VARCHAR(200),
  FOREIGN KEY (Legajo) REFERENCES Medico(Legajo),
  FOREIGN KEY (DNIPaciente) REFERENCES Persona(DNI),
  FOREIGN KEY (estado) REFERENCES EstadoTurnos(idEstado),
  CONSTRAINT UQ_Turno_Unico UNIQUE (Legajo, fechaPactada, hora) --> Para que no se pisen los turnos.
);
