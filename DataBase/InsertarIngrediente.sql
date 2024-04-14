USE [BDBubbleHouse]
GO
/****** Object:  StoredProcedure [dbo].[Insertar_Ingrediente]    Script Date: 14/4/2024 13:01:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Insertar_Ingrediente]
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
        BEGIN TRANSACTION 

        IF NOT EXISTS (SELECT * FROM TB_CATE_INGREDIENTE WHERE ID_CATE_INGREDIENTE = @Id_Cate_Ingrediente)
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 1;
            SET @ERRORDESCRIPCION = 'La categoría de ingrediente especificada no existe.';
        END
		ELSE IF PATINDEX('%[^a-zA-Z0-9 ]%', @Dsc_Nombre_Ingrediente) > 0 OR LEN(@Dsc_Nombre_Ingrediente) = 0
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 2;
            SET @ERRORDESCRIPCION = 'El nombre del ingrediente contiene caracteres especiales o está vacío.';
        END
		ELSE IF PATINDEX('%[^a-zA-Z0-9 ]%', @Dsc_Descripcion) > 0 OR LEN(@Dsc_Descripcion) = 0
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 3;
            SET @ERRORDESCRIPCION = 'La descripción del ingrediente contiene caracteres especiales o está vacía.';
        END
		ELSE IF PATINDEX('%[^a-zA-Z0-9:/. ]%', @Dsc_Url_Imagen) > 0 OR LEN(@Dsc_Url_Imagen) = 0
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 4;
            SET @ERRORDESCRIPCION = 'La URL de la imagen contiene caracteres no válidos o está vacía.';
        END
		ELSE
		BEGIN
			-- Aquí podrías agregar validaciones adicionales si es necesario

			INSERT INTO TB_INGREDIENTE (ID_CATE_INGREDIENTE, DSC_NOMBRE_INGREDIENTE, DSC_DESCRIPCION, DSC_URL_IMAGEN, NUM_PRECIO, ESTADO)
			VALUES (@Id_Cate_Ingrediente, @Dsc_Nombre_Ingrediente, @Dsc_Descripcion, @Dsc_Url_Imagen, @Num_Precio, 1);

			SET @IDRETURN = SCOPE_IDENTITY();
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
