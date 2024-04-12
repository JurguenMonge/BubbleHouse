-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE Modificar_Usuario
    @Id_Usuario int,
    @Dsc_Nombre nvarchar(50),
    @Dsc_Primer_Apellido nvarchar(50),
    @Dsc_Segundo_Apellido nvarchar(50),
    @Dsc_Correo nvarchar(50),
    @Dsc_Password nvarchar(max),
    @Fec_Registro datetime,
    @Dsc_Telefono nvarchar(50),
    @Estado tinyint,
	@IDRETURN int output,
	@ERRORID int output,
	@ERRORDESCRIPCION nvarchar(max) output
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION 

        -- Verificar si el usuario existe
        IF NOT EXISTS (SELECT * FROM TB_USUARIO WHERE ID_USUARIO = @Id_Usuario)
        BEGIN
			set @idReturn = -1;
			set @errorId = 1;
			set @errorDescripcion = 'El usuario especificado no existe.';
            RETURN; -- Salir del procedimiento almacenado
        END

        IF PATINDEX('%[^a-zA-Z0-9 ]%', @Dsc_Nombre) > 0 OR LEN(@Dsc_Nombre) = 0
        BEGIN
			set @idReturn = -1;
			set @errorId = 2;
			set @errorDescripcion = 'El nombre contiene caracteres especiales o está vacío.';
            RETURN; -- Salir del procedimiento almacenado
        END

        IF PATINDEX('%[^a-zA-Z0-9 ]%', @Dsc_Primer_Apellido) > 0 OR LEN(@Dsc_Primer_Apellido) = 0
        BEGIN
			set @idReturn = -1;
			set @errorId = 2;
			set @errorDescripcion = 'El primer apellido contiene caracteres especiales o está vacío.';
            RETURN; -- Salir del procedimiento almacenado
        END

        IF PATINDEX('%[^a-zA-Z0-9 ]%', @Dsc_Segundo_Apellido) > 0 OR LEN(@Dsc_Segundo_Apellido) = 0
        BEGIN
			set @idReturn = -1;
			set @errorId = 3;
			set @errorDescripcion = 'El segundo apellido contiene caracteres especiales o está vacío.';
            RETURN; -- Salir del procedimiento almacenado
        END

        IF LEN(@Dsc_Correo) = 0
        BEGIN
			set @idReturn = -1;
			set @errorId = 4;
			set @errorDescripcion = 'El correo está vacío.';
            RETURN; -- Salir del procedimiento almacenado
        END

        IF LEN(@Dsc_Password) = 0
        BEGIN
			set @idReturn = -1;
			set @errorId = 5;
			set @errorDescripcion = 'La contraseña está vacía.';
            RETURN; -- Salir del procedimiento almacenado
        END

        -- Actualizar el usuario
        UPDATE TB_USUARIO
        SET DSC_NOMBRE = @Dsc_Nombre,
            DSC_PRIMER_APELLIDO = @Dsc_Primer_Apellido,
            DSC_SEGUNDO_APELLIDO = @Dsc_Segundo_Apellido,
            DSC_CORREO = @Dsc_Correo,
            DSC_PASSWORD = @Dsc_Password,
            FEC_REGISTRO = @Fec_Registro,
            DSC_TELEFONO = @Dsc_Telefono,
            ESTADO = @Estado
        WHERE ID_USUARIO = @Id_Usuario;

        COMMIT TRANSACTION 
    END TRY
    BEGIN CATCH
        set @idReturn = -1;
		set @errorId = ERROR_NUMBER();
		set @errorDescripcion = ERROR_MESSAGE();
        ROLLBACK TRANSACTION 
        -- Aquí puedes manejar el error como prefieras, por ejemplo, lanzar una excepción o registrar el error en una tabla de registro de errores.
    END CATCH
END
GO
