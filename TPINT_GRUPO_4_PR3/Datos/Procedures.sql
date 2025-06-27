USE ClinicaDB;

GO

CREATE
	OR

ALTER PROCEDURE sp_AltaPaciente @DNI VARCHAR(20),
	@Nombre VARCHAR(25),
	@Apellido VARCHAR(25),
	@Nacionalidad VARCHAR(25),
	@FechaNacimiento DATE,
	@Sexo INT,
	@IdLocalidad INT,
	@ObraSocial INT,
	@UltimaAtencion DATETIME,
	@Alta DATETIME,
	@Direccion VARCHAR(60),
	@Telefono VARCHAR(25),
	@Correo VARCHAR(40)
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION;

		IF EXISTS (
				SELECT 1
				
				FROM Persona
				
				WHERE DNI = @DNI
				)
		BEGIN
			RAISERROR (
					'Ya existe una persona con ese DNI.',
					16,
					1
					);

			ROLLBACK TRANSACTION;

			RETURN;
		
		END

		INSERT INTO Persona (
			DNI,
			nombre,
			apellido,
			sexo,
			direccion,
			idLocalidad,
			fechaNacimiento,
			nacionalidad
			)
		
		VALUES (
			@DNI,
			@Nombre,
			@Apellido,
			@Sexo,
			@Direccion,
			@IdLocalidad,
			@FechaNacimiento,
			@Nacionalidad
			);

		INSERT INTO Paciente (
			DNI,
			ObraSocial,
			ultimaAtencion,
			alta
			)
		
		VALUES (
			@DNI,
			@ObraSocial,
			@UltimaAtencion,
			@Alta
			);

		INSERT INTO Telefonos (
			idPersona,
			telefono
			)
		
		VALUES (
			@DNI,
			@telefono
			)

		INSERT INTO Correos (
			idPersona,
			correo
			)
		
		VALUES (
			@DNI,
			@Correo
			)

		COMMIT TRANSACTION;

		PRINT 'Paciente agregado exitosamente.';
	
	END TRY

	BEGIN CATCH
		ROLLBACK TRANSACTION;

		PRINT 'Error: ' + ERROR_MESSAGE();
	
	END CATCH

END;

GO

CREATE
	OR

ALTER PROCEDURE sp_EliminarPaciente @DNI VARCHAR(20)
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION;

		IF NOT EXISTS (
				SELECT 1
				
				FROM Paciente
				
				WHERE DNI = @DNI
				)
		BEGIN
			RAISERROR (
					'No existe ningún paciente con ese DNI.',
					16,
					1
					);

			ROLLBACK TRANSACTION;

			RETURN;
		
		END

		UPDATE Persona
		
		SET activo = 0
		
		WHERE DNI = @DNI;

		COMMIT TRANSACTION;

		PRINT 'Paciente eliminado correctamente.';
	
	END TRY

	BEGIN CATCH
		ROLLBACK TRANSACTION;

		PRINT 'Error: ' + ERROR_MESSAGE();
	
	END CATCH

END;

GO

CREATE
	OR

ALTER PROCEDURE sp_ModificarPaciente @DNI_VIEJO VARCHAR(20),
	@DNI_NUEVO VARCHAR(20),
	@Nombre VARCHAR(25),
	@Apellido VARCHAR(25),
	@Nacionalidad VARCHAR(25),
	@FechaNacimiento DATE,
	@Sexo INT,
	@IdLocalidad INT,
	@Direccion VARCHAR(60),
	@ObraSocial INT,
	@Correo VARCHAR(40) = NULL,
	@Telefono VARCHAR(25) = NULL
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION;

		-- Validar que existe paciente viejo
		IF NOT EXISTS (
				SELECT 1
				
				FROM Persona
				
				WHERE DNI = @DNI_VIEJO
				)
		BEGIN
			RAISERROR (
					'No existe persona con DNI viejo.',
					16,
					1
					);

			ROLLBACK TRANSACTION;

			RETURN;
		
		END

		-- Validar que NO existe DNI nuevo
		IF EXISTS (
				SELECT 1
				
				FROM Persona
				
				WHERE DNI = @DNI_NUEVO
				)
		BEGIN
			RAISERROR (
					'Ya existe persona con DNI nuevo.',
					16,
					1
					);

			ROLLBACK TRANSACTION;

			RETURN;
		
		END

		-- Insertar nuevo registro Persona (con DNI nuevo)
		INSERT INTO Persona (
			DNI,
			nombre,
			apellido,
			sexo,
			direccion,
			idLocalidad,
			fechaNacimiento,
			nacionalidad,
			activo
			)
		
		SELECT @DNI_NUEVO,
			@Nombre,
			@Apellido,
			@Sexo,
			@Direccion,
			@IdLocalidad,
			@FechaNacimiento,
			@Nacionalidad,
			activo
		
		FROM Persona
		
		WHERE DNI = @DNI_VIEJO;

		-- Actualizar FK en Correos
		UPDATE Correos
		
		SET idPersona = @DNI_NUEVO
		
		WHERE idPersona = @DNI_VIEJO;

		-- Actualizar FK en Telefonos
		UPDATE Telefonos
		
		SET idPersona = @DNI_NUEVO
		
		WHERE idPersona = @DNI_VIEJO;

		-- Actualizar FK en Turnos
		UPDATE Turnos
		
		SET DNIPaciente = @DNI_NUEVO
		
		WHERE DNIPaciente = @DNI_VIEJO;

		-- Actualizar FK en Paciente
		UPDATE Paciente
		
		SET DNI = @DNI_NUEVO,
			ObraSocial = @ObraSocial
		
		WHERE DNI = @DNI_VIEJO;

		-- Eliminar registro viejo Persona
		DELETE
		
		FROM Persona
		
		WHERE DNI = @DNI_VIEJO;

		-- Actualizar Correos con nuevo correo si se pasó
		IF @Correo IS NOT NULL
		BEGIN
			UPDATE Correos
			
			SET correo = @Correo
			
			WHERE idPersona = @DNI_NUEVO;
		
		END

		-- Actualizar Telefonos con nuevo teléfono si se pasó
		IF @Telefono IS NOT NULL
		BEGIN
			UPDATE Telefonos
			
			SET telefono = @Telefono
			
			WHERE idPersona = @DNI_NUEVO;
		
		END

		COMMIT TRANSACTION;

		PRINT 'Paciente modificado correctamente.';
	
	END TRY

	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION;

		PRINT 'Error: ' + ERROR_MESSAGE();
	
	END CATCH

END;

GO

CREATE
	OR

ALTER PROCEDURE sp_AltaMedico @Legajo VARCHAR(20),
	@DNI VARCHAR(20),
	@idEspecialidad INT,
	@Nombre VARCHAR(25),
	@Apellido VARCHAR(25),
	@Nacionalidad VARCHAR(25),
	@FechaNacimiento DATE,
	@Sexo INT,
	@IdLocalidad INT,
	@Telefono VARCHAR(25),
	@Direccion VARCHAR(60),
	@Correo VARCHAR(40),
	@Usuario VARCHAR(25),
	@Contrasenia VARCHAR(25),
	@Alta DATETIME,
	@ultimoIngreso DATETIME,
	@IdRol INT
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION;

		IF EXISTS (
				SELECT 1
				
				FROM Persona
				
				WHERE DNI = @DNI
				)
		BEGIN
			RAISERROR (
					'Ya existe un medico con ese DNI.',
					16,
					1
					);

			ROLLBACK TRANSACTION;

			RETURN;
		
		END

		INSERT INTO Persona (
			DNI,
			nombre,
			apellido,
			sexo,
			direccion,
			idLocalidad,
			fechaNacimiento,
			nacionalidad
			)
		
		VALUES (
			@DNI,
			@Nombre,
			@Apellido,
			@Sexo,
			@Direccion,
			@IdLocalidad,
			@FechaNacimiento,
			@Nacionalidad
			);

		INSERT INTO Medico (
			DNI,
			Legajo,
			idEspecialidad
			)
		
		VALUES (
			@DNI,
			@Legajo,
			@idEspecialidad
			);

		INSERT INTO Usuario (
			DNI,
			nombreUsuario,
			idRol,
			contrasenia,
			alta,
			ultimoIngreso
			)
		
		VALUES (
			@DNI,
			@Usuario,
			@IdRol,
			@Contrasenia,
			@Alta,
			@ultimoIngreso
			)

		INSERT INTO Telefonos (
			idPersona,
			telefono
			)
		
		VALUES (
			@DNI,
			@telefono
			)

		INSERT INTO Correos (
			idPersona,
			correo
			)
		
		VALUES (
			@DNI,
			@Correo
			)

		COMMIT TRANSACTION;

		PRINT 'Médico agregado exitosamente.';
	
	END TRY

	BEGIN CATCH
		ROLLBACK TRANSACTION;

		DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();

		RAISERROR (
				@ErrorMessage,
				16,
				1
				);
	
	END CATCH

END;

GO

CREATE
	OR

ALTER PROCEDURE sp_EliminarMedico @DNI VARCHAR(20)
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION;

		IF NOT EXISTS (
				SELECT 1
				
				FROM Medico
				
				WHERE DNI = @DNI
				)
		BEGIN
			RAISERROR (
					'No existe ningún médico con ese DNI.',
					16,
					1
					);

			ROLLBACK TRANSACTION;

			RETURN;
		
		END

		UPDATE Persona
		
		SET activo = 0
		
		WHERE DNI = @DNI;

		COMMIT TRANSACTION;

		PRINT 'Médico eliminado correctamente.';
	
	END TRY

	BEGIN CATCH
		ROLLBACK TRANSACTION;

		PRINT 'Error: ' + ERROR_MESSAGE();
	
	END CATCH

END;

GO

CREATE PROCEDURE sp_ModificarMedico --  Modificar Medico
	@Legajo VARCHAR(20),
	@DNI VARCHAR(20),
	@idEspecialidad INT,
	@Nombre VARCHAR(25),
	@Apellido VARCHAR(25),
	@Nacionalidad VARCHAR(25),
	@FechaNacimiento DATE,
	@Sexo INT,
	@IdLocalidad INT,
	@Telefono VARCHAR(25),
	@Direccion VARCHAR(60),
	@Correo VARCHAR(40),
	@DNI_NUEVO VARCHAR(20),
	@LEGAJO_NUEVO VARCHAR(20),
	@Usuario VARCHAR(25),
	@Contrasenia VARCHAR(25)
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION;

		PRINT 'Antes de IF1';

		IF NOT EXISTS (
				SELECT 1
				
				FROM Persona
				
				WHERE DNI = @DNI
				)
		BEGIN
			RAISERROR (
					'No existe una persona con ese DNI.',
					16,
					1
					);

			ROLLBACK TRANSACTION;

			RETURN;
		
		END

		PRINT 'Antes de IF2';

		IF NOT EXISTS (
				SELECT 1
				
				FROM Medico
				
				WHERE DNI = @DNI
				)
		BEGIN
			RAISERROR (
					'La persona no está registrada como médico.',
					16,
					1
					);

			ROLLBACK TRANSACTION;

			RETURN;
		
		END

		PRINT 'Antes de Carga';

		UPDATE Usuario
		
		SET DNI = @DNI_NUEVO,
			nombreUsuario = @Usuario,
			contrasenia = @Contrasenia
		
		WHERE DNI = @DNI;

		UPDATE Correos
		
		SET idPersona = @DNI_NUEVO,
			correo = @Correo
		
		WHERE idPersona = @DNI;

		UPDATE Telefonos
		
		SET idPersona = @DNI_NUEVO,
			telefono = @Telefono
		
		WHERE idPersona = @DNI;

		UPDATE Jornadas
		
		SET Legajo = @LEGAJO_NUEVO
		
		WHERE Legajo = @Legajo;

		UPDATE Turnos
		
		SET Legajo = @LEGAJO_NUEVO
		
		WHERE Legajo = @Legajo;

		UPDATE Persona
		
		SET DNI = @DNI_NUEVO,
			nombre = @Nombre,
			apellido = @Apellido,
			sexo = @Sexo,
			direccion = @Direccion,
			idLocalidad = @IdLocalidad,
			fechaNacimiento = @FechaNacimiento,
			nacionalidad = @Nacionalidad
		
		WHERE DNI = @DNI;

		UPDATE Medico
		
		SET DNI = @DNI_NUEVO,
			idEspecialidad = @idEspecialidad,
			legajo = @LEGAJO_NUEVO
		
		WHERE DNI = @DNI;

		PRINT 'Despues de Carga';

		COMMIT TRANSACTION;

		PRINT 'Médico modificado correctamente.';
	
	END TRY

	BEGIN CATCH
		ROLLBACK TRANSACTION;

		PRINT 'Error: ' + ERROR_MESSAGE();
	
	END CATCH

END;
GO

CREATE OR ALTER PROCEDURE sp_ListarTurnosAdmin
AS
BEGIN
	SELECT *
	FROM vw_TurnosconDatos
	ORDER BY FechaPactada,Legajo,DNIPaciente;

END

GO;


CREATE PROCEDURE sp_ListarTurnosMedico
	@Legajo VARCHAR(20),
	@Fecha DATETIME
	AS
	BEGIN
		SELECT *
		FROM vw_TurnosConDatos
		WHERE @Legajo = Legajo
			AND (@Fecha IS NULL OR CONVERT(date, fechaPactada) = CONVERT(date, @Fecha))
		ORDER BY fechaPactada,Legajo,DNIPaciente

	END;
	GO


CREATE OR ALTER PROCEDURE sp_MarcarAsistenciaTurno

@Legajo        VARCHAR(20),
    @FechaPactada  DATETIME,
    @Estado        INT,                
    @Observacion   VARCHAR(200),
	@Diagnostico VARCHAR(120)
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;

        
        IF NOT EXISTS (
            SELECT 1
              FROM Turnos
             WHERE Legajo       = @Legajo
               AND fechaPactada = @FechaPactada
        )
        BEGIN
            RAISERROR('Turno no encontrado.', 16, 1);
            ROLLBACK;
            RETURN;
        END

      
        UPDATE Turnos
           SET estado      = @Estado,
               observacion = @Observacion,
			   diagnostico = @Diagnostico

         WHERE Legajo       = @Legajo
           AND fechaPactada = @FechaPactada;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
        DECLARE @ErrMsg NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@ErrMsg, 16, 1);
    END CATCH
END;

GO
		
	
