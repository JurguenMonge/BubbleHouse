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
CREATE PROCEDURE Solicitar_Login
    @Dsc_Correo nvarchar(50),
    @IDRETURN int OUTPUT,
    @ERRORID int OUTPUT,
    @ERRORDESCRIPCION nvarchar(max) OUTPUT
AS
BEGIN
    BEGIN TRY
	IF PATINDEX('%[^a-zA-Z0-9@. ]%', @Dsc_Correo) > 0 OR LEN(@Dsc_Correo) = 0
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 7;
            SET @ERRORDESCRIPCION = 'El Correo contiene caracteres especiales o está vacío.';
        END
	ElSE
		BEGIN
			SELECT U.ID_USUARIO, U.DSC_NOMBRE, U.DSC_PRIMER_APELLIDO, U.DSC_SEGUNDO_APELLIDO, U.DSC_CORREO, U.DSC_PASSWORD, U.FEC_REGISTRO, U.DSC_TELEFONO, U.ESTADO
			FROM TB_USUARIO AS U
			WHERE ESTADO <> 0 AND U.ID_USUARIO = @Dsc_Correo;
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
