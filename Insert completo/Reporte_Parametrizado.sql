USE ClinicaDB
GO
CREATE  PROCEDURE sp_ReporteParametrizadoAdmin_Medico

	@DNI VARCHAR (50) = NULL,
	@nombre VARCHAR (50) = NULL,
	@apellido VARCHAR (50) = NULL,
	@nombreUsuario VARCHAR (50) = NULL,
	@contrasenia VARCHAR (50) = NULL,
	@Especialidad VARCHAR(30) = NULL,
	@sexo INT = NULL,
	@edad INT = NULL,
	@edad_min INT = NULL,
	@edad_max INT = NULL,
	@direccion NVARCHAR(100) = NULL,
	@fecha_nacimiento DATE = NULL,
	@correo VARCHAR (60) = NULL,
	@telefono VARCHAR(25) = NULL,
	@nacionalidad VARCHAR(50) = NULL,
	@activo BIT = NULL
AS
BEGIN
	SELECT ME.*
	FROM Medico ME
	WHERE (@nombre IS NULL OR ME.nombre LIKE '%' + @nombre + '%')
		AND (@apellido IS NULL OR ME.apellido LIKE '%' + @apellido + '%')
		AND (@sexo IS NULL OR ME.sexo = @sexo)
		AND (@edad IS NULL OR ME.edad = @edad
		AND (@edad_min IS NULL OR ME.edad >= @edad_min)
		AND (@edad_max IS NULL OR ME.edad <= @edad_max)
		AND (@direccion IS NULL OR ME.direccion LIKE '%' + @direccion + '%')
		AND (@fecha_nacimiento IS NULL OR ME.fecha_nacimiento = @fecha_nacimiento)
		AND (@nacionalidad IS NULL OR ME.nacionalidad = @nacionalidad)
		AND (
			@tiene_cuota_activa IS NULL
			OR @tiene_cuota_activa = 
				(SELECT CASE WHEN EXISTS (
					SELECT 1 
					FROM cuotas cu 
					WHERE ME.Legajo = cu.id_cliente AND ME.estado = 1
				) THEN 1 ELSE 0 END)
		)
END