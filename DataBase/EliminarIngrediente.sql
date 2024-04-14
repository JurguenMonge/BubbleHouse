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
CREATE PROCEDURE Eliminar_Ingrediente
    @Id_Ingrediente int,
    @IDRETURN int OUTPUT,
    @ERRORID int OUTPUT,
    @ERRORDESCRIPCION nvarchar(max) OUTPUT
AS
BEGIN
    BEGIN TRY
        -- Verificar si el ingrediente existe
        IF NOT EXISTS (SELECT * FROM TB_INGREDIENTE WHERE ID_INGREDIENTE = @Id_Ingrediente)
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 12;
            SET @ERRORDESCRIPCION = 'El ingrediente especificado no existe.';
        END
		ELSE
		BEGIN
			-- Eliminar el ingrediente
			UPDATE TB_INGREDIENTE
			SET ESTADO = 0
			WHERE ID_INGREDIENTE = @Id_Ingrediente;

			SET @IDRETURN = 1; -- Éxito
		END
    END TRY
    BEGIN CATCH
        SET @IDRETURN = -1;
        SET @ERRORID = ERROR_NUMBER();
        SET @ERRORDESCRIPCION = ERROR_MESSAGE();
        --Bitacorear error en BD.
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
GO
