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
Create PROCEDURE Obtener_Sesion_By_Usuario
	-- Add the parameters for the stored procedure here
	@Id_Usuario int
AS
BEGIN
	SELECT S.ID_SESION, S.ID_USUARIO, S.DSC_SESION, S.FEC_INICIO, S.ESTADO, U.ID_USUARIO, U.DSC_CORREO, U.DSC_NOMBRE, U.DSC_PRIMER_APELLIDO, U.DSC_SEGUNDO_APELLIDO, U.DSC_TELEFONO, U.ESTADO,
	R.DSC_PERMISOS, R.DSC_TIPO_ROL FROM TB_SESION AS S
	INNER JOIN TB_USUARIO AS U ON U.ID_USUARIO = S.ID_USUARIO
	INNER JOIN TB_R_ROL_USUARIO AS RE ON RE.ID_USUARIO = U.ID_USUARIO
	INNER JOIN TB_ROL AS R ON R.ID_ROL = RE.ID_ROL
	WHERE S.ID_SESION = @Id_Usuario;
END

