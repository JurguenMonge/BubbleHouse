
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[Insertar_Sesion]
	@Id_Sesion NVARCHAR(100),
    @Id_Usuario INT,
    @Dsc_Sesion NVARCHAR(MAX),
    @Dsc_Origen NVARCHAR(MAX),
    @IDRETURN INT OUTPUT,
    @ERRORID INT OUTPUT,
    @ERRORDESCRIPCION NVARCHAR(MAX) OUTPUT
AS
BEGIN
    BEGIN TRY

        -- Verificar si el usuario existe
        IF NOT EXISTS (SELECT * FROM TB_USUARIO WHERE ID_USUARIO = @Id_Usuario)
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 1;
            SET @ERRORDESCRIPCION = 'El usuario especificado no existe.';
        END
		ELSE
		BEGIN
			-- Insertar la sesión
			INSERT INTO TB_SESION (ID_SESION, ID_USUARIO, DSC_SESION, DSC_ORIGEN, DSC_CIERRE, FEC_INICIO, FEC_CIERRE, ESTADO)
			VALUES (@Id_Sesion, @Id_Usuario, @Dsc_Sesion, @Dsc_Origen, null, GETDATE(), null, 1);

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
