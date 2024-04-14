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
CREATE PROCEDURE Obtener_Usuario
@Id_Usuario int
AS
BEGIN
    -- Seleccionar todos los usuarios cuyo estado sea diferente de 0
    SELECT U.ID_USUARIO, U.DSC_NOMBRE, U.DSC_PRIMER_APELLIDO, U.DSC_SEGUNDO_APELLIDO, U.DSC_CORREO, U.DSC_PASSWORD, U.FEC_REGISTRO, U.DSC_TELEFONO, U.ESTADO
    FROM TB_USUARIO AS U
    WHERE ESTADO <> 0 AND U.ID_USUARIO = @Id_Usuario;
END
