
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE Eliminar_Categoria_Producto
    @Id_Categoria_Producto int,
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
            SET @ERRORID = 12;
            SET @ERRORDESCRIPCION = 'La categoría de producto especificada no existe.';
            RETURN; -- Salir del procedimiento almacenado
        END

        -- Eliminar la categoría de producto
        UPDATE TB_CATE_PRODUCTO
        SET ESTADO = 0
        WHERE ID_CATE_PRODUCTO = @Id_Categoria_Producto;

        SET @IDRETURN = 1; -- Éxito

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