
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Fernanda
-- Create date: 13-04-2024
-- Description:	SP Insertar Receta
-- =============================================
CREATE PROCEDURE Insertar_Receta

	@ID_RECETA INT,
	@ID_PRODUCTO INT,
	@ID_ING_LACTEO INT,
	@ID_ING_SABOR INT,
	@ID_ING_AZUCAR INT,
	@ID_ING_TOPPING INT,
	@ID_ING_BORDEADO INT,
	@ID_ING_BUBBLES INT,
	@DSC_NOMBRE VARCHAR(MAX),
	@DSC_TAMANO VARCHAR(50),
	@IDRETURN int OUTPUT,
    @ERRORID int OUTPUT,
    @ERRORDESCRIPCION nvarchar(max) OUTPUT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION

        IF NOT EXISTS (SELECT ID_PRODUCTO FROM TB_PRODUCTO WHERE ID_PRODUCTO = @ID_PRODUCTO)
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 1;
            SET @ERRORDESCRIPCION = 'El producto especificado no existe.';
            RETURN; -- Salir del procedimiento almacenado
        END

		IF NOT EXISTS (SELECT ID_INGREDIENTE FROM TB_INGREDIENTE WHERE ID_INGREDIENTE = @ID_ING_LACTEO)
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 1;
            SET @ERRORDESCRIPCION = 'El Lacteo especificado no existe.';
            RETURN; -- Salir del procedimiento almacenado
        END

	    IF NOT EXISTS (SELECT ID_INGREDIENTE FROM TB_INGREDIENTE WHERE ID_INGREDIENTE = @ID_ING_SABOR)
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 1;
            SET @ERRORDESCRIPCION = 'El Sabor especificado no existe.';
            RETURN; -- Salir del procedimiento almacenado
        END

		IF NOT EXISTS (SELECT ID_INGREDIENTE FROM TB_INGREDIENTE WHERE ID_INGREDIENTE = @ID_ING_AZUCAR)
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 1;
            SET @ERRORDESCRIPCION = 'El azucar especificado no existe.';
            RETURN; -- Salir del procedimiento almacenado
        END

	    IF NOT EXISTS (SELECT ID_INGREDIENTE FROM TB_INGREDIENTE WHERE ID_INGREDIENTE = @ID_ING_TOPPING)
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 1;
            SET @ERRORDESCRIPCION = 'El Topping especificado no existe.';
            RETURN; -- Salir del procedimiento almacenado
        END

		IF NOT EXISTS (SELECT ID_INGREDIENTE FROM TB_INGREDIENTE WHERE ID_INGREDIENTE = @ID_ING_BORDEADO)
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 1;
            SET @ERRORDESCRIPCION = 'El Bordeado especificado no existe.';
            RETURN; -- Salir del procedimiento almacenado
        END

		IF NOT EXISTS (SELECT ID_INGREDIENTE FROM TB_INGREDIENTE WHERE ID_INGREDIENTE = @ID_ING_BUBBLES)
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 1;
            SET @ERRORDESCRIPCION = 'El Bubbles especificado no existe.';
            RETURN; -- Salir del procedimiento almacenado
        END

	    IF PATINDEX('%[^a-zA-Z0-9 ]%', @DSC_NOMBRE) > 0 OR LEN(@DSC_NOMBRE) = 0
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 2;
            SET @ERRORDESCRIPCION = 'El nombre del producto contiene caracteres especiales o está vacío.';
            RETURN; -- Salir del procedimiento almacenado
        END

		IF PATINDEX('%[^a-zA-Z0-9 ]%', @DSC_TAMANO) > 0 OR LEN(@DSC_TAMANO) = 0
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 2;
            SET @ERRORDESCRIPCION = 'El tamaño del producto contiene caracteres especiales o está vacío.';
            RETURN; -- Salir del procedimiento almacenado
        END

		INSERT INTO TB_RECETA(ID_RECETA, ID_PRODUCTO, ID_ING_TOPPING, ID_ING_SABOR, ID_ING_LACTEO, ID_ING_BUBBLES, ID_ING_BORDEADO, ID_ING_AZUCAR, DSC_NOMBRE, DSC_TAMANO, FECHA, ESTADO)
        VALUES (@ID_RECETA, @ID_PRODUCTO, @ID_ING_TOPPING, @ID_ING_SABOR, @ID_ING_LACTEO, @ID_ING_BUBBLES,@ID_ING_BORDEADO, @ID_ING_AZUCAR,@DSC_NOMBRE, @DSC_TAMANO, GETUTCDATE(), 1);

        SET @IDRETURN = SCOPE_IDENTITY();

      COMMIT TRANSACTION 
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

        ROLLBACK TRANSACTION 
    END CATCH
END
GO
