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
CREATE PROCEDURE Eliminar_Usuario
    @Id_Usuario int,
    @IDRETURN int OUTPUT,
    @ERRORID int OUTPUT,
    @ERRORDESCRIPCION nvarchar(max) OUTPUT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION 

        -- Verificar si el usuario existe
        IF NOT EXISTS (SELECT * FROM TB_USUARIO WHERE ID_USUARIO = @Id_Usuario)
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 1;
            SET @ERRORDESCRIPCION = 'El usuario especificado no existe.';
            RETURN; -- Salir del procedimiento almacenado
        END

        -- Eliminar el usuario
        DELETE FROM TB_USUARIO
        WHERE ID_USUARIO = @Id_Usuario;

        SET @IDRETURN = 1; -- Éxito

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
