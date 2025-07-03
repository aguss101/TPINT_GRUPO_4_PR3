USE ClinicaDB
GO
--MEDICOS
--Medicos por especialidad
SELECT E.descripcion AS Especialidad, COUNT(ME.Legajo ) AS CantMedicos FROM Especialidades E
LEFT JOIN Medico ME ON ME.idEspecialidad = E.idEspecialidad
GROUP BY E.descripcion
ORDER BY CantMedicos DESC

-- Cantidad de ausentes por mes
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

--PACIENTE
--PacientexEdad
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


--TURNOS
--PromediosTurnosxEspecialidad
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