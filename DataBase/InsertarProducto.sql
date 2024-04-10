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
CREATE PROCEDURE Insertar_Producto
    @Id_SubCategoria_Producto int, 
    @Dsc_Nombre_Producto nvarchar(100),
    @Dsc_Descripcion nvarchar(max),
    @Dsc_Url_Imagen nvarchar(max),
    @Num_Precio float
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION 

		IF NOT EXISTS (SELECT * FROM TB_SUBCATEGORIA_PRODUCTO WHERE ID_SUBCATE_PRODUCTO = @Id_SubCategoria_Producto)
		BEGIN
			PRINT 'La subcategoría especificada no existe.';
			RETURN; -- Salir del procedimiento almacenado
		END

		IF PATINDEX('%[^a-zA-Z0-9 ]%', @Dsc_Nombre_Producto) > 0 OR LEN(@Dsc_Nombre_Producto) = 0
		BEGIN
			PRINT 'El nombre del producto contiene caracteres especiales o está vacío.';
			RETURN; -- Salir del procedimiento almacenado
		END

		IF PATINDEX('%[^a-zA-Z0-9 ]%', @Dsc_Descripcion) > 0 OR LEN(@Dsc_Descripcion) = 0
		BEGIN
			PRINT 'La descripción del producto contiene caracteres especiales o está vacía.';
			RETURN; -- Salir del procedimiento almacenado
		END

		IF PATINDEX('%[^a-zA-Z0-9:/. ]%', @Dsc_Url_Imagen) > 0 OR LEN(@Dsc_Url_Imagen) = 0
		BEGIN
			PRINT 'La URL de la imagen contiene caracteres no válidos o está vacía.';
			RETURN; -- Salir del procedimiento almacenado
		END

		IF ISNUMERIC(@Num_Precio) <> 1
		BEGIN
			PRINT 'El precio del producto debe ser un valor numérico.';
			RETURN; -- Salir del procedimiento almacenado
		END

		INSERT INTO TB_PRODUCTO (ID_SUBCATE_PRODUCTO, DSC_NOMBRE_PRODUCTO, DSC_DESCRIPCION, DSC_URL_IMAGEN, NUM_PRECIO, ESTADO)
		VALUES (@Id_SubCategoria_Producto, @Dsc_Nombre_Producto, @Dsc_Descripcion, @Dsc_Url_Imagen, @Num_Precio, 1);

        COMMIT TRANSACTION 
    END TRY
    BEGIN CATCH
        -- Si ocurre algún error durante la transacción, se deshace la transacción
        ROLLBACK TRANSACTION 
        -- Aquí puedes manejar el error como prefieras, por ejemplo, lanzar una excepción o registrar el error en una tabla de registro de errores.
    END CATCH
END
GO

