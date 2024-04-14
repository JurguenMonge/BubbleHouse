SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE Modificar_Producto
    @Id_Producto int,
    @Id_SubCategoria_Producto int, 
    @Dsc_Nombre_Producto nvarchar(100),
    @Dsc_Descripcion nvarchar(max),
    @Dsc_Url_Imagen nvarchar(max),
    @Num_Precio float,
    @IDRETURN int OUTPUT,
    @ERRORID int OUTPUT,
    @ERRORDESCRIPCION nvarchar(max) OUTPUT
AS
BEGIN
    BEGIN TRY

        -- Verificar si el producto existe
        IF NOT EXISTS (SELECT * FROM TB_PRODUCTO WHERE ID_PRODUCTO = @Id_Producto)
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 6;
            SET @ERRORDESCRIPCION = 'El producto especificado no existe.';
        END
		ELSE IF NOT EXISTS (SELECT * FROM TB_SUBCATEGORIA_PRODUCTO WHERE ID_SUBCATE_PRODUCTO = @Id_SubCategoria_Producto)
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 7;
            SET @ERRORDESCRIPCION = 'La subcategoría especificada no existe.';
        END

        ELSE IF PATINDEX('%[^a-zA-Z0-9 ]%', @Dsc_Nombre_Producto) > 0 OR LEN(@Dsc_Nombre_Producto) = 0
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 8;
            SET @ERRORDESCRIPCION = 'El nombre del producto contiene caracteres especiales o está vacío.';
        END
		ELSE IF PATINDEX('%[^a-zA-Z0-9 ]%', @Dsc_Descripcion) > 0 OR LEN(@Dsc_Descripcion) = 0
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 9;
            SET @ERRORDESCRIPCION = 'La descripción del producto contiene caracteres especiales o está vacía.';
        END
		ELSE IF PATINDEX('%[^a-zA-Z0-9:/. ]%', @Dsc_Url_Imagen) > 0 OR LEN(@Dsc_Url_Imagen) = 0
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 10;
            SET @ERRORDESCRIPCION = 'La URL de la imagen contiene caracteres no válidos o está vacía.';
        END
		ELSE IF ISNUMERIC(@Num_Precio) <> 1
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 11;
            SET @ERRORDESCRIPCION = 'El precio del producto debe ser un valor numérico.';
        END
		ELSE
		BEGIN
			-- Actualizar el producto
			UPDATE TB_PRODUCTO
			SET ID_SUBCATE_PRODUCTO = @Id_SubCategoria_Producto,
				DSC_NOMBRE_PRODUCTO = @Dsc_Nombre_Producto,
				DSC_DESCRIPCION = @Dsc_Descripcion,
				DSC_URL_IMAGEN = @Dsc_Url_Imagen,
				NUM_PRECIO = @Num_Precio
			WHERE ID_PRODUCTO = @Id_Producto;

			SET @IDRETURN = @Id_Producto;

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
