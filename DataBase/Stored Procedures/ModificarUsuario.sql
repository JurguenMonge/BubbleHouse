USE [BDBubbleHouse]
GO
/****** Object:  StoredProcedure [dbo].[Modificar_Usuario]    Script Date: 14/4/2024 03:50:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[Modificar_Usuario]
    @Id_Usuario int,
    @Dsc_Nombre nvarchar(50),
    @Dsc_Primer_Apellido nvarchar(50),
    @Dsc_Segundo_Apellido nvarchar(50),
    @Dsc_Correo nvarchar(50),
    @Dsc_Password nvarchar(max),
    @Dsc_Telefono nvarchar(50),
	@IDRETURN int output,
	@ERRORID int output,
	@ERRORDESCRIPCION nvarchar(max) output
AS
BEGIN
    BEGIN TRY
        IF EXISTS (SELECT DSC_CORREO, ID_USUARIO FROM TB_USUARIO WHERE DSC_CORREO = @Dsc_Correo AND ID_USUARIO <> @Id_Usuario)
			BEGIN
				SET @IDRETURN = -1;
				SET @ERRORID = 1;
				SET @ERRORDESCRIPCION = 'Correo ya registrado en otro usuario';
			END
			ELSE
				IF NOT EXISTS (SELECT ID_USUARIO FROM TB_USUARIO WHERE ID_USUARIO = @Id_Usuario)
					BEGIN
						SET @IDRETURN = -1;
						SET @ERRORID = 2;
						SET @ERRORDESCRIPCION = 'El Usuario seleccionado no existe';
				END
			ELSE
				IF PATINDEX('%[^a-zA-Z ]%', @Dsc_Nombre) > 0 OR LEN(@Dsc_Nombre) = 0
				BEGIN
				SET @IDRETURN = -1;
					SET @ERRORID = 3;
					SET @ERRORDESCRIPCION = 'El nombre contiene caracteres especiales o está vacío.';
				END
			ELSE
				IF PATINDEX('%[^a-zA-Z ]%', @Dsc_Primer_Apellido) > 0 OR LEN(@Dsc_Primer_Apellido) = 0
				BEGIN
					SET @IDRETURN = -1;
					SET @ERRORID = 4;
					SET @ERRORDESCRIPCION = 'El primer apellido contiene caracteres especiales o está vacío.';
				END
			ELSE
				IF PATINDEX('%[^a-zA-Z ]%', @Dsc_Segundo_Apellido) > 0 OR LEN(@Dsc_Segundo_Apellido) = 0
				BEGIN
					SET @IDRETURN = -1;
					SET @ERRORID = 5;
					SET @ERRORDESCRIPCION = 'El segundo apellido contiene caracteres especiales o está vacío.';
				END
			ELSE
				IF LEN(@Dsc_Password) = 0
				BEGIN
					SET @IDRETURN = -1;
					SET @ERRORID = 6;
					SET @ERRORDESCRIPCION = 'La contraseña está vacía.';
				END
			ELSE
			IF LEN(@Dsc_Telefono) = 0
				BEGIN
					SET @IDRETURN = -1;
					SET @ERRORID = 7;
					SET @ERRORDESCRIPCION = 'El telefono está vacío.';
				END
			ELSE
				BEGIN
					BEGIN TRANSACTION
						UPDATE TB_USUARIO SET
						DSC_NOMBRE = @Dsc_Nombre,
						DSC_PRIMER_APELLIDO = @Dsc_Primer_Apellido,
						DSC_SEGUNDO_APELLIDO = @Dsc_Segundo_Apellido,
						DSC_CORREO = @Dsc_Correo,
						DSC_PASSWORD = @Dsc_Password,
						FEC_REGISTRO = GETUTCDATE(),
						DSC_TELEFONO = @Dsc_Telefono,
						ESTADO = 1
						WHERE ID_USUARIO = @Id_Usuario

						set @idReturn = scope_identity();
				COMMIT TRANSACTION
			END
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
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
