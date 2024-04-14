SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Insertar_SubCategoria_Producto]
	@Id_Categoria_Producto int,
    @Dsc_Nombre_SubCategoria nvarchar(100),
    @IDRETURN int OUTPUT,
    @ERRORID int OUTPUT,
    @ERRORDESCRIPCION nvarchar(max) OUTPUT
AS
BEGIN
    BEGIN TRY

		IF NOT EXISTS (SELECT * FROM TB_CATE_PRODUCTO WHERE ID_CATE_PRODUCTO = @Id_Categoria_Producto)
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 1;
            SET @ERRORDESCRIPCION = 'La categoría del producto especificada no existe.';
        END
		ELSE IF PATINDEX('%[^a-zA-Z0-9 ]%', @Dsc_Nombre_SubCategoria) > 0 OR LEN(@Dsc_Nombre_SubCategoria) = 0
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 1;
            SET @ERRORDESCRIPCION = 'El nombre de la subcategoría contiene caracteres especiales o está vacío.';
        END
		ELSE
		BEGIN
			-- Insertar la nueva subcategoría de producto
			INSERT INTO TB_CATE_PRODUCTO(ID_CATE_PRODUCTO, DSC_NOMBRE_CATEGORIA, ESTADO)
			VALUES (@Id_Categoria_Producto, @Dsc_Nombre_SubCategoria, 1);

			SET @IDRETURN = SCOPE_IDENTITY();
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
