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
CREATE PROCEDURE Modificar_Ingrediente
    @Id_Ingrediente int,
    @Id_Cate_Ingrediente int,
    @Dsc_Nombre_Ingrediente nvarchar(100),
    @Dsc_Descripcion nvarchar(max),
    @Dsc_Url_Imagen nvarchar(max),
    @Num_Precio decimal(10,2),
    @IDRETURN int OUTPUT,
    @ERRORID int OUTPUT,
    @ERRORDESCRIPCION nvarchar(max) OUTPUT
AS
BEGIN
    BEGIN TRY

        -- Verificar si el ingrediente existe
        IF NOT EXISTS (SELECT * FROM TB_INGREDIENTE WHERE ID_INGREDIENTE = @Id_Ingrediente)
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 6;
            SET @ERRORDESCRIPCION = 'El ingrediente especificado no existe.';
        END

        ELSE IF NOT EXISTS (SELECT * FROM TB_CATE_INGREDIENTE WHERE ID_CATE_INGREDIENTE = @Id_Cate_Ingrediente)
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 7;
            SET @ERRORDESCRIPCION = 'La categoría de ingrediente especificada no existe.';
        END
		ELSE IF PATINDEX('%[^a-zA-Z0-9 ]%', @Dsc_Nombre_Ingrediente) > 0 OR LEN(@Dsc_Nombre_Ingrediente) = 0
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 8;
            SET @ERRORDESCRIPCION = 'El nombre del ingrediente contiene caracteres especiales o está vacío.';
        END
		ELSE IF PATINDEX('%[^a-zA-Z0-9 ]%', @Dsc_Descripcion) > 0 OR LEN(@Dsc_Descripcion) = 0
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 9;
            SET @ERRORDESCRIPCION = 'La descripción del ingrediente contiene caracteres especiales o está vacía.';
        END
		ELSE IF PATINDEX('%[^a-zA-Z0-9:/. ]%', @Dsc_Url_Imagen) > 0 OR LEN(@Dsc_Url_Imagen) = 0
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 10;
            SET @ERRORDESCRIPCION = 'La URL de la imagen contiene caracteres no válidos o está vacía.';
        END
		ELSE
		BEGIN
			-- Actualizar el ingrediente
			UPDATE TB_INGREDIENTE
			SET ID_CATE_INGREDIENTE = @Id_Cate_Ingrediente,
				DSC_NOMBRE_INGREDIENTE = @Dsc_Nombre_Ingrediente,
				DSC_DESCRIPCION = @Dsc_Descripcion,
				DSC_URL_IMAGEN = @Dsc_Url_Imagen,
				NUM_PRECIO = @Num_Precio
			WHERE ID_INGREDIENTE = @Id_Ingrediente;

			SET @IDRETURN = @Id_Ingrediente;

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
GO
