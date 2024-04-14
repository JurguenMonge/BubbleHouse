
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE Insertar_Factura
    @Id_Producto int,
	@Id_Sesion int,
    @Num_Subtotal decimal(10,2),
	@Num_Descuento decimal(10,2),
	@Num_Total decimal(10,2),
    @IDRETURN int OUTPUT,
    @ERRORID int OUTPUT,
    @ERRORDESCRIPCION nvarchar(max) OUTPUT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION 

        IF NOT EXISTS (SELECT * FROM TB_PRODUCTO WHERE ID_PRODUCTO = @Id_Producto)
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 1;
            SET @ERRORDESCRIPCION = 'El producto especificado no existe.';
            RETURN; -- Salir del procedimiento almacenado
        END

		IF NOT EXISTS (SELECT * FROM TB_SESION WHERE ID_SESION = @Id_Sesion)
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 1;
            SET @ERRORDESCRIPCION = 'La sesión especificada no existe.';
            RETURN; -- Salir del procedimiento almacenado
        END

        IF @Num_Subtotal IS NULL OR LEN(@Num_Subtotal) = 0
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 1;
            SET @ERRORDESCRIPCION = 'El subtotal es nulo o está vacío.';
            RETURN; -- Salir del procedimiento almacenado
        END

        IF @Num_Descuento IS NULL OR LEN(@Num_Descuento) = 0
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 1;
            SET @ERRORDESCRIPCION = 'El descuento es nulo o está vacío.';
            RETURN; -- Salir del procedimiento almacenado
        END

        IF @Num_Total IS NULL OR LEN(@Num_Descuento) = 0
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 1;
            SET @ERRORDESCRIPCION = 'El total es nulo o está vacío.';
            RETURN; -- Salir del procedimiento almacenado
        END

        INSERT INTO TB_FACTURA(ID_PRODUCTO, ID_SESION, NUM_SUBTOTAL, NUM_DESCUENTO, NUM_TOTAL, FECHA, ESTADO)
        VALUES (@Id_Producto, @Id_Sesion, @Num_Subtotal, @Num_Descuento, @Num_Total, GETUTCDATE(), 1);

        SET @IDRETURN = SCOPE_IDENTITY();

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