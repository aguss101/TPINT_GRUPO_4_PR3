USE ClinicaDB;
GO

CREATE OR ALTER PROCEDURE sp_AltaPaciente
    @DNI VARCHAR(25),
    @Nombre VARCHAR(50),
    @Apellido VARCHAR(50),
    @Nacionalidad VARCHAR(50),
    @FechaNacimiento DATE,
    @Sexo INT,
    @IdLocalidad INT,
    @ObraSocial INT,
    @UltimaAtencion DATETIME,
    @Alta DATETIME,
	@Direccion varchar(50),
	@Telefono VARCHAR(25),
	@Correo VARCHAR(50)
	
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;  

        IF EXISTS (SELECT 1 FROM Persona WHERE DNI = @DNI)
        BEGIN
            RAISERROR('Ya existe una persona con ese DNI.', 16, 1);
            ROLLBACK TRANSACTION;
            RETURN;
        END

        INSERT INTO Persona (DNI, nombre, apellido, sexo, direccion, idLocalidad, fechaNacimiento, nacionalidad)
        VALUES (@DNI, @Nombre, @Apellido, @Sexo, @Direccion,@IdLocalidad, @FechaNacimiento, @Nacionalidad);

        INSERT INTO Paciente (DNI, ObraSocial, ultimaAtencion, alta)
        VALUES (@DNI, @ObraSocial, @UltimaAtencion, @Alta);

		INSERT INTO Telefonos(idPersona,telefono)
		VALUES (@DNI, @telefono)

		INSERT INTO Correos(idPersona, correo)
		VALUES (@DNI, @Correo)

        COMMIT TRANSACTION;
        PRINT 'Paciente agregado exitosamente.';
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        PRINT 'Error: ' + ERROR_MESSAGE();
    END CATCH
END;

GO

CREATE OR ALTER PROCEDURE sp_EliminarPaciente
    @DNI VARCHAR(50)
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

   
        IF NOT EXISTS (SELECT 1 FROM Paciente WHERE DNI = @DNI)
        BEGIN
            RAISERROR('No existe ningún paciente con ese DNI.', 16, 1);
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

CREATE OR ALTER PROCEDURE sp_ModificarPaciente
    @DNI_NUEVO VARCHAR(50),
    @Nombre VARCHAR(50),
    @Apellido VARCHAR(50),
    @Nacionalidad VARCHAR(50),
    @FechaNacimiento DATE,
    @Sexo INT,
    @IdLocalidad INT,    
    @Direccion VARCHAR(50),
    @ObraSocial INT,
    @Correo VARCHAR(50),
    @Telefono VARCHAR(25),
    @DNI_VIEJO VARCHAR(50)
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        -- Validar que existe el paciente con el DNI antiguo
        IF NOT EXISTS (SELECT 1 FROM Persona WHERE DNI = @DNI_VIEJO)
        BEGIN
            RAISERROR('No existe una persona con ese DNI antiguo.', 16, 1);
            ROLLBACK TRANSACTION;
            RETURN;
        END

        IF NOT EXISTS (SELECT 1 FROM Paciente WHERE DNI = @DNI_VIEJO)
        BEGIN
            RAISERROR('La persona no está registrada como paciente.', 16, 1);
            ROLLBACK TRANSACTION;
            RETURN;
        END
              
        -- Actualizar Correos
        UPDATE Correos
        SET idPersona = @DNI_NUEVO, 
            correo = @Correo
        WHERE idPersona = @DNI_VIEJO;

        -- Actualizar Turnos (FK DNI Paciente)
        UPDATE Turnos
        SET DNIPaciente = @DNI_NUEVO
        WHERE DNIPaciente = @DNI_VIEJO;
       
        -- Actualizar Telefonos
        UPDATE Telefonos
        SET idPersona =  @DNI_NUEVO,
            telefono = @Telefono
        WHERE idPersona = @DNI_VIEJO;
           

        -- Actualizar Persona
        UPDATE Persona
        SET DNI = @DNI_NUEVO, 
            nombre = @Nombre,
            apellido = @Apellido,
            sexo = @Sexo,
            direccion = @Direccion,
            idLocalidad = @IdLocalidad,
            fechaNacimiento = @FechaNacimiento,
            nacionalidad = @Nacionalidad
        WHERE DNI = @DNI_VIEJO;
        -- Actualizar Paciente
        UPDATE Paciente
        SET DNI = @DNI_NUEVO, 
            ObraSocial = @ObraSocial
        WHERE DNI = @DNI_VIEJO;

        COMMIT TRANSACTION;
        PRINT 'Paciente modificado correctamente.';
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        PRINT 'Error: ' + ERROR_MESSAGE();
    END CATCH
END;

GO
CREATE OR ALTER PROCEDURE sp_AltaMedico
    @Legajo VARCHAR(50),
	@DNI VARCHAR(50),
	@idEspecialidad INT,
    @Nombre VARCHAR(50),
    @Apellido VARCHAR(50),
    @Nacionalidad VARCHAR(50),
    @FechaNacimiento DATE,
    @Sexo INT,
    @IdLocalidad INT,
	@Telefono VARCHAR(50),
	@Direccion VARCHAR(50),
	@Correo VARCHAR(50)
	
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        IF EXISTS (SELECT 1 FROM Persona WHERE DNI = @DNI)
        BEGIN
            RAISERROR('Ya existe un medico con ese DNI.', 16, 1);
            ROLLBACK TRANSACTION;
            RETURN;
        END

        INSERT INTO Persona (DNI, nombre, apellido, sexo, direccion, idLocalidad, fechaNacimiento, nacionalidad)
        VALUES (@DNI, @Nombre, @Apellido, @Sexo, @Direccion,@IdLocalidad, @FechaNacimiento, @Nacionalidad);

        INSERT INTO Medico(DNI, Legajo, idEspecialidad)
        VALUES (@DNI, @Legajo, @idEspecialidad);

		INSERT INTO Telefonos(idPersona,telefono)
		VALUES (@DNI, @telefono)

		INSERT INTO Correos(idPersona, correo)
		VALUES (@DNI, @Correo)

        COMMIT TRANSACTION;
        PRINT 'Médico agregado exitosamente.';
    END TRY
    BEGIN CATCH
    ROLLBACK TRANSACTION;

    DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
    RAISERROR(@ErrorMessage, 16, 1); 
END CATCH
END;

GO

CREATE OR ALTER PROCEDURE sp_EliminarMedico
    @DNI VARCHAR(50)
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

   
        IF NOT EXISTS (SELECT 1 FROM Medico WHERE DNI = @DNI)
        BEGIN
            RAISERROR('No existe ningún médico con ese DNI.', 16, 1);
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

CREATE OR ALTER PROCEDURE sp_ModificarMedico --  Modificar Medico
	@Legajo VARCHAR(50),
	@DNI VARCHAR(50),
	@idEspecialidad INT,
    @Nombre VARCHAR(50),
    @Apellido VARCHAR(50),
    @Nacionalidad VARCHAR(50),
    @FechaNacimiento DATE,
    @Sexo INT,
    @IdLocalidad INT,
	@Telefono VARCHAR(25),
	@Direccion VARCHAR(50),
	@Correo VARCHAR(50),
	@DNI_NUEVO VARCHAR(50),
	@LEGAJO_NUEVO VARCHAR(50)


AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

    
        PRINT 'Antes de IF1';
        IF NOT EXISTS (SELECT 1 FROM Persona WHERE DNI = @DNI)
        BEGIN
            RAISERROR('No existe una persona con ese DNI.', 16, 1);
            ROLLBACK TRANSACTION;
            RETURN;
        END

 
        PRINT 'Antes de IF2';
        IF NOT EXISTS (SELECT 1 FROM Medico WHERE DNI = @DNI)
        BEGIN
            RAISERROR('La persona no está registrada como médico.', 16, 1);
            ROLLBACK TRANSACTION;
            RETURN;
        END

     
        PRINT 'Antes de Carga';
       

		UPDATE Usuario
		SET DNI = @DNI_NUEVO
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

SELECT * FROM Persona

