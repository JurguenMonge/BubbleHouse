SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Modificar_Categoria_Producto]
    @Id_Categoria_Producto int,
    @Dsc_Nombre_Categoria nvarchar(100),
    @IDRETURN int OUTPUT,
    @ERRORID int OUTPUT,
    @ERRORDESCRIPCION nvarchar(max) OUTPUT
AS
BEGIN
    BEGIN TRY

        -- Verificar si la categoría de producto existe
        IF NOT EXISTS (SELECT * FROM TB_CATE_PRODUCTO WHERE ID_CATE_PRODUCTO = @Id_Categoria_Producto)
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 6;
            SET @ERRORDESCRIPCION = 'La categoría de producto especificada no existe.';
        END
		ELSE IF PATINDEX('%[^a-zA-Z0-9 ]%', @Dsc_Nombre_Categoria) > 0 OR LEN(@Dsc_Nombre_Categoria) = 0
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 7;
            SET @ERRORDESCRIPCION = 'El nombre de la categoría contiene caracteres especiales o está vacío.';
        END
		ELSE
		BEGIN
			-- Actualizar la categoría de producto
			UPDATE TB_CATE_PRODUCTO
			SET DSC_NOMBRE_CATEGORIA = @Dsc_Nombre_Categoria
			WHERE ID_CATE_PRODUCTO = @Id_Categoria_Producto;

			SET @IDRETURN = @Id_Categoria_Producto;
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
