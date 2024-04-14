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
CREATE PROCEDURE Obtener_Ingredientes_Activos
AS
BEGIN
    -- Seleccionar todos los productos cuyo estado sea diferente de 0
    SELECT I.ID_INGREDIENTE, I.DSC_NOMBRE_INGREDIENTE, I.DSC_DESCRIPCION, I.DSC_URL_IMAGEN, I.NUM_PRECIO, CI.ID_CATE_INGREDIENTE, CI.DSC_NOMBRE_CATEGORIA
    FROM TB_INGREDIENTE AS I
    INNER JOIN TB_CATE_INGREDIENTE AS CI ON CI.ID_CATE_INGREDIENTE = I.ID_CATE_INGREDIENTE
    WHERE I.ESTADO <> 0;
END
GO
