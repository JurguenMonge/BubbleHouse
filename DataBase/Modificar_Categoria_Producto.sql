
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE Modificar_Categoria_Producto
    @Id_Categoria_Producto int,
    @Dsc_Nombre_Categoria nvarchar(100),
    @IDRETURN int OUTPUT,
    @ERRORID int OUTPUT,
    @ERRORDESCRIPCION nvarchar(max) OUTPUT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION 

        -- Verificar si la categoría de producto existe
        IF NOT EXISTS (SELECT * FROM TB_CATE_PRODUCTO WHERE ID_CATE_PRODUCTO = @Id_Categoria_Producto)
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 6;
            SET @ERRORDESCRIPCION = 'La categoría de producto especificada no existe.';
            RETURN; -- Salir del procedimiento almacenado
        END

        -- Validaciones de datos
        IF PATINDEX('%[^a-zA-Z0-9 ]%', @Dsc_Nombre_Categoria) > 0 OR LEN(@Dsc_Nombre_Categoria) = 0
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 7;
            SET @ERRORDESCRIPCION = 'El nombre de la categoría contiene caracteres especiales o está vacío.';
            RETURN; -- Salir del procedimiento almacenado
        END

        -- Actualizar la categoría de producto
        UPDATE TB_CATE_PRODUCTO
        SET DSC_NOMBRE_CATEGORIA = @Dsc_Nombre_Categoria
        WHERE ID_CATE_PRODUCTO = @Id_Categoria_Producto;

        SET @IDRETURN = @Id_Categoria_Producto;

        COMMIT TRANSACTION 
    END TRY
    BEGIN CATCH
        SET @IDRETURN = -1;
        SET @ERRORID = ERROR_NUMBER();
        SET @ERRORDESCRIPCION = ERROR_MESSAGE();
        ROLLBACK TRANSACTION 
    END CATCH
END
GO