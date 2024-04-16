SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE Modificar_Receta
	@ID_RECETA INT,
	@ID_PRODUCTO INT,
	@ID_ING_LACTEO INT,
	@ID_ING_SABOR INT,
	@ID_ING_AZUCAR INT,
	@ID_ING_TOPPING INT,
	@ID_ING_BORDEADO INT,
	@ID_ING_BUBBLES INT,
	@DSC_NOMBRE NVARCHAR(MAX),
	@DSC_TAMANO NVARCHAR(50),
	@IDRETURN int OUTPUT,
    @ERRORID int OUTPUT,
    @ERRORDESCRIPCION nvarchar(max) OUTPUT
AS
BEGIN
	 BEGIN TRY
			IF NOT EXISTS (SELECT ID_RECETA FROM TB_RECETA WHERE ID_RECETA = @ID_RECETA)
			BEGIN
				SET @IDRETURN = -1;
				SET @ERRORID = 1;
				SET @ERRORDESCRIPCION = 'La receta especificada no existe.';
			END
			ELSE IF NOT EXISTS (SELECT ID_PRODUCTO FROM TB_PRODUCTO WHERE ID_PRODUCTO = @ID_PRODUCTO)
			BEGIN
				SET @IDRETURN = -1;
				SET @ERRORID = 2;
				SET @ERRORDESCRIPCION = 'El producto especificado no existe.';
				
			END
			ELSE IF NOT EXISTS (SELECT ID_INGREDIENTE FROM TB_INGREDIENTE WHERE ID_INGREDIENTE = @ID_ING_LACTEO)
			BEGIN
				SET @IDRETURN = -1;
				SET @ERRORID = 3;
				SET @ERRORDESCRIPCION = 'El ingrediente lacteo especificado no existe.';
				 
			END
			ELSE IF NOT EXISTS (SELECT ID_INGREDIENTE FROM TB_INGREDIENTE WHERE ID_INGREDIENTE = @ID_ING_SABOR)
			BEGIN
				SET @IDRETURN = -1;
				SET @ERRORID = 4;
				SET @ERRORDESCRIPCION = 'El ingrediente sabor especificado no existe.';
				
			END
			ELSE IF NOT EXISTS (SELECT ID_INGREDIENTE FROM TB_INGREDIENTE WHERE ID_INGREDIENTE = @ID_ING_AZUCAR)
			BEGIN
				SET @IDRETURN = -1;
				SET @ERRORID = 5;
				SET @ERRORDESCRIPCION = 'El ingrediente azúcar especificado no existe.';
				 
			END
			ELSE IF NOT EXISTS (SELECT ID_INGREDIENTE FROM TB_INGREDIENTE WHERE ID_INGREDIENTE = @ID_ING_TOPPING)
			BEGIN
				SET @IDRETURN = -1;
				SET @ERRORID = 6;
				SET @ERRORDESCRIPCION = 'El ingrediente topping especificado no existe.';
				 
			END
			ELSE IF NOT EXISTS (SELECT ID_INGREDIENTE FROM TB_INGREDIENTE WHERE ID_INGREDIENTE = @ID_ING_BORDEADO)
			BEGIN
				SET @IDRETURN = -1;
				SET @ERRORID = 7;
				SET @ERRORDESCRIPCION = 'El ingrediente de bordeado especificado no existe.';
				 
			END
			ELSE IF NOT EXISTS (SELECT ID_INGREDIENTE FROM TB_INGREDIENTE WHERE ID_INGREDIENTE = @ID_ING_BUBBLES)
			BEGIN
				SET @IDRETURN = -1;
				SET @ERRORID = 8;
				SET @ERRORDESCRIPCION = 'El ingrediente de bubble especificado no existe.';
				 
			END
			ELSE IF PATINDEX('%[^a-zA-Z0-9 ":]%', @Dsc_Nombre) > 0 OR LEN(@Dsc_Nombre) = 0
			BEGIN
				SET @idReturn = -1;
				SET @errorId = 9;
				SET @errorDescripcion = 'El nombre contiene caracteres especiales o está vacío.';
				
			END
			ELSE IF PATINDEX('%[^a-zA-Z0-9 ]%', @DSC_TAMANO) > 0 OR LEN(@DSC_TAMANO) = 0
			BEGIN
				set @idReturn = -1;
				set @errorId = 2;
				set @errorDescripcion = 'El tamaño contiene caracteres especiales o está vacío.';
				
			END
			ELSE
			BEGIN
				UPDATE TB_RECETA SET
					ID_PRODUCTO = @ID_PRODUCTO,
					ID_ING_LACTEO = @ID_ING_LACTEO,
					ID_ING_SABOR = @ID_ING_SABOR,
					ID_ING_AZUCAR = @ID_ING_AZUCAR,
					ID_ING_TOPPING = @ID_ING_TOPPING,
					ID_ING_BORDEADO = @ID_ING_BORDEADO,
					ID_ING_BUBBLES = @ID_ING_BUBBLES,
					DSC_NOMBRE = @DSC_NOMBRE,
					DSC_TAMANO = @DSC_TAMANO,
					FECHA = GETUTCDATE(),
					ESTADO = 1
				WHERE ID_RECETA = @ID_RECETA

        END 
    END TRY
    BEGIN CATCH
        SET @IDRETURN = -1;
        SET @ERRORID = ERROR_NUMBER();
        SET @ERRORDESCRIPCION = ERROR_MESSAGE();

		INSERT INTO TB_ERROR_EN_BASE_DATOS 
			(
				NUM_SEVERIVDAD,
				STORE_PROCEDURE,
				NUM_ERROR,
				DSC_DESCRIPCION,
				NUM_LINEA,
				FEC_ERROR
			) 

			select ERROR_SEVERITY(),
					ERROR_PROCEDURE(),
					ERROR_NUMBER(),
					ERROR_MESSAGE(),
					ERROR_LINE(),
					GETUTCDATE()
        
    END CATCH
END
GO
