USE [BDBubbleHouse]
GO
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
    @IDRETURN int OUTPUT,
    @ERRORID int OUTPUT,
    @ERRORDESCRIPCION nvarchar(max) OUTPUT
AS
BEGIN
    BEGIN TRY
		IF EXISTS (SELECT * FROM TB_USUARIO WHERE DSC_CORREO = @Dsc_Correo)
		-- Si, el correo si está registrado. Devolver error.
		BEGIN
			SET @IDRETURN = -1;
			SET @ERRORID = 1; --Correo ya registrado
			SET @ERRORDESCRIPCION = 'CORREO YA REGISTRADO';
		END
	ELSE
		BEGIN
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
