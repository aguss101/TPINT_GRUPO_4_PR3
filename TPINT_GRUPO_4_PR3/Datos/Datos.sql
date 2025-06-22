-- INSERCIONES DE DATOS

-- Roles
INSERT INTO Roles VALUES (1, 'Administrador'), (2, 'Médico'), (3, 'Paciente');

-- Sexos
INSERT INTO Sexos VALUES (1, 'Masculino'), (2, 'Femenino'), (3, 'Otro');

-- Paises
INSERT INTO Paises VALUES (1, 'Argentina', 'argentino'), (2, 'Uruguay', 'uruguayo');

-- Provincias
INSERT INTO Provincias VALUES (1, 'Buenos Aires'), (2, 'Córdoba');

-- Localidades
INSERT INTO Localidades VALUES (1, 'Pilar', 1), (2, 'Villa Carlos Paz', 2);

-- Personas
INSERT INTO Persona VALUES 
('12345678', 'Juan', 'Pérez', 1, 'Calle Falsa 123', 1, '1985-06-15', 'argentino', 1),
('87654321', 'María', 'Gómez', 2, 'Av Siempreviva 742', 2, '1990-09-22', 'uruguayo', 1);

-- Usuarios (clave simple: 123)
INSERT INTO Usuario VALUES 
('12345678', 2, 'medico1', '123', GETDATE(), GETDATE()),
('87654321', 3, 'paciente1', '123', GETDATE(), GETDATE());

-- Teléfonos
INSERT INTO Telefonos VALUES 
('12345678', '1122334455'),
('87654321', '1199887766');

-- Correos
INSERT INTO Correos VALUES 
('12345678', 'juanp@salud.com'),
('87654321', 'mariag@paciente.com');

-- Especialidades
INSERT INTO Especialidades VALUES 
(1, 'Clínico'),
(2, 'Pediatría');

-- Médico
INSERT INTO Medico VALUES 
('12345678', 'M001', 1);

-- Jornadas
INSERT INTO Jornadas VALUES 
('M001', 'Lunes', '09:00'),
('M001', 'Martes', '10:00');

-- Obra Social
INSERT INTO ObraSocial VALUES 
(1, 'PAMI'),
(2, 'OSDE');

-- Paciente
INSERT INTO Paciente VALUES 
('87654321', 2, GETDATE(), GETDATE());

-- Estado de turnos
INSERT INTO EstadoTurnos VALUES 
(1, 1, 'Confirmado'),
(2, 0, 'Cancelado');

-- Turno
INSERT INTO Turnos VALUES 
('M001', '87654321', DEFAULT, 1, 'Dolor abdominal', 'Gastritis');