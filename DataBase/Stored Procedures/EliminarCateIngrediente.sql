USE [BDBubbleHouse]
GO
/****** Object:  StoredProcedure [dbo].[Eliminar_Categoria_Ingrediente]    Script Date: 15/4/2024 06:14:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[Eliminar_Categoria_Ingrediente]
    @Id_Categoria_Ingrediente int,
    @IDRETURN int OUTPUT,
    @ERRORID int OUTPUT,
    @ERRORDESCRIPCION nvarchar(max) OUTPUT
AS
BEGIN
    BEGIN TRY

        -- Verificar si la categoría de ingrediente existe
        IF NOT EXISTS (SELECT ID_CATE_INGREDIENTE FROM TB_CATE_INGREDIENTE WHERE ID_CATE_INGREDIENTE = @Id_Categoria_Ingrediente)
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 12;
            SET @ERRORDESCRIPCION = 'La categoría de ingrediente especificada no existe.';
        END
		ELSE
		BEGIN
			-- Eliminar la categoría de ingrediente
			UPDATE TB_CATE_INGREDIENTE
			SET ESTADO = 0
			WHERE ID_CATE_INGREDIENTE = @Id_Categoria_Ingrediente;

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
