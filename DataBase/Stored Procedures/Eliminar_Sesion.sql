USE [BDBubbleHouse]
GO
/****** Object:  StoredProcedure [dbo].[Eliminar_Sesion]    Script Date: 21/04/2024 02:20:27 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>D
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[Eliminar_Sesion]
    @Id_Sesion nvarchar(max),
    @DSC_cierre nvarchar(max),
    @IDRETURN int OUTPUT,
    @ERRORID int OUTPUT,
    @ERRORDESCRIPCION nvarchar(max) OUTPUT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;
        -- Verificar si la sesión existe
        IF NOT EXISTS (SELECT ID_SESION FROM TB_SESION WHERE ID_SESION = @Id_Sesion)
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 12;
            SET @ERRORDESCRIPCION = 'La sesión especificada no existe.';
        END
		ELSE
		BEGIN
			-- Eliminar la sesión
			UPDATE TB_SESION
			SET ESTADO = 0,
			    DSC_CIERRE = @DSC_cierre,
			    FEC_CIERRE = GETDATE()
			WHERE ID_SESION = @Id_Sesion;

			SET @IDRETURN = 1; -- Éxito
		END
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
        SET @IDRETURN = -1;
        SET @ERRORID = ERROR_NUMBER();
        SET @ERRORDESCRIPCION = ERROR_MESSAGE();
        -- Bitácora de error en BD.
		INSERT INTO TB_ERROR_EN_BASE_DATOS 
			(
				NUM_SEVERIVDAD,
				STORE_PROCEDURE,
				NUM_ERROR,
				DSC_DESCRIPCION,
				NUM_LINEA,
				FEC_ERROR
			) 
			SELECT ERROR_SEVERITY(),
				   ERROR_PROCEDURE(),
				   ERROR_NUMBER(),
				   ERROR_MESSAGE(),
				   ERROR_LINE(),
				   GETUTCDATE(); 
    END CATCH
END

