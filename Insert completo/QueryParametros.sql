USE ClinicaDB
GO
--MEDICOS
--Medicos por especialidad
SELECT E.descripcion AS Especialidad, COUNT(ME.Legajo ) AS CantMedicos FROM Especialidades E
LEFT JOIN Medico ME ON ME.idEspecialidad = E.idEspecialidad
GROUP BY E.descripcion
ORDER BY CantMedicos DESC

 --Cantidad de ausentes por mes
SELECT 
    YEAR(T.fechaPactada) AS Anio,
    MONTH(T.fechaPactada) AS Mes,
    COUNT(*) AS CantidadAusentes
FROM 
    Turnos T
INNER JOIN 
    EstadoTurnos ET ON T.estado = ET.idEstado
WHERE 
    ET.descripcion = 'AUSENTE'
GROUP BY 
    YEAR(T.fechaPactada), 
    MONTH(T.fechaPactada)
ORDER BY 
    Anio, Mes;

--MedicosxEdad
SELECT 
  CASE 
    WHEN edad <= 30 THEN 'Joven'
    WHEN edad <= 50 THEN 'Adulto'
    ELSE 'AdultoMayor'
  END AS CategoriaEdad,
  COUNT(*) AS Cantidad
FROM (
  SELECT DATEDIFF(YEAR, fechaNacimiento, GETDATE()) AS edad
  FROM Persona PE
  INNER JOIN Medico ME ON ME.DNI = PE.DNI
) AS sub
GROUP BY 
  CASE 
    WHEN edad <= 30 THEN 'Joven'
    WHEN edad <= 50 THEN 'Adulto'
    ELSE 'AdultoMayor'
  END
ORDER BY CategoriaEdad;

PACIENTE
PacientexEdad
SELECT 
  CASE 
    WHEN edad <= 30 THEN 'Joven'
    WHEN edad <= 50 THEN 'Adulto'
    ELSE 'AdultoMayor'
  END AS CategoriaEdad,
  COUNT(*) AS Cantidad
FROM (
  SELECT DATEDIFF(YEAR, fechaNacimiento, GETDATE()) AS edad
  FROM Persona PE
  INNER JOIN Paciente PA ON PA.DNI = PE.DNI
) AS sub
GROUP BY 
  CASE 
    WHEN edad <= 30 THEN 'Joven'
    WHEN edad <= 50 THEN 'Adulto'
    ELSE 'AdultoMayor'
  END
ORDER BY CategoriaEdad;


--Cantidad de pacientesxObraSocial
SELECT O.nombre AS ObraSocial, COUNT(PA.DNI ) AS CantPacientes FROM Paciente PA
LEFT JOIN ObraSocial O ON PA.ObraSocial = O.idObraSocial
GROUP BY O.nombre
ORDER BY CantPacientes DESC



--TURNOS
-- PromediosTurnosxEspecialidad
SELECT E.descripcion AS Especialidad, AVG (CantidadTurnos * 1.0) AS PromediosTurnosxEspecialidad 
FROM (
SELECT ME.Legajo,
ME.idEspecialidad,
COUNT(*) AS CantidadTurnos 
FROM Medico ME
LEFT JOIN Turnos T ON T.Legajo = ME.Legajo
GROUP BY ME.Legajo, ME.idEspecialidad
) AS Sub
INNER JOIN Especialidades E ON E.idEspecialidad = Sub.idEspecialidad
GROUP BY E.descripcion
ORDER BY PromediosTurnosxEspecialidad


SELECT
  ET.descripcion     AS Estado,   -- Pacientes x estado
  COUNT(*)           AS CantidadTurnos
FROM Turnos T
JOIN EstadoTurnos ET
  ON T.estado = ET.idEstado
GROUP BY ET.descripcion
ORDER BY CantidadTurnos DESC;



SELECT
  S.descripcion      AS Sexo,
  COUNT(*)           AS CantidadPacientes
FROM Paciente PA
JOIN Persona P
  ON PA.DNI = P.DNI
JOIN Sexos S
  ON P.sexo = S.idSexo
GROUP BY S.descripcion
ORDER BY CantidadPacientes DESC;  -- Paciente x sexo


SELECT
          m.Legajo,
          p.nombre + ' ' + p.apellido AS NombreCompleto, -- Cantidad de turnos x medico
          COUNT(*)              AS CantidadTurnos
        FROM Turnos t
        JOIN Medico m
          ON t.Legajo = m.Legajo
        JOIN Persona p
          ON m.DNI = p.DNI
        GROUP BY
          m.Legajo, p.nombre, p.apellido
        ORDER BY
          CantidadTurnos DESC;