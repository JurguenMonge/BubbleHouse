SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[Modificar_Factura]
	@Id_Factura int,
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

		IF NOT EXISTS (SELECT * FROM TB_FACTURA WHERE ID_FACTURA = @Id_Factura)
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 1;
            SET @ERRORDESCRIPCION = 'La factura especificada no existe.';
        END
		ELSE IF NOT EXISTS (SELECT * FROM TB_PRODUCTO WHERE ID_PRODUCTO = @Id_Producto)
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 1;
            SET @ERRORDESCRIPCION = 'El producto especificado no existe.';
        END
		ELSE IF NOT EXISTS (SELECT * FROM TB_SESION WHERE ID_SESION = @Id_Sesion)
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 1;
            SET @ERRORDESCRIPCION = 'La sesión especificada no existe.';
        END
		ELSE IF @Num_Subtotal IS NULL OR LEN(@Num_Subtotal) = 0
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 1;
            SET @ERRORDESCRIPCION = 'El subtotal es nulo o está vacío.';
        END
		ELSE IF @Num_Descuento IS NULL OR LEN(@Num_Descuento) = 0
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 1;
            SET @ERRORDESCRIPCION = 'El descuento es nulo o está vacío.';
        END
		ELSE IF @Num_Total IS NULL OR LEN(@Num_Descuento) = 0
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 1;
            SET @ERRORDESCRIPCION = 'El total es nulo o está vacío.';
        END
		ELSE
		BEGIN
			UPDATE TB_FACTURA 
			SET ID_PRODUCTO = @Id_Producto,
				ID_SESION = @Id_Sesion, 
				NUM_SUBTOTAL = @Num_Subtotal, 
				NUM_DESCUENTO = @Num_Descuento, 
				NUM_TOTAL = @Num_Total, 
				FECHA = GETUTCDATE()
			WHERE ID_FACTURA = @Id_Factura;

			SET @IDRETURN = @Id_Factura;
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
