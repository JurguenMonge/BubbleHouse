
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE Eliminar_SubCategoria_Producto
    @Id_SubCategoria_Producto int,
    @IDRETURN int OUTPUT,
    @ERRORID int OUTPUT,
    @ERRORDESCRIPCION nvarchar(max) OUTPUT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION 

        -- Verificar si la subcategoría de producto existe
        IF NOT EXISTS (SELECT * FROM TB_SUBCATEGORIA_PRODUCTO WHERE ID_SUBCATE_PRODUCTO = @Id_SubCategoria_Producto)
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 12;
            SET @ERRORDESCRIPCION = 'La subcategoría de producto especificada no existe.';
            RETURN; -- Salir del procedimiento almacenado
        END

        -- Eliminar la subcategoría de producto
        UPDATE TB_SUBCATEGORIA_PRODUCTO
        SET ESTADO = 0
        WHERE ID_SUBCATE_PRODUCTO = @Id_SubCategoria_Producto;

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