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
CREATE PROCEDURE Obtener_Usuarios_Activos
AS
BEGIN
    -- Seleccionar todos los productos cuyo estado sea diferente de 0
    SELECT P.DSC_NOMBRE_PRODUCTO, P.DSC_DESCRIPCION, P.DSC_URL_IMAGEN, P.NUM_PRECIO, P.ESTADO, SUBC.ID_SUBCATE_PRODUCTO, SUBC.DSC_NOMBRE_SUBCATEGORIA, CATE.ID_CATE_PRODUCTO, CATE.DSC_NOMBRE_CATEGORIA
    FROM TB_PRODUCTO AS P
    INNER JOIN TB_SUBCATEGORIA_PRODUCTO AS SUBC ON P.ID_SUBCATE_PRODUCTO = SUBC.ID_SUBCATE_PRODUCTO
    INNER JOIN TB_CATE_PRODUCTO AS CATE ON SUBC.ID_CATE_PRODUCTO_ID = CATE.ID_CATE_PRODUCTO
    WHERE P.ESTADO <> 0;
END
GO
