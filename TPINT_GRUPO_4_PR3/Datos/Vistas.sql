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
