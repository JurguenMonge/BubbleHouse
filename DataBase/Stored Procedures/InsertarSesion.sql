USE [BDBubbleHouse]
GO
/****** Object:  StoredProcedure [dbo].[Insertar_Sesion]    Script Date: 14/4/2024 04:42:PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[Insertar_Sesion]
	@Id_Sesion NVARCHAR(100),
    @Id_Usuario INT,
    @Dsc_Sesion NVARCHAR(MAX),
    @Dsc_Origen NVARCHAR(MAX),
    @IDRETURN INT OUTPUT,
    @ERRORID INT OUTPUT,
    @ERRORDESCRIPCION NVARCHAR(MAX) OUTPUT
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

        -- Insertar la sesión
        INSERT INTO TB_SESION (ID_SESION, ID_USUARIO, DSC_SESION, DSC_ORIGEN, DSC_CIERRE, FEC_INICIO, FEC_CIERRE, ESTADO)
        VALUES (@Id_Sesion, @Id_Usuario, @Dsc_Sesion, @Dsc_Origen, null, GETDATE(), null, 1);

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
