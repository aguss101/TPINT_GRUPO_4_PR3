USE ClinicaDB
GO

CREATE OR ALTER VIEW vw_TurnosConDatos AS
SELECT
    T.Legajo,
    T.DNIPaciente,
    T.fechaPactada,
    T.estado                     AS EstadoId,                  --  VER TODOS LOS TURNOS CON SUS DATOS.
    ET.descripcion               AS EstadoDescripcion,
    T.observacion,
    T.diagnostico,
    P_Pac.nombre + ' ' + P_Pac.apellido AS Paciente,
	P_Pac.apellido AS ApellidoPaciente,						   -- PARA LA FUNC FiltrarPacientexApellido EN CONSULTASTURNOS.CS
    OS.nombre                        AS ObraSocial,
    P_Med.nombre + ' ' + P_Med.apellido AS Medico,
    M.idEspecialidad,
    E.descripcion                     AS Especialidad
FROM Turnos T
INNER JOIN Paciente      PA ON T.DNIPaciente = PA.DNI
INNER JOIN Persona       P_Pac ON PA.DNI        = P_Pac.DNI
LEFT  JOIN ObraSocial    OS    ON PA.ObraSocial = OS.idObraSocial
INNER JOIN Medico        M     ON T.Legajo       = M.Legajo
INNER JOIN Persona       P_Med ON M.DNI          = P_Med.DNI
INNER JOIN Especialidades E    ON M.idEspecialidad = E.idEspecialidad
INNER JOIN EstadoTurnos  ET    ON T.estado       = ET.idEstado;


GO
CREATE OR ALTER VIEW vw_MedicoConDatos AS 
SELECT
	ME.*, PE.nombre, 
	PE.apellido AS apellido, 
	PE.nacionalidad, 
	PE.fechaNacimiento, 
	PE.Direccion, 
	S.idSexo, 
	S.descripcion AS genero,
	C.correo,
	T.telefono,
	L.idLocalidad,
	L.nombreLocalidad,
	P.idProvincia, 
	P.nombreProvincia,
    U.nombreUsuario,
	U.contrasenia,
	E.descripcion AS Especialidad,
	PE.activo AS activo,
	PE.DNI AS dniMedico,
	ME.Legajo AS legajoMedico
FROM Medico ME
INNER JOIN Persona PE ON ME.DNI = PE.DNI
INNER JOIN Usuario U ON PE.DNI = U.DNI
INNER JOIN Sexos S ON PE.sexo = S.idSexo
INNER JOIN Especialidades E ON ME.idEspecialidad = E.idEspecialidad
INNER JOIN Localidades L ON PE.idLocalidad = L.idLocalidad
INNER JOIN Provincias P ON L.idProvincia = P.idProvincia
LEFT JOIN Telefonos T ON PE.DNI = T.idPersona
LEFT JOIN Correos C ON PE.DNI = C.idPersona



GO
CREATE OR ALTER VIEW vw_PacienteConDatos AS
SELECT
	PA.*,
	PE.nombre, 
	PE.apellido, 
	PE.nacionalidad, 
	PE.fechaNacimiento, 
	PE.Direccion,
	S.idSexo,
	S.descripcion AS genero,
	O.idObraSocial,
	O.nombre AS nombreObraSocial,
	C.correo, T.telefono,
	L.idLocalidad,
	L.nombreLocalidad,
	P.idProvincia,
	P.nombreProvincia,
	PE.activo AS activo,
	PA.DNI AS dniPaciente
FROM Paciente PA
INNER JOIN Persona PE ON PA.DNI = PE.DNI INNER JOIN Sexos S ON PE.sexo = S.idSexo 
INNER JOIN ObraSocial O ON PA.ObraSocial = O.idObraSocial 
INNER JOIN Localidades L ON PE.idLocalidad = L.idLocalidad 
INNER JOIN Provincias P ON L.idProvincia = P.idProvincia
LEFT JOIN Correos C ON PE.DNI = C.idPersona
LEFT JOIN Telefonos T ON PE.DNI = T.idPersona