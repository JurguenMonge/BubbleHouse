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
CREATE PROCEDURE Modificar_Categoria_Ingrediente
    @Id_Categoria_Ingrediente int,
    @Dsc_Nombre_Categoria nvarchar(100),
    @IDRETURN int OUTPUT,
    @ERRORID int OUTPUT,
    @ERRORDESCRIPCION nvarchar(max) OUTPUT
AS
BEGIN
    BEGIN TRY

        -- Verificar si la categoría de ingrediente existe
        IF NOT EXISTS (SELECT * FROM TB_CATE_INGREDIENTE WHERE ID_CATE_INGREDIENTE = @Id_Categoria_Ingrediente)
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 6;
            SET @ERRORDESCRIPCION = 'La categoría de ingrediente especificada no existe.';
        END
		ELSE IF PATINDEX('%[^a-zA-Z0-9 ]%', @Dsc_Nombre_Categoria) > 0 OR LEN(@Dsc_Nombre_Categoria) = 0
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 7;
            SET @ERRORDESCRIPCION = 'El nombre de la categoría contiene caracteres especiales o está vacío.';
        END
		ELSE
		BEGIN
			-- Actualizar la categoría de ingrediente
			UPDATE TB_CATE_INGREDIENTE
			SET DSC_NOMBRE_CATEGORIA = @Dsc_Nombre_Categoria
			WHERE ID_CATE_INGREDIENTE = @Id_Categoria_Ingrediente;

			SET @IDRETURN = @Id_Categoria_Ingrediente;

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
