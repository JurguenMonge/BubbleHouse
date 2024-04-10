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
CREATE PROCEDURE Eliminar_Producto
    @Id_Producto int
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION 

        -- Verificar si el producto existe
        IF NOT EXISTS (SELECT * FROM TB_PRODUCTO WHERE ID_PRODUCTO = @Id_Producto)
        BEGIN
            PRINT 'El producto especificado no existe.';
            RETURN; -- Salir del procedimiento almacenado
        END

        -- Cambiar el estado del producto a 0 (desactivado)
        UPDATE TB_PRODUCTO
        SET ESTADO = 0
        WHERE ID_PRODUCTO = @Id_Producto;

        COMMIT TRANSACTION 
    END TRY
    BEGIN CATCH
        -- Si ocurre algún error durante la transacción, se deshace la transacción
        ROLLBACK TRANSACTION 
        -- Aquí puedes manejar el error como prefieras, por ejemplo, lanzar una excepción o registrar el error en una tabla de registro de errores.
    END CATCH
END
GO

