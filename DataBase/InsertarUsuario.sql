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
CREATE PROCEDURE Insertar_Usuario
    @Dsc_Nombre nvarchar(50),
    @Dsc_Primer_Apellido nvarchar(50),
    @Dsc_Segundo_Apellido nvarchar(50),
    @Dsc_Correo nvarchar(50),
    @Dsc_Password nvarchar(max),
    @Fec_Registro datetime,
    @Dsc_Telefono nvarchar(50),
    @Estado tinyint,
    @IDRETURN int OUTPUT,
    @ERRORID int OUTPUT,
    @ERRORDESCRIPCION nvarchar(max) OUTPUT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION 

        IF PATINDEX('%[^a-zA-Z0-9 ]%', @Dsc_Nombre) > 0 OR LEN(@Dsc_Nombre) = 0
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 1;
            SET @ERRORDESCRIPCION = 'El nombre contiene caracteres especiales o está vacío.';
            RETURN; -- Salir del procedimiento almacenado
        END

        IF PATINDEX('%[^a-zA-Z0-9 ]%', @Dsc_Primer_Apellido) > 0 OR LEN(@Dsc_Primer_Apellido) = 0
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 2;
            SET @ERRORDESCRIPCION = 'El primer apellido contiene caracteres especiales o está vacío.';
            RETURN; -- Salir del procedimiento almacenado
        END

        IF PATINDEX('%[^a-zA-Z0-9 ]%', @Dsc_Segundo_Apellido) > 0 OR LEN(@Dsc_Segundo_Apellido) = 0
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 3;
            SET @ERRORDESCRIPCION = 'El segundo apellido contiene caracteres especiales o está vacío.';
            RETURN; -- Salir del procedimiento almacenado
        END

        IF LEN(@Dsc_Correo) = 0
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 4;
            SET @ERRORDESCRIPCION = 'El correo está vacío.';
            RETURN; -- Salir del procedimiento almacenado
        END

        IF LEN(@Dsc_Password) = 0
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 5;
            SET @ERRORDESCRIPCION = 'La contraseña está vacía.';
            RETURN; -- Salir del procedimiento almacenado
        END

        INSERT INTO TB_USUARIO (DSC_NOMBRE, DSC_PRIMER_APELLIDO, DSC_SEGUNDO_APELLIDO, DSC_CORREO, DSC_PASSWORD, FEC_REGISTRO, DSC_TELEFONO, ESTADO)
        VALUES (@Dsc_Nombre, @Dsc_Primer_Apellido, @Dsc_Segundo_Apellido, @Dsc_Correo, @Dsc_Password, @Fec_Registro, @Dsc_Telefono, @Estado);

        SET @IDRETURN = SCOPE_IDENTITY();

        COMMIT TRANSACTION 
    END TRY
    BEGIN CATCH
        SET @IDRETURN = -1;
        SET @ERRORID = ERROR_NUMBER();
        SET @ERRORDESCRIPCION = ERROR_MESSAGE();
        ROLLBACK TRANSACTION 
        -- Aquí puedes manejar el error como prefieras, por ejemplo, lanzar una excepción o registrar el error en una tabla de registro de errores.
    END CATCH
END
GO
