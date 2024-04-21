USE [BDBubbleHouse]
GO
/****** Object:  StoredProcedure [dbo].[Modificar_Password]    Script Date: 21/04/2024 05:24:22 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[Modificar_Password]
    @Dsc_Token nvarchar(100),
    @New_Password nvarchar(max),
    @IDRETURN int OUTPUT,
    @ERRORID int OUTPUT,
    @ERRORDESCRIPCION nvarchar(max) OUTPUT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION
        IF LEN(@Dsc_Token) = 0
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 2;
            SET @ERRORDESCRIPCION = 'El token está vacío';
        END
        ELSE
        BEGIN
            IF EXISTS (SELECT R.ID_USUARIO FROM TB_RECUPERACION_PASSWORD AS R WHERE R.DSC_TOKEN = @Dsc_Token AND R.ESTADO = 1)
            BEGIN
                DECLARE @FEC_SOLICITUD DATETIME;
                DECLARE @ID_USUARIO int = 0;

                SELECT @FEC_SOLICITUD = R.FEC_SOLICITUD 
                FROM TB_RECUPERACION_PASSWORD AS R 
                WHERE R.DSC_TOKEN = @Dsc_Token AND R.ESTADO = 0;

                IF DATEDIFF(HOUR, @FEC_SOLICITUD, GETDATE()) >= 1
                BEGIN
                    SET @IDRETURN = -1;
                    SET @ERRORID = 1;
                    SET @ERRORDESCRIPCION = 'El Token ya expiró';

                    UPDATE TB_RECUPERACION_PASSWORD 
                    SET FEC_USO = GETDATE(), ESTADO = 0
                    WHERE DSC_TOKEN = @Dsc_Token AND ESTADO = 0;
                END
                ELSE
                BEGIN
                    SELECT @ID_USUARIO = R.ID_USUARIO 
                    FROM TB_RECUPERACION_PASSWORD AS R 
                    WHERE R.DSC_TOKEN = @Dsc_Token AND R.ESTADO = 0;

                    UPDATE TB_USUARIO 
                    SET DSC_PASSWORD = @New_Password
                    WHERE ID_USUARIO = @ID_USUARIO;

                    UPDATE TB_RECUPERACION_PASSWORD 
                    SET FEC_USO = GETDATE(), ESTADO = 0
                    WHERE DSC_TOKEN = @Dsc_Token AND ESTADO = 0;
                END
            END
            ELSE
            BEGIN
				SET @IDRETURN = -1;
                SET @ERRORID = 1;
                SET @ERRORDESCRIPCION = 'El Usuario no existe';
            END  
        END
        COMMIT TRANSACTION
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
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
        SELECT 
            ERROR_SEVERITY(),
            ERROR_PROCEDURE(),
            ERROR_NUMBER(),
            ERROR_MESSAGE(),
            ERROR_LINE(),
            GETUTCDATE();
        
    END CATCH
END
