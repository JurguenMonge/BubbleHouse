
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE Eliminar_Factura
    @Id_Factura int,
    @IDRETURN int OUTPUT,
    @ERRORID int OUTPUT,
    @ERRORDESCRIPCION nvarchar(max) OUTPUT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION 

        -- Verificar si la factura existe
        IF NOT EXISTS (SELECT * FROM TB_FACTURA WHERE ID_FACTURA = @Id_Factura)
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 12;
            SET @ERRORDESCRIPCION = 'La factura especificada no existe.';
            RETURN; -- Salir del procedimiento almacenado
        END

        -- Eliminar la factura
        UPDATE TB_FACTURA
        SET ESTADO = 0
        WHERE ID_FACTURA = @Id_Factura;

        SET @IDRETURN = 1;

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