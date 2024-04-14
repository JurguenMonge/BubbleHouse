USE [BDBubbleHouse]
GO
/****** Object:  StoredProcedure [dbo].[Eliminar_Receta]    Script Date: 14/4/2024 12:29:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jurguen Monge
-- Create date: 13-04-2024
-- Description:	SP de eliminar receta
-- =============================================
ALTER PROCEDURE [dbo].[Eliminar_Receta]
	@ID_RECETA INT,
	@IDRETURN int OUTPUT,
    @ERRORID int OUTPUT,
    @ERRORDESCRIPCION nvarchar(max) OUTPUT
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION
			-- Verificar si la receta existe
			IF NOT EXISTS (SELECT ID_RECETA FROM TB_RECETA WHERE ID_RECETA = @ID_RECETA)
			BEGIN
				SET @IDRETURN = -1;
				SET @ERRORID = 12;
				SET @ERRORDESCRIPCION = 'La receta especificada no existe.';
				RETURN;
			END
			 -- Eliminar la receta
			UPDATE TB_RECETA
			SET ESTADO = 0
			WHERE ID_RECETA = @ID_RECETA;

			SET @IDRETURN = 1;
		COMMIT TRANSACTION 
	END TRY
	BEGIN CATCH
        SET @IDRETURN = -1;
        SET @ERRORID = ERROR_NUMBER();
        SET @ERRORDESCRIPCION = ERROR_MESSAGE();

		INSERT INTO TB_ERROR_EN_BASE_DATOS 
			(
				NUM_SEVERIVDAD,
				STORE_PROCEDURE,
				NUM_ERROR,
				DSC_DESCRIPCION,
				NUM_LINEA,
				FEC_ERROR
			) 

			select ERROR_SEVERITY(),
					ERROR_PROCEDURE(),
					ERROR_NUMBER(),
					ERROR_MESSAGE(),
					ERROR_LINE(),
					GETUTCDATE()
        ROLLBACK TRANSACTION 
    END CATCH
END
