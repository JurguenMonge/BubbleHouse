USE [BDBubbleHouse]
GO
/****** Object:  StoredProcedure [dbo].[Insertar_Usuario]    Script Date: 14/4/2024 02:45:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[Insertar_Usuario]
    @Dsc_Nombre nvarchar(50),
    @Dsc_Primer_Apellido nvarchar(50),
    @Dsc_Segundo_Apellido nvarchar(50),
    @Dsc_Correo nvarchar(50),
    @Dsc_Password nvarchar(max),
    @Dsc_Telefono nvarchar(50),
	@ID_ROL int,
    @IDRETURN int OUTPUT,
    @ERRORID int OUTPUT,
    @ERRORDESCRIPCION nvarchar(max) OUTPUT
AS
BEGIN
    BEGIN TRY
			IF EXISTS (SELECT DSC_CORREO FROM TB_USUARIO WHERE DSC_CORREO = @Dsc_Correo)
				-- Si, el correo si está registrado. Devolver error.
				BEGIN
					SET @IDRETURN = -1;
					SET @ERRORID = 1; --Correo ya registrado
					SET @ERRORDESCRIPCION = 'Correo ya registrado';
				END
			ELSE
				IF NOT EXISTS (SELECT ID_ROL FROM TB_ROL WHERE ID_ROL = @ID_ROL)
					BEGIN
						SET @IDRETURN = -1;
						SET @ERRORID = 2;
						SET @ERRORDESCRIPCION = 'El Rol seleccionado no existe';
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
						INSERT INTO TB_USUARIO(
							DSC_NOMBRE,
							DSC_PRIMER_APELLIDO,
							DSC_SEGUNDO_APELLIDO,
							DSC_CORREO,
							DSC_PASSWORD,
							FEC_REGISTRO,
							DSC_TELEFONO,
							ESTADO
						)VALUES(
							@Dsc_Nombre,
							@Dsc_Primer_Apellido,
							@Dsc_Segundo_Apellido,
							@Dsc_Correo,
							@Dsc_Password,
							GETUTCDATE(),
							@Dsc_Telefono,
							1
						)
						set @idReturn = scope_identity();

						INSERT INTO TB_R_ROL_USUARIO(
							ID_ROL,
							ID_USUARIO
						)VALUES(
							@ID_ROL,
							@IDRETURN
						)
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
