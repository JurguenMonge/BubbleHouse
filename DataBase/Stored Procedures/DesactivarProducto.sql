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
CREATE PROCEDURE Desactivar_Producto
    @Id_Producto int,
	@IDRETURN int OUTPUT,
    @ERRORID int OUTPUT,
    @ERRORDESCRIPCION nvarchar(max) OUTPUT
AS
BEGIN
    BEGIN TRY

        -- Verificar si el producto existe
        IF NOT EXISTS (SELECT * FROM TB_PRODUCTO WHERE ID_PRODUCTO = @Id_Producto)
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 12;
            SET @ERRORDESCRIPCION = 'El producto especificado no existe.';
        END
		ELSE
		BEGIN
			-- Cambiar el estado del producto a 0 (desactivado)
			UPDATE TB_PRODUCTO
			SET ESTADO = 2
			WHERE ID_PRODUCTO = @Id_Producto;
			SET @IDRETURN = 1; -- Éxito
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
