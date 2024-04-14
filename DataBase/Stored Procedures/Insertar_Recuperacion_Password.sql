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
CREATE PROCEDURE Insertar_Recuperacion_Password
    @Dsc_Token nvarchar(100),
    @Dsc_Correo nvarchar(50),
    @IDRETURN int OUTPUT,
    @ERRORID int OUTPUT,
    @ERRORDESCRIPCION nvarchar(max) OUTPUT
AS
BEGIN
    BEGIN TRY
		IF PATINDEX('%[^a-zA-Z0-9@.]%', @Dsc_Correo) > 0 OR LEN(@Dsc_Correo) = 0
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 3;
            SET @ERRORDESCRIPCION = 'El correo contiene caracteres especiales o está vacía.';
        END
		ELSE
		BEGIN
			IF LEN(@Dsc_Token) = 0
			BEGIN
				SET @IDRETURN = -1;
				SET @ERRORID = 2;
				SET @ERRORDESCRIPCION = 'El token está vacío';
			END
			ELSE
			BEGIN
				DECLARE @ID_USUARIO int = 0;
				IF NOT EXISTS (SELECT U.ID_USUARIO FROM TB_USUARIO AS U WHERE U.DSC_CORREO = @Dsc_Correo)
				BEGIN
					SET @IDRETURN = -1;
					SET @ERRORID = 1;
					SET @ERRORDESCRIPCION = 'El Usuario no existe';
				END
				ELSE
				BEGIN
					SELECT @ID_USUARIO = U.ID_USUARIO 
					FROM TB_USUARIO AS U 
					WHERE U.DSC_CORREO = @Dsc_Correo;
				END

				INSERT INTO TB_RECUPERACION_PASSWORD (DSC_TOKEN, ID_USUARIO, FEC_SOLICITUD, FEC_USO, ESTADO)
				VALUES (@Dsc_Token,@ID_USUARIO,GETDATE(),NULL,1);

				SET @IDRETURN = SCOPE_IDENTITY();
			END
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

