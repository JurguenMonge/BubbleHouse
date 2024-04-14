
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE Obtener_Cate_Productos_Activos
AS
BEGIN
    -- Seleccionar todos las categorías de productos cuyo estado sea diferente de 0
    SELECT CP.ID_CATE_PRODUCTO, CP.DSC_NOMBRE_CATEGORIA
    FROM TB_CATE_PRODUCTO AS CP
    WHERE CP.ESTADO <> 0;
END
GO