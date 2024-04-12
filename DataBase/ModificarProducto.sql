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
        BEGIN TRANSACTION 

        -- Verificar si el producto existe
        IF NOT EXISTS (SELECT * FROM TB_PRODUCTO WHERE ID_PRODUCTO = @Id_Producto)
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 6;
            SET @ERRORDESCRIPCION = 'El producto especificado no existe.';
            RETURN; -- Salir del procedimiento almacenado
        END

        -- Verificar si la subcategoría especificada existe
        IF NOT EXISTS (SELECT * FROM TB_SUBCATEGORIA_PRODUCTO WHERE ID_SUBCATE_PRODUCTO = @Id_SubCategoria_Producto)
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 7;
            SET @ERRORDESCRIPCION = 'La subcategoría especificada no existe.';
            RETURN; -- Salir del procedimiento almacenado
        END

        -- Validaciones de datos
        IF PATINDEX('%[^a-zA-Z0-9 ]%', @Dsc_Nombre_Producto) > 0 OR LEN(@Dsc_Nombre_Producto) = 0
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 8;
            SET @ERRORDESCRIPCION = 'El nombre del producto contiene caracteres especiales o está vacío.';
            RETURN; -- Salir del procedimiento almacenado
        END

        IF PATINDEX('%[^a-zA-Z0-9 ]%', @Dsc_Descripcion) > 0 OR LEN(@Dsc_Descripcion) = 0
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 9;
            SET @ERRORDESCRIPCION = 'La descripción del producto contiene caracteres especiales o está vacía.';
            RETURN; -- Salir del procedimiento almacenado
        END

        IF PATINDEX('%[^a-zA-Z0-9:/. ]%', @Dsc_Url_Imagen) > 0 OR LEN(@Dsc_Url_Imagen) = 0
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 10;
            SET @ERRORDESCRIPCION = 'La URL de la imagen contiene caracteres no válidos o está vacía.';
            RETURN; -- Salir del procedimiento almacenado
        END

        IF ISNUMERIC(@Num_Precio) <> 1
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 11;
            SET @ERRORDESCRIPCION = 'El precio del producto debe ser un valor numérico.';
            RETURN; -- Salir del procedimiento almacenado
        END

        -- Actualizar el producto
        UPDATE TB_PRODUCTO
        SET ID_SUBCATE_PRODUCTO = @Id_SubCategoria_Producto,
            DSC_NOMBRE_PRODUCTO = @Dsc_Nombre_Producto,
            DSC_DESCRIPCION = @Dsc_Descripcion,
            DSC_URL_IMAGEN = @Dsc_Url_Imagen,
            NUM_PRECIO = @Num_Precio
        WHERE ID_PRODUCTO = @Id_Producto;

        SET @IDRETURN = @Id_Producto;

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
GO
